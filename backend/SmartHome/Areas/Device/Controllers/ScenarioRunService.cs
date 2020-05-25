using Microsoft.EntityFrameworkCore;
using SmartHome.Device.Controllers;
using SmartHome.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHome.Areas.Device.Controllers
{
    public class ScenarioRunService : IScenarioRunService
    {
        SmartHomeDbContext _context;
        public ScenarioRunService(SmartHomeDbContext context)
        {
            _context = context;
        }

        public void IterateScenarios()
        {
            var deviceController = new DeviceController(_context);
            DateTime currentTime = DateTime.Now;
            List<Models.Scenario> scenarios = _context.Scenarios.ToList();

            foreach (var scenario in scenarios)
            {
                if (DateTime.Compare(scenario.TimeOfEvent, DateTime.Now) < 0)
                {
                    deviceController.ExecuteScene(scenario.Id);
                }
            }
        }

    }
}
