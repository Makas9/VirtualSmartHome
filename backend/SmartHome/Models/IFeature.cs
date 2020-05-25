using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHome.Models
{
    interface IFeature
    {
        public void TurnOn();
        public void turnOff();
    }
}
