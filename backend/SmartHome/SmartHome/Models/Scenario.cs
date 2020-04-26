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
        public DateTimeOffset TimeOfEvent { get; set; }
        public string EventURL { get; set; }
        [Required]
        public int DeviceId { get; set; }
        public Device Device { get; set; }
    }
}
