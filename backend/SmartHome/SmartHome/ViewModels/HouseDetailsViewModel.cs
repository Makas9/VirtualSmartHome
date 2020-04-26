using SmartHome.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHome.ViewModels
{
    public class HouseDetailsViewModel
    {
        public IEnumerable<Room> Rooms { get; set; }
        public House House { get; set; }
    }
}
