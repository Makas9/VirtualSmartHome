using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHome.Models
{
    public class Scenario
    {
        public int Id { get; set; }
        public DateTimeOffset TimeOfEvent { get; set; }
        public string EventURL { get; set; }
        public int DeviceId { get; set; }
        public Device Device { get; set; }
    }
}
