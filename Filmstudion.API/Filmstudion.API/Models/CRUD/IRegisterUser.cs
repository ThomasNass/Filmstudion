namespace Filmstudion.API.Models.CRUD
{
    public interface IRegisterUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}
