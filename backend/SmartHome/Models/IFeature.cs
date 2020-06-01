using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SmartHome.Models
{
    interface IFeature
    {
        HttpResponseMessage TurnOn(Device device, HttpClient httpClient);

        HttpResponseMessage TurnOff(Device device, HttpClient httpClient);
    }
}
