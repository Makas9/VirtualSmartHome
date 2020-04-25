using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHome.Models
{
    public class UserDevice
    {
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public UserModel User { get; set; }
        [ForeignKey("Device")]
        public int DeviceId { get; set; }
        public Device Device { get; set; }
    }
}
