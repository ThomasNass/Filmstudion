using Filmstudion.API.Models.User;
using Filmstudion.API.Persistence.Repositories;

namespace Filmstudion.API.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User Create(User user)
        {
            _userRepository.Create(user);

            return user;

        }
    }
}
