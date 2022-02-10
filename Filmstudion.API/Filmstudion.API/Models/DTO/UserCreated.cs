using Filmstudion.API.Models.FilmStudioDir;
using Filmstudion.API.Models.User;
using Newtonsoft.Json;

namespace Filmstudion.API.Models.DTO
{
    public class UserCreated:IUser
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        [JsonIgnore]
        public bool IsAdmin { get; set; }
        [JsonIgnore]
        public int FilmStudioId { get; set; }
        [JsonIgnore]
        public FilmStudio FilmStudio { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
       
      
    }
}
