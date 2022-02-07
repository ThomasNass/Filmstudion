namespace Filmstudion.API.Models.CRUD
{
    public class RegisterFilmStudio:IRegisterFilmStudio
    {
        public string Password { get; set; }
        public string FilmStudioName { get; set; }
        public string FilmStudioCity { get; set; }
        public string UserName { get; set; }
    }
}
