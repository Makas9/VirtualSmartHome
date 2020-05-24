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

    [Area("Device")]
    public class ScenarioController : Controller
    {
        private readonly SmartHomeDbContext _context;
        private const string _ControllerPath = "device/scenario/";
        private const string _ViewPath = "../";

        public ScenarioController(SmartHomeDbContext context)
        {
            _context = context;
        }


        [HttpGet(_ControllerPath + "AddScenario/{roomID}")]
        public ActionResult AddScenario(int? roomID)
        {
            if (HttpContext.Session.GetInt32(UserController._UserID) < 0) return Redirect(UserController._LoginPath);

            if (roomID == null)
            {
                return NotFound();
            }

            return View(_ViewPath + "AddScenario");
        }

        [HttpPost]
        public async Task<IActionResult> AddScenario(Scenario scenarioData, int roomID)
        {
            if (HttpContext.Session.GetInt32(UserController._UserID) < 0) return Redirect(UserController._LoginPath);

            if (ValidateScenarioData(scenarioData))
            {
                Models.Scenario scenario = new Models.Scenario
                {
                    TimeOfEvent = scenarioData.TimeOfEvent,
                    EventURL = scenarioData.EventURL,
                    DeviceId = scenarioData.DeviceId
                };

                Models.Scenario.AddScenario(_context, scenario);
                await _context.SaveChangesAsync();

                //return RedirectToAction(nameof(Controllers.DeviceController.OpenRoomDeviceList), new { roomID = roomID });
            }

            return View(_ViewPath + "AddScenario");
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