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
        public const string _Role = "null";

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(UserModel User)
        {
            string Username = User.Username;
            string Password = User.Password;

            bool loggedIn = true;
            if (loggedIn)
            {
                HttpContext.Session.SetString(_Username, "Username");
                HttpContext.Session.SetString(_Role, "Roles");
                return View("../LoggedIn/Index");
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