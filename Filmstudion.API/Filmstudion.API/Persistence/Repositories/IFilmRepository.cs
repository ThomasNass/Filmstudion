using Filmstudion.API.Models.Film;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Filmstudion.API.Persistence.Repositories
{
    public interface IFilmRepository
    {
        Task<IEnumerable<Film>> ListAsync();
    }
}
