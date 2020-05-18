using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SmartHome.Models
{
    public class Device
    {
#nullable enable
        [JsonIgnore]
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Model { get; set; }
        public double? Value { get; set; }
        public DeviceType? Type { get; set; }
        public DeviceState? State { get; set; }
        [JsonIgnore]
        [Required]
        [RegularExpression(@"^(?:[0-9]{1,3}\.){3}[0-9]{1,3}$")]
        [StringLength(15, MinimumLength = 7)]
        public string  IpAddress { get; set; }
        [JsonIgnore]
        [Required]
        public int Port { get; set; }
        [JsonIgnore]
        [Required]
        public int RoomId { get; set; }
        [JsonIgnore]
        public Room Room { get; set; }
        [JsonIgnore]
        [InverseProperty("Device")]
        public ICollection<Scenario> Scenarios { get; set; }
        [JsonIgnore]
        public ICollection<UserDevice> Users { get; set; }

        public static List<Models.Device> SelectBelongingToRoom(SmartHomeDbContext context, int roomId)
        {
            return context.Devices.Where(r => r.RoomId == roomId).ToList();
        }

        public static void AddDevice(SmartHomeDbContext context, Models.Device device)
        {
            context.Add(device);
        }
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
