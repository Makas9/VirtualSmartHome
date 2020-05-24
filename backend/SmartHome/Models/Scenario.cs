using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHome.Models
{
    public class Scenario
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Time of scenario")]
        [DataType(DataType.Date)]
        public DateTime TimeOfEvent { get; set; }
        public string EventURL { get; set; }
        [Required]
        public int DeviceId { get; set; }
        public Device Device { get; set; }

        public static Scenario GetScenario(SmartHomeDbContext context, int id)
        {
            var scenes = context.Scenarios.Where(s => s.Id == id).ToList();
            if (scenes.Count == 0)
                return null;
            return scenes[0];
        }

        public static void AddScenario(SmartHomeDbContext context, Models.Scenario scenario)
        {
            context.Add(scenario);
        }
    }
}
