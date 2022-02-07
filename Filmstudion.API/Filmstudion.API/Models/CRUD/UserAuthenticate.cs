using System.ComponentModel.DataAnnotations;

namespace Filmstudion.API.Models.CRUD
{
    public class UserAuthenticate
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
