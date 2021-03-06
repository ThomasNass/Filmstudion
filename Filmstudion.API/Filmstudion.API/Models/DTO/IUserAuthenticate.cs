using System.ComponentModel.DataAnnotations;

namespace Filmstudion.API.Models.DTO
{
    public interface IUserAuthenticate
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
