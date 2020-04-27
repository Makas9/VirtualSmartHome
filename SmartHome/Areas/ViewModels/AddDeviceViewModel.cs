using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHome.ViewModels
{
    public class AddDeviceViewModel
    {
        [Required]
        public string IpAddress { get; set; }
        [Required]
        public int Port { get; set; }
        [Required]
        public int RoomID { get; set; }
    }
}
