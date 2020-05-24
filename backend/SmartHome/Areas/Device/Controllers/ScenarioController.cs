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
            if (HttpContext.Session.GetInt32(UserController._UserID) < 0) return;

            if (ValidateScenarioData(scenarioData))
            {
                Models.Scenario scenario = new Models.Scenario
                {
                    TimeOfEvent = scenarioData.TimeOfEvent,
                    EventURL = scenarioData.EventURL,
                    DeviceId = scenarioData.DeviceId
                };

                // TODO
            }
        }

        public bool ValidateScenarioData(Scenario scenarioData)
        {
            if (!ModelState.IsValid)
                return false;

            string scenario;
            try
            {
                using (var wc = new System.Net.WebClient())
                    scenario = wc.DownloadString(scenarioData.EventURL);
            }
            catch
            {
                return false;
            }
            string[] lines = scenario.Split(new char[] { '\n', '\r', '\t', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (lines.Length < 2 || !lines[0].Equals("SHScript"))
                return false;
            
            return true;
        }
    }
}