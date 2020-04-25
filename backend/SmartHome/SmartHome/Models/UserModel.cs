using System;
using Microsoft.AspNetCore.Mvc;

namespace SmartHome.Models
{
    public class UserModel
    {
        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Password { get; set; }
    }
}
