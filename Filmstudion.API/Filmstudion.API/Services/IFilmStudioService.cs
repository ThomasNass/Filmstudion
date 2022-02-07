using Filmstudion.API.Models.FilmStudioDir;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Filmstudion.API.Services
{
    public class IFilmStudioService
    {
        FilmStudio CreateFilmStudio(FilmStudio filmstudio)
        {
            return filmstudio;
        }

        public Task<IEnumerable<FilmStudio>> GetAllFilmStudios { get; }
       
    }
}
