using Filmstudion.API.Models.FilmStudioDir;
using Filmstudion.API.Models.User;
using Filmstudion.API.Persistence.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Filmstudion.API.Services
{
    public class FilmStudioService//:IFilmStudioService
    {
        private readonly FilmStudioRepository _filmStudioRepository;
        private readonly UserRepository _userRepository;

        public FilmStudioService(FilmStudioRepository filmStudioRepository, UserRepository userRepository)
        {
            _filmStudioRepository = filmStudioRepository;
            _userRepository = userRepository;
        }

        public FilmStudio CreateFilmStudio(FilmStudio filmStudio, User user)
        {
            _filmStudioRepository.Create(filmStudio);
            user.Role = "filmstudio";
            user.FilmStudio = filmStudio;
            user.FilmStudioId = filmStudio.FilmStudioId;
            _userRepository.Create(user);

            return filmStudio;
          
        }
        public async Task<IEnumerable<FilmStudio>> GetAllFilmStudios()
        {
            var filmStudios = await _filmStudioRepository.ListAsync();
            return filmStudios;
        }
    }
}
