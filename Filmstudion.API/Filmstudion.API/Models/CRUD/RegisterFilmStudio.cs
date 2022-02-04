namespace Filmstudion.API.Models.CRUD
{
    public class RegisterFilmStudio:IRegisterFilmStudio
    {
        public string Name { get; set; }
        public string City { get; set; }
        public string Password { get; set; }
    }
}
