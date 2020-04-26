using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHome.Models
{
    public class SmartHomeDbContext : IdentityDbContext
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<House> Houses { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Scenario> Scenarios { get; set; }
        public DbSet<UserDevice> UserDevices { get; set; }

        public SmartHomeDbContext(DbContextOptions<SmartHomeDbContext> options)
            : base(options)
        {
            
        }

        
    }
}
