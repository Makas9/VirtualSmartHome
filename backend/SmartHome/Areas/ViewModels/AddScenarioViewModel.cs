using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHome.Areas.ViewModels
{
    public class AddScenarioViewModel
    {
        [Required]
        public DateTime TimeOfEvent { get; set; }
        [Required]
        public string EventURL { get; set; }
        [Required]
        public int DeviceId { get; set; }
        [Required]
        public int RoomID { get; set; }
    }
}
