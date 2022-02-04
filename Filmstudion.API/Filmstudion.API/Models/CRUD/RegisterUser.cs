namespace Filmstudion.API.Models.CRUD
{
    public class RegisterUser:IRegisterUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}
