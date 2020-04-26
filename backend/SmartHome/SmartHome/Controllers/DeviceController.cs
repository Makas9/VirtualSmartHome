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

namespace SmartHome.Controllers
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
            if (HttpContext.Session.GetInt32(UserController._UserID) < 0) return Redirect("../User/UserLogin");

            return View();
        }

        [HttpPost]
        public ActionResult AddDevice()
        {
            if (HttpContext.Session.GetInt32(UserController._UserID) < 0) return Redirect("../User/UserLogin");

            return View();
        }

        public ActionResult DeviceSystemComm()
        {
            if (HttpContext.Session.GetInt32(UserController._UserID) < 0) return Redirect("../User/UserLogin");

            return View();
        }

        public IEnumerable<Device> GetDevicesOfRoom(int roomID)
        {
            List<Device> devices = _context.Devices.Where(r => r.RoomId == roomID).ToList();

            return devices;
        }

        [HttpGet("Device/RoomDeviceList/{roomID}")]
        public ActionResult OpenRoomDeviceList(int? roomID)
        {
            if (roomID == null)
            {
                return NotFound();
            }

            var devices = GetDevicesOfRoom(roomID.Value);

            return View("RoomDeviceList", devices);
        }

        public ActionResult OpenAddDeviceWindow()
        {
            if (HttpContext.Session.GetInt32(UserController._UserID) < 0) return Redirect("../User/UserLogin");

            return View("AddDevice");
        }

        public void AddDevice(Device deviceData)
        {
            // TODO
        }

        public void ValidateDeviceData(Device deviceData)
        {
            // TODO
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