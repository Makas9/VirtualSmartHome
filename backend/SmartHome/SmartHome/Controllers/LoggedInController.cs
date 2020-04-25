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
    public class LoggedInController : Controller
    {
        private int _UserID = -1;

        public ActionResult Index()
        {
            if (HttpContext.Session.GetInt32(DefaultController._UserID) < 0) return Redirect("../Default/Index");

            _UserID = (int)HttpContext.Session.GetInt32(DefaultController._UserID);
            ViewData["UserID"] = _UserID;

            return View("Home");
        }

        public ActionResult Home()
        {
            if (HttpContext.Session.GetInt32(DefaultController._UserID) < 0) return Redirect("../Default/Index");

            return View();
        }

        // --------------------------------------------------
        // ROOM CONTROLS
        // --------------------------------------------------

        public ActionResult RoomSelection()
        {
            if (HttpContext.Session.GetInt32(DefaultController._UserID) < 0) return Redirect("../Default/Index");

            return View();
        }

        [HttpGet]
        public ActionResult Room(string id)
        {
            if (HttpContext.Session.GetInt32(DefaultController._UserID) < 0) return Redirect("../Default/Index");

            ViewData["RoomID"] = id;

            return View();
        }

        public ActionResult RoomNew()
        {
            if (HttpContext.Session.GetInt32(DefaultController._UserID) < 0) return Redirect("../Default/Index");

            return View();
        }

        [HttpPost]
        public ActionResult RoomNew(Room room)
        {
            if (HttpContext.Session.GetInt32(DefaultController._UserID) < 0) return Redirect("../Default/Index");

            // Save room to DB if data is correct

            return View("RoomSelection");
        }

        // --------------------------------------------------
        // DEVICE CONTROLS
        // --------------------------------------------------

        public ActionResult DeviceNew(int id)
        {
            if (HttpContext.Session.GetInt32(DefaultController._UserID) < 0) return Redirect("../Default/Index");

            ViewData["RoomID"] = id;

            return View();
        }

        [HttpPost]
        public ActionResult DeviceNew(Device device)
        {
            if (HttpContext.Session.GetInt32(DefaultController._UserID) < 0) return Redirect("../Default/Index");


            // Save device to DB if data is correct

            return RedirectToAction("Room", new { id = device.RoomId });
        }


        public ActionResult Logout()
        {
            HttpContext.Session.SetInt32(DefaultController._UserID, -1);
            _UserID = -1;

            return Redirect("../Default/Index");
        }
    }
}
