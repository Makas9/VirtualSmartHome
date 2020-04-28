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
        //[RegularExpression(@"^(?:[0-9]{1,3}\.){3}[0-9]{1,3}$")]
        [StringLength(15, MinimumLength = 7)]
        public string IpAddress { get; set; }
        [Required]
        public int Port { get; set; }
        [Required]
        public int RoomID { get; set; }
    }
}
