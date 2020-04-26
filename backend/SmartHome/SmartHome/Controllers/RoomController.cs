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
            if (!TryValidateModel(roomData, nameof(roomData)))
            {
                return false;
            }

            return true;
        }

        [HttpPost]
        public async Task<ActionResult> CreateRoom([Bind("Id,Title,ImagePath,Points,DateCreated,FkCategory")] Room room)
        {
            if (HttpContext.Session.GetInt32(UserController._UserID) < 0) return Redirect("../User/UserLogin");

            if(!ValidateRoomData(room))
            {
                return View("RoomAdd", room);
            }

            _context.Add(room);
                
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(RoomList));
        }
    }
}