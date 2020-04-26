using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartHome.Models;

namespace SmartHome.Controllers
{
    public class DeviceController : Controller
    {
        public ActionResult RoomDeviceList()
        {
            if (HttpContext.Session.GetInt32(UserController._UserID) < 0) return Redirect("../User/UserLogin");

            return View();
        }

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

        public void GetDevicesOfRoom(int RoomID)
        {
            // TODO
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