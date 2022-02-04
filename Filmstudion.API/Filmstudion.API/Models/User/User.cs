using Microsoft.AspNetCore.Identity;

namespace Filmstudion.API.Models.User
{
    public class User: IdentityUser, IUser
    {
        public int UserId { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}
