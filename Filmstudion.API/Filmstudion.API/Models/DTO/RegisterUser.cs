using System.ComponentModel.DataAnnotations;

namespace Filmstudion.API.Models.DTO
{
    public class RegisterUser:IRegisterUser
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public bool IsAdmin { get; set; }
    }
}
