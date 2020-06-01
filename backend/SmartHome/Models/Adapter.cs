using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SmartHome.Models
{
    public class Adapter : IFeature
    {
        API api;

        public Adapter()
        {
            api = new API();
        }

        public HttpResponseMessage TurnOff(Device device, HttpClient httpClient)
        {
            device.State = DeviceState.Off;
            var json = JsonConvert.SerializeObject(device);

            return api.SetData(httpClient, UrlBuilder(device.IpAddress, device.Port), json);
        }

        public HttpResponseMessage TurnOn(Device device, HttpClient httpClient)
        {
            device.State = DeviceState.On;
            var json = JsonConvert.SerializeObject(device);

            return api.SetData(httpClient, UrlBuilder(device.IpAddress, device.Port), json);
        }

        public string UrlBuilder(string address, int port)
        {
            return "https://" + address + ":" + port.ToString(); ;
        }
    }
}
