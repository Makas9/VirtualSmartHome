﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHome.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int HouseId { get; set; }
        public House House { get; set; }
        [InverseProperty("Room")]
        public ICollection<Device> Devices { get; set; }
    }
}