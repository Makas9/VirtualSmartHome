﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHome.Models
{
    public class House
    {
#nullable enable
        [Key]
        public int Id { get; set; }
        [StringLength(100, MinimumLength = 1)]
        public string? Address { get; set; }
        [Required]
        [ForeignKey("Owner")]
        public int OwnerId { get; set; }
        public UserModel Owner { get; set; }

        public ICollection<Room> Rooms { get; set; }
        [InverseProperty("Residence")]
        public ICollection<UserModel> Residents { get; set; }
    }
}
