using Filmstudion.API.Models.User;
using Filmstudion.API.Persistence.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace Filmstudion.API.Services
{
    public class UserService//:IUserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetByName(string name)
        {
            var users = await _userRepository.ListAsync();
            var user = users.FirstOrDefault(n=> n.UserName == name);

            return user;
        }
    }
}
