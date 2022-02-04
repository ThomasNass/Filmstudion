using Filmstudion.API.Models.FilmStudio;
using Filmstudion.API.Persistence.Repositories;

namespace Filmstudion.API.Services
{
    public class FilmStudioService
    {
        private readonly FilmStudioRepository _filmStudioRepository;

        public FilmStudioService(FilmStudioRepository filmStudioRepository)
        {
            _filmStudioRepository = filmStudioRepository;
        }

        public FilmStudio Create(FilmStudio filmstudio)
        {
            _filmStudioRepository.Create(filmstudio);

            return filmstudio;
          
        }
    }
}
