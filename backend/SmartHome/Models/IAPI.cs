using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SmartHome.Models
{
    interface IAPI
    {
        Task<String> GetData(HttpClient httpClient, string endpoint);
        HttpResponseMessage SetData(HttpClient httpClient, string endpoint, string deviceJSON);
    }
}
