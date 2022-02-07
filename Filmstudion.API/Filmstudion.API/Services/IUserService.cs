using Filmstudion.API.Models.User;

namespace Filmstudion.API.Services
{
    public interface IUserService
    {
        User CreateUser(User user)
        {
            return user;
        }
    }
}
