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
    public class DefaultController : Controller
    {

        public const string _Username = "null";
        public const string _UserID = "null";

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(UserModel User)
        {
            string Username = User.Username;
            string Password = User.Password;

            // Check if data is correct, then set session and redirect TODO
            bool loggedIn = true;
            if (loggedIn)
            {
                HttpContext.Session.SetInt32(_UserID, 10); // Save UserID in session TODO

                return Redirect("../LoggedIn/Index");
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}