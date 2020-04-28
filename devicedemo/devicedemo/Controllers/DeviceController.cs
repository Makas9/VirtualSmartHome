using devicedemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace devicedemo.Controllers
{
    [Route("api/device")]
    public class DeviceController : ApiController
    {
        Device _device = new Device
        {
            Name = "oro kondicionierius",
            Model = "model1",
            Value = 0,
            State = Device.DeviceState.On,
            Type = Device.DeviceType.Air_Conditioner
        };
        public DeviceController()
        {
            
        }
        // GET api/values
        public Device GetI()
        {
            return _device;
        }

        // keisti state
        public void Post([FromBody]int state)
        {
            _device.State = (Device.DeviceState)state;
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
