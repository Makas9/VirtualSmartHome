﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartHome.Models;
using SmartHome.ViewModels;
using SmartHome.Resident.Controllers;
using System.Net.Http;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Newtonsoft.Json;

namespace SmartHome.Device.Controllers
{

    [Area("Device")]
    public class DeviceController : Controller
    {
        private readonly SmartHomeDbContext _context;
        private HttpClient _httpClient;
        private HttpClientHandler clientHandler = new HttpClientHandler();
        private const string _ViewPath = "../";
        private const string _ControllerPath = "device/device/";

        public DeviceController(SmartHomeDbContext context)
        {
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }; // Ignoring certificates
            _context = context;
            _httpClient = new HttpClient(clientHandler);
            //_httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddDevice(AddDeviceViewModel deviceData)
        {
            if (HttpContext.Session.GetInt32(UserController._UserID) < 0) return Redirect(UserController._LoginPath);

            if (ValidateDeviceData(deviceData))
            {
                Models.Device device = new Models.Device
                {
                    IpAddress = deviceData.IpAddress,
                    Port = deviceData.Port,
                    RoomId = deviceData.RoomID
                };

                device = await GetDeviceData(device);

                //_context.Add(device);
                Models.Device.AddDevice(_context, device);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(OpenRoomDeviceList), new { roomID = deviceData.RoomID });
            }

            return View(_ViewPath + "AddDevice", deviceData);
        }

        public IActionResult AddDevice()
        {
            return View(UserController._LoginPath);
        }

        public ActionResult DeviceSystemComm()
        {
            if (HttpContext.Session.GetInt32(UserController._UserID) < 0) return Redirect(UserController._LoginPath);

            return View();
        }

        public IEnumerable<Models.Device> GetDevicesOfRoom(int roomID)
        {
            //List<Models.Device> devices = _context.Devices.Where(r => r.RoomId == roomID).ToList();
            //return DeviceState;

            return Models.Device.SelectBelongingToRoom(_context, roomID);
        }

        [HttpGet(_ControllerPath + "OpenRoomDeviceList/{roomID}")]
        public ActionResult OpenRoomDeviceList(int? roomID)
        {
            if (roomID == null)
            {
                return NotFound();
            }

            var devices = GetDevicesOfRoom(roomID.Value);

            RoomDeviceListViewModel deviceList = new RoomDeviceListViewModel
            {
                Devices = devices,
                roomID = roomID.Value
            };

            return View(_ViewPath + "RoomDeviceList", deviceList);
        }

        [HttpGet(_ControllerPath + "OpenAddDeviceWindow/{roomID}")]
        public ActionResult OpenAddDeviceWindow(int? roomID)
        {
            if (HttpContext.Session.GetInt32(UserController._UserID) < 0) return Redirect(UserController._LoginPath);

            if (roomID == null)
            {
                return NotFound();
            }

            AddDeviceViewModel vm = new AddDeviceViewModel
            {
                RoomID = roomID.Value
            };

            return View(_ViewPath + "AddDevice", vm);
        }

        
        public async Task<Models.Device> GetDeviceData(Models.Device device)
        {
            _httpClient.BaseAddress = new Uri(UrlBuilder(device.IpAddress, device.Port));

            string deviceInfo = null;

            try
            {
                deviceInfo = await _httpClient.GetStringAsync("api/device");
            } 
            catch (Exception ex)  // fails to connect with device
            {
                throw new ArgumentException($"Could not reach device with IP: {device.IpAddress}, Port: {device.Port}.", nameof(device.IpAddress));
            }

            var options = new JsonSerializerOptions
            {
                 PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var jsonModel = JsonConvert.DeserializeObject<Models.Device>(deviceInfo);

            jsonModel.IpAddress = device.IpAddress;
            jsonModel.Port = device.Port;
            jsonModel.RoomId = device.RoomId;

            return jsonModel;
        }

        public async Task<DeviceState?> GetState(int? deviceID)
        {
            if(deviceID == null)
            {
                new ArgumentException("", nameof(deviceID));
            }

            var device = await _context.Devices.FirstOrDefaultAsync(d => d.Id == deviceID);

            if(device == null)
            {
                new ArgumentException("", nameof(device));
            }

            _httpClient.BaseAddress = new Uri(UrlBuilder(device.IpAddress, device.Port));

            string deviceInfo = null;

            try
            {
                deviceInfo = await _httpClient.GetStringAsync("api/device");
            }
            catch (Exception ex)  // fails to connect with device
            {
                throw new ArgumentException($"Could not reach device with IP: {device.IpAddress}, Port: {device.Port}.", nameof(device.IpAddress));
            }

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var jsonModel = JsonConvert.DeserializeObject<Models.Device>(deviceInfo);

            if(jsonModel.State != null)
            {
                return jsonModel.State.Value;
            }

            return null;
        }

        public string UrlBuilder(string address, int port)
        {
            return "https://" + address + ":" + port.ToString(); ;
        }
        public bool ValidateDeviceData(AddDeviceViewModel deviceData)
        {
            return ModelState.IsValid;
        }

        public void OpenAddScenarioWindow()
        {
            // TODO
        }

        public async Task<IActionResult> TurnOn(int deviceID)
        {
            var state = await GetState(deviceID);
            if(state == null)
            {
                throw new Exception();
            }
            var device = await _context.Devices.Include(d => d.Room).FirstOrDefaultAsync(d => d.Id == deviceID);
            if (state == DeviceState.Off)
            {
                device.State = DeviceState.On;
                var json = JsonConvert.SerializeObject(device);

                var response = await _httpClient.PostAsync(UrlBuilder(device.IpAddress, device.Port) + "/api/device", new StringContent(json, Encoding.UTF8, "application/json"));
                Console.WriteLine(response);
                    
                if (response.IsSuccessStatusCode)
                {
                    var deviceInfo = JsonConvert.DeserializeObject<Models.Device>(await response.Content.ReadAsStringAsync());
                    device.State = deviceInfo.State;
                    //_context.Update(device);
                    Models.Device.UpdateState(_context, device);
                    await _context.SaveChangesAsync();
                }
            }
            return RedirectToAction(nameof(OpenRoomDeviceList), new { roomID = device.RoomId });
            
        }


        public async Task<IActionResult> TurnOff(int deviceID)
        {
            var state = await GetState(deviceID);
            if (state == null)
            {
                throw new Exception();
            }
            var device = await _context.Devices.Include(d => d.Room).FirstOrDefaultAsync(d => d.Id == deviceID);
            if (state == DeviceState.On)
            {
                device.State = DeviceState.Off;
                var json = JsonConvert.SerializeObject(device);

                var response = await _httpClient.PostAsync(UrlBuilder(device.IpAddress, device.Port) + "/api/device", new StringContent(json, Encoding.UTF8, "application/json"));
                Console.WriteLine(response);

                if (response.IsSuccessStatusCode)
                {
                    var deviceInfo = JsonConvert.DeserializeObject<Models.Device>(await response.Content.ReadAsStringAsync());
                    device.State = deviceInfo.State;
                    _context.Update(device);
                    await _context.SaveChangesAsync();
                }
            }
            return RedirectToAction(nameof(OpenRoomDeviceList), new { roomID = device.RoomId });
        }

        public void Log()
        {
            // TODO
        }
    }
}