using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace SmartHome.Models
{
    public class UserModel
    {
#nullable enable
        [Key]
        public int Id { get; set; }
        [Required]
        [BindProperty]
        public string Username { get; set; }
        [Required]
        [BindProperty]
        public string Password { get; set; }
        public UserType Type { get; set; }


        public int? ResidenceId { get; set; }
        public House Residence { get; set; }

        [InverseProperty("Owner")]
        public ICollection<House> Houses { get; set; }
        public ICollection<UserDevice> Devices { get; set; }
    }

    public enum UserType
    {
        Owner, Resident
    }
}
