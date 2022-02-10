using Filmstudion.API.Models.FilmStudioDir;
using Filmstudion.API.Models.User;
using Filmstudion.API.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public FilmStudio CreateFilmStudio(FilmStudio filmStudio)
        {
            
            _filmStudioRepository.Create(filmStudio);
           
            return filmStudio;
          
        }
        public async Task<IEnumerable<FilmStudio>> GetAllFilmStudios()
        {
            var filmStudios = await _filmStudioRepository.ListAsync();
            return filmStudios;
        }
        public async Task<FilmStudio> GetFilmStudio(int filmStudioId)
        {
            var filmStudios = await _filmStudioRepository.ListAsync();
            var filmStudio = filmStudios.FirstOrDefault(x => x.FilmStudioId == filmStudioId);
            return filmStudio;
        }

    }
}
