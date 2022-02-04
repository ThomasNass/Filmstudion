namespace Filmstudion.API.Models.CRUD
{
    public interface IRegisterFilmStudio
    {
        public string Name { get; set; }
        public string City { get; set; }
        public string Password { get; set; }
    }
}
