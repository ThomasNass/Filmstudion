using Filmstudion.API.Models.FilmStudioDir;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Filmstudion.API.Services
{
    public interface IFilmStudioService
    {
        public FilmStudio CreateFilmStudio(FilmStudio filmStudio);
      
        
        public Task<IEnumerable<FilmStudio>> GetAllFilmStudios();
        public Task <FilmStudio> GetFilmStudio(string id);
        
    }
}
