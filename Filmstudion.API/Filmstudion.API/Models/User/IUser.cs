using Filmstudion.API.Models.FilmStudioDir;

namespace Filmstudion.API.Models.User
{
    public interface IUser
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public bool IsAdmin { get; set; }
        public int FilmStudioId { get; set; }
        public FilmStudio FilmStudio { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}
