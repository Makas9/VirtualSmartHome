using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmartHome.Models;
using SmartHome.ViewModels;
using SmartHome.Resident.Controllers;

namespace SmartHome.Device.Controllers
{

    [Area("Device")]
    public class DeviceController : Controller
    {
        private readonly SmartHomeDbContext _context;
        private const string _ViewPath = "../";
        private const string _ControllerPath = "device/device/";

        public DeviceController(SmartHomeDbContext context)
        {
            _context = context;
        }

        /*public ActionResult RoomDeviceList()
        {
            if (HttpContext.Session.GetInt32(UserController._UserID) < 0) return Redirect(UserController._LoginPath);

            return View(_ViewPath + "RoomDeviceList");
        }*/

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

        public bool ValidateDeviceData(AddDeviceViewModel deviceData)
        {
            return ModelState.IsValid;
        }

        public void OpenAddScenarioWindow()
        {
            // TODO
        }

        public void TurnOn(int deviceID)
        {
            // TODO
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