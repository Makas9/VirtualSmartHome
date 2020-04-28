using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Devicedemo.Models;

namespace Devicedemo.Data
{
    public interface IDeviceRepo
    {
        Device GetDevice();
        void UpdateDevice(Device device);
    }
}