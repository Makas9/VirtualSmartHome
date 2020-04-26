using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHome.Models
{
    public class Device
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public double Value { get; set; }
        public DeviceType Type { get; set; }
        public DeviceState State { get; set; }
        [Required]
        public int RoomId { get; set; }
        public Room Room { get; set; }
        [InverseProperty("Device")]
        public ICollection<Scenario> Scenarios { get; set; }
        public ICollection<UserDevice> Users { get; set; }
    }

    public enum DeviceType
    {
        Lamp, Door, Air_Conditioner, Blinds
    }

    public enum DeviceState
    {
        Off, On, Broken
    }
}
