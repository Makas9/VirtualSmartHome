using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SmartHome.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Room Name")]
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        public int HouseId { get; set; }
        public House House { get; set; }
        [InverseProperty("Room")]
        public ICollection<Device> Devices { get; set; }

        public static void AddRoom(SmartHomeDbContext context, Models.Room roomData)
        {
            context.Add(roomData);
        }

        public static List<Models.Room> Select(SmartHomeDbContext context)
        {
            return context.Rooms.Include(r => r.House).ToList();
        }
    }
}
