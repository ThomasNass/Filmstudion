using Filmstudion.API.Models.FilmStudioDir;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Filmstudion.API.Models.User
{
    public interface IUser
    {
        [Key]
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public bool IsAdmin { get; set; }
        public string FilmStudioId { get; set; }
        public FilmStudio FilmStudio { get; set; }
        
        public string Password { get; set; }
        
    }
}
