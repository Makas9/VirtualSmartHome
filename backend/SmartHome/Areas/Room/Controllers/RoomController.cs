using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmartHome.Models;
using SmartHome.Resident.Controllers;

namespace SmartHome.Room.Controllers
{

    [Area("Room")]
    public class RoomController : Controller
    {
        private readonly SmartHomeDbContext _context;
        private const string _ViewPath = "../";
        private const string _ControllerPath = "device/device/";

        public RoomController(SmartHomeDbContext context)
        {
            _context = context;
        }


        public ActionResult RoomList()
        {
            if (HttpContext.Session.GetInt32(UserController._UserID) < 0) return Redirect(UserController._LoginPath);
            var roomList = GetRoomList();

            return View(_ViewPath + "RoomList", roomList);
        }

        public ActionResult RoomAdd()
        {
            if (HttpContext.Session.GetInt32(UserController._UserID) < 0) return Redirect(UserController._LoginPath);

            return View(_ViewPath + "RoomAdd");
        }

        public ActionResult RoomEdit()
        {
            if (HttpContext.Session.GetInt32(UserController._UserID) < 0) return Redirect(UserController._LoginPath);

            return View(_ViewPath + "RoomEdit");
        }

        public IEnumerable<Models.Room> GetRoomList()
        {
            var rooms = _context.Rooms.Include(r => r.House).ToList();

            // todo: kai bus vartotojas atrinkt pagal prisijungusio vartotojo namus

            return rooms;
        }

        public ActionResult OpenRoomAddWindow()
        {
            if (HttpContext.Session.GetInt32(UserController._UserID) < 0) return Redirect(UserController._LoginPath);

            return View(_ViewPath + "RoomAdd");
        }

        public bool ValidateRoomData(Models.Room roomData)
        {
            return ModelState.IsValid;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoom([Bind("Id", "Name", "HouseId")] Models.Room room)
        {
            if (HttpContext.Session.GetInt32(UserController._UserID) < 0) return Redirect(UserController._LoginPath);

            room.HouseId = 1; // Pridedam prie pirmo namo del demo
            if (ValidateRoomData(room))
            {
                _context.Add(room);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(RoomList));
            }


            return View(_ViewPath + "RoomAdd", room);
        }
    }
}