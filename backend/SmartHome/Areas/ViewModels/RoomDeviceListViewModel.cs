using SmartHome.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHome.ViewModels
{
    public class RoomDeviceListViewModel
    {
        public IEnumerable<Models.Device> Devices { get; set; }
        public int roomID { get; set; }
    }
}
