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

namespace SmartHome.Controllers
{
    public class RoomController : Controller
    {
        private readonly SmartHomeDbContext _context;

        public RoomController(SmartHomeDbContext context)
        {
            _context = context;
        }


        public ActionResult RoomList()
        {
            if (HttpContext.Session.GetInt32(UserController._UserID) < 0) return Redirect("../User/UserLogin");
            var roomList = GetRoomList();

            return View("RoomList", roomList);
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

        public List<Room> GetRoomList()
        {
            var smartHomeDbContext = _context.Rooms.Include(r => r.House);

            // todo: kai bus vartotojas atrinkt pagal prisijungusio vartotojo namus

            return smartHomeDbContext.ToList();
        }

        public ActionResult OpenRoomAddWindow()
        {
            if (HttpContext.Session.GetInt32(UserController._UserID) < 0) return Redirect("../User/UserLogin");

            return View("RoomAdd");
        }

        public bool ValidateRoomData(Room roomData)
        {
            return ModelState.IsValid;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoom([Bind("Id","Name","HouseId")] Room room)
        {
            if (HttpContext.Session.GetInt32(UserController._UserID) < 0) return Redirect("../User/UserLogin");

            room.HouseId = 1; // Pridedam prie pirmo namo del demo
            if(ValidateRoomData(room))
            {
                _context.Add(room);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(RoomList));
            }

            
            return View("RoomAdd", room);
        }
    }
}