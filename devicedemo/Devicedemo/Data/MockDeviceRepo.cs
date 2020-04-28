using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Devicedemo.Models;

namespace Devicedemo.Data
{
    public class MockDeviceRepo : IDeviceRepo
    {
        private Device _device;

        public MockDeviceRepo()
        {
            _device = new Device
            {
                Name = "oro kondicionierius",
                Model = "model1",
                Value = 0,
                State = Device.DeviceState.Off,
                Type = Device.DeviceType.Air_Conditioner
            };
        }

        public Device GetDevice()
        {
            return _device;
        }

        public void UpdateDevice(Device device)
        {
            _device = device;
        }
    }
}