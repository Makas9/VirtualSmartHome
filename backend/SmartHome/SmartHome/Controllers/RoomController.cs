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
    public class RoomController : Controller
    {
        public ActionResult RoomList()
        {
            if (HttpContext.Session.GetInt32(UserController._UserID) < 0) return Redirect("../User/UserLogin");

            return View();
        }

        public ActionResult RoomAdd()
        {
            if (HttpContext.Session.GetInt32(UserController._UserID) < 0) return Redirect("../User/UserLogin");

            return View();
        }

        public ActionResult RoomEdit()
        {
            if (HttpContext.Session.GetInt32(UserController._UserID) < 0) return Redirect("../User/UserLogin");

            return View();
        }

        public Room GetRoomList()
        {
            Room room = new Room();

            // TODO

            return room;
        }

        public ActionResult OpenRoomAddWindow()
        {
            if (HttpContext.Session.GetInt32(UserController._UserID) < 0) return Redirect("../User/UserLogin");

            return View("RoomAdd");
        }

        public bool ValidateRoomData(Room roomData)
        {
            bool validated = false;

            // Validation

            return validated;
        }

        [HttpPost]
        public ActionResult CreateRoom(Room roomData)
        {
            if (HttpContext.Session.GetInt32(UserController._UserID) < 0) return Redirect("../User/UserLogin");

            // Validation
            bool validated = ValidateRoomData(roomData);

            return View("RoomList");
        }
    }
}