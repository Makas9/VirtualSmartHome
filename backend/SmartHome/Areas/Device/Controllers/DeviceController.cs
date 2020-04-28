using System;
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
            _httpClient = new HttpClient(clientHandler); // HttpClient for communication through http
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

                device = GetState(device).Result;

                _context.Add(device);
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
            List<Models.Device> devices = _context.Devices.Where(r => r.RoomId == roomID).ToList();

            return devices;
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

        
        public async Task<Models.Device> GetState(Models.Device device) // reiketu pervadint i getDeviceData ar pns.
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
            
            var jsonModel = JsonSerializer.Deserialize<Models.Device>(deviceInfo);

            jsonModel.IpAddress = device.IpAddress;
            jsonModel.Port = device.Port;
            jsonModel.RoomId = device.RoomId;

            return jsonModel;
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
            var device = _context.Devices.FirstOrDefaultAsync(d => d.Id == deviceID).;

            if (device == null)
            {
                return NotFound();
            }

            _httpClient.BaseAddress = new Uri(UrlBuilder(device.IpAddress, device.Port));
            
        }

        public void TurnOff(int deviceID)
        {
            // TODO
        }

        public void Log()
        {
            // TODO
        }
    }
}