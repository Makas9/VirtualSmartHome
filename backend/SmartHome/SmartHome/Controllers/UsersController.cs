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
    public class UsersController : Controller
    {

        public const string _Username = "null";
        public const string _UserID = "null";

        public ActionResult UserLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UserLogin(UserModel User)
        {
            string Username = User.Username;
            string Password = User.Password;

            // Check if data is correct, then set session and redirect TODO
            bool loggedIn = true;
            if (loggedIn)
            {
                HttpContext.Session.SetInt32(_UserID, 10); // Save UserID in session TODO

                return View("Home");
            }

            return View();
        }

        public ActionResult Home()
        {
            if (HttpContext.Session.GetInt32(UsersController._UserID) < 0) return Redirect("../User/UserLogin");

            return View();
        }

        public ActionResult Logout()
        {
            HttpContext.Session.SetInt32(UsersController._UserID, -1);

            return Redirect("../User/UserLogin");
        }

        public ActionResult UserAssignDevices()
        {
            if (HttpContext.Session.GetInt32(UsersController._UserID) < 0) return Redirect("../User/UserLogin");

            return View();
        }

        public ActionResult UserAdd()
        {
            if (HttpContext.Session.GetInt32(UsersController._UserID) < 0) return Redirect("../User/UserLogin");

            return View();
        }

        public ActionResult UserList()
        {
            if (HttpContext.Session.GetInt32(UsersController._UserID) < 0) return Redirect("../User/UserLogin");

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}