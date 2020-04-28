using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartHome.Models;
using SmartHome.Resident.Controllers;

namespace SmartHome.Device.Controllers
{

    public class ScenarioController : Controller
    {
        private const string _ControllerPath = "device/device/";
        public ActionResult AddScenario()
        {
            if (HttpContext.Session.GetInt32(UserController._UserID) < 0) return Redirect(UserController._LoginPath);

            return View();
        }

        public void CreateScenario(Scenario scenarioData)
        {
            // TODO
        }

        public void ValidateScenarioData(Scenario scenarioData)
        {
            // TODO
        }
    }
}