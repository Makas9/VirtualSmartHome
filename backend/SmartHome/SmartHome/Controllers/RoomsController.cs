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
    public class RoomsController : Controller
    {
        private readonly SmartHomeDbContext _context;

        public RoomsController(SmartHomeDbContext context)
        {
            _context = context;
        }

        [HttpGet("Room/RoomList")]
        public ActionResult RoomList()
        {
            if (HttpContext.Session.GetInt32(UsersController._UserID) < 0) return Redirect("../User/UserLogin");
            var roomList = GetRoomList();

            return View("RoomList", roomList);
        }

        public ActionResult RoomAdd()
        {
            if (HttpContext.Session.GetInt32(UsersController._UserID) < 0) return Redirect("../User/UserLogin");

            return View();
        }

        public ActionResult RoomEdit()
        {
            if (HttpContext.Session.GetInt32(UsersController._UserID) < 0) return Redirect("../User/UserLogin");

            return View();
        }

        public List<Room> GetRoomList()
        {
            var smartHomeDbContext = _context.Rooms.Include(r => r.House);

            // TODO

            return smartHomeDbContext.ToList();
        }

        public ActionResult OpenRoomAddWindow()
        {
            if (HttpContext.Session.GetInt32(UsersController._UserID) < 0) return Redirect("../User/UserLogin");

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
            if (HttpContext.Session.GetInt32(UsersController._UserID) < 0) return Redirect("../User/UserLogin");

            // Validation
            bool validated = ValidateRoomData(roomData);

            return View("RoomList");
        }
    }
}