using Filmstudion.API.Models.FilmStudioDir;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Filmstudion.API.Persistence.Repositories
{
    public interface IFilmStudioRepository
    {
        public Task<IEnumerable<FilmStudio>> ListAsync();
        

        void Create(FilmStudio filmStudio);
        
    }
}
