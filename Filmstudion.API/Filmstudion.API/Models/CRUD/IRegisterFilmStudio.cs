namespace Filmstudion.API.Models.CRUD
{
    public interface IRegisterFilmStudio
    {
        public string FilmStudioName { get; set; }
        public string FilmStudioCity { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
    }
}
