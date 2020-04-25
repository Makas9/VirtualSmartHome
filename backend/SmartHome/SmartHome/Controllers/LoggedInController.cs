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
    public class LoggedInController : DefaultController
    {

        public ActionResult Index()
        {
            //ViewBag.Username = HttpContext.Session.GetString(_Username);
            ViewData["test"] = "Hello";

            return View(ViewData);
        }
    }
}
