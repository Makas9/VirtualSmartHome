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
    public class DeviceController : Controller
    {
        private readonly SmartHomeDbContext _context;
        public DeviceController(SmartHomeDbContext context)
        {
            _context = context;
        }

        public ActionResult RoomDeviceList()
        {
            if (HttpContext.Session.GetInt32(UserController._UserID) < 0) return Redirect("../../Resident/Views/UserLogin");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddDevice(AddDeviceViewModel deviceData)
        {
            if (HttpContext.Session.GetInt32(UserController._UserID) < 0) return Redirect("../../Resident/Views/UserLogin");
            
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

            return View("../Views/AddDevice", deviceData);
        }

        public IActionResult AddDevice()
        {
            return View("../../Resident/Views/UserLogin");
        }

        public ActionResult DeviceSystemComm()
        {
            if (HttpContext.Session.GetInt32(UserController._UserID) < 0) return Redirect("../../Resident/Views/UserLogin");

            return View();
        }

        public IEnumerable<Models.Device> GetDevicesOfRoom(int roomID)
        {
            List<Models.Device> devices = _context.Devices.Where(r => r.RoomId == roomID).ToList();

            return devices;
        }

        [HttpGet("Device/Views/RoomDeviceList/{roomID}")]
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

            return View("../Views/RoomDeviceList", deviceList);
        }

        [HttpGet("Device/Views/OpenAddDeviceWindow/{roomID}")]
        public ActionResult OpenAddDeviceWindow(int? roomID)
        {
            if (HttpContext.Session.GetInt32(UserController._UserID) < 0) return Redirect("../../Resident/Views/UserLogin");

            if (roomID == null)
            {
                return NotFound();
            }

            AddDeviceViewModel vm = new AddDeviceViewModel
            {
                RoomID = roomID.Value
            };

            return View("../Views/AddDevice", vm);
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