using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Models
{
    public class API
    {
        public HttpResponseMessage SetState(HttpClient httpClient, string url, string deviceJSON)
        {
            var response = httpClient.PostAsync(url + "/api/device", new StringContent(deviceJSON, Encoding.UTF8, "application/json"));
            Console.WriteLine(response);

            return response.Result;
        }
    }
}
