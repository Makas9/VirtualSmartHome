using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Devicedemo.Models
{
    public class Device
    {
        public string Name { get; set; }
        public string Model { get; set; }
        public double Value { get; set; }
        public DeviceType Type { get; set; }
        public DeviceState State { get; set; }

    public enum DeviceType
    {
        Lamp, Door, Air_Conditioner, Blinds
    }

    public enum DeviceState
    {
        Off, On, Broken
    }
}
}