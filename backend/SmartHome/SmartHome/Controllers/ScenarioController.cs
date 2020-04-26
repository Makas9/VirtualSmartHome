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
    public class ScenarioController : Controller
    {
        public ActionResult AddScenario()
        {
            if (HttpContext.Session.GetInt32(UserController._UserID) < 0) return Redirect("../User/UserLogin");

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