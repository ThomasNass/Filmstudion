﻿using Filmstudion.API.Models.FilmStudioDir;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Filmstudion.API.Models.User
{
    public class User: IdentityUser, IUser
    {
        public int UserId { get; set; }
        public string Role { get; set; }
        public bool IsAdmin { get; set; }
        public int FilmStudioId { get; set; }
        public FilmStudio FilmStudio { get ; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}
