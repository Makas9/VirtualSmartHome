using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Models
{
    public class API : IAPI
    {

        public virtual async Task<String> GetData(HttpClient httpClient, string endpoint)
        {
            string response = await httpClient.GetStringAsync("api/device");

            return response;
        }

        public virtual HttpResponseMessage SetData(HttpClient httpClient, string AddressUrl, string deviceJSON)
        {
            var response = httpClient.PostAsync(AddressUrl + "/api/device", new StringContent(deviceJSON, Encoding.UTF8, "application/json"));
            Console.WriteLine(response);

            return response.Result;
        }
    }
}
