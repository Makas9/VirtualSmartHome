using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace devicedemo.Models
{
    public class MockDevice : IDeviceRepository
    {
        private Device _device;

        public MockDevice()
        {
            _device = new Device
            {
                Name = "oro kondicionierius",
                Model = "model1",
                Value = 0,
                State = Device.DeviceState.On,
                Type = Device.DeviceType.Air_Conditioner
            };
        }

        public Device GetDevice()
        {
            return _device;
        }
    }
}