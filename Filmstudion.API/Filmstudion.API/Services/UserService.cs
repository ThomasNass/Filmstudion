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

        public object CreateUser(User user)
        {
            if (user.IsAdmin == true) {
                user.Role = "admin";
            _userRepository.Create(user);
                

            var newUser = new {id= user.UserId,role = user.Role, name = user.UserName };
                return newUser;
            }
            
            return null;
        }

        public async Task<User> Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;
            var users = await _userRepository.ListAsync();
            var user = users.Where(u => u.UserName == username && u.Password == password).FirstOrDefault();
           
            if (user == null) return null;
            return user;


        }
    }
}
