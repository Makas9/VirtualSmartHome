using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SmartHome.Models
{
    public class Adapter : API, IFeature
    {

        public override Task<string> GetData(HttpClient httpClient, string endpoint)
        {
            return base.GetData(httpClient, endpoint);
        }

        public override HttpResponseMessage SetData(HttpClient httpClient, string AddressUrl, string deviceJSON)
        {
            return base.SetData(httpClient, null, deviceJSON);
        }

        public HttpResponseMessage TurnOff(Device device, HttpClient httpClient)
        {
            device.State = DeviceState.Off;
            var json = JsonConvert.SerializeObject(device);

            return SetData(httpClient, UrlBuilder(device.IpAddress, device.Port), json);
        }

        public HttpResponseMessage TurnOn(Device device, HttpClient httpClient)
        {
            device.State = DeviceState.On;
            var json = JsonConvert.SerializeObject(device);

            return SetData(httpClient, UrlBuilder(device.IpAddress, device.Port), json);
        }

        public string UrlBuilder(string address, int port)
        {
            return "https://" + address + ":" + port.ToString(); ;
        }
    }
}
