using Filmstudion.API.Models.Film;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Filmstudion.API.Persistence.Repositories
{
    public interface IFilmRepository
    {
        public Task<IEnumerable<Film>> ListAsync();


        void Create(Film film);


       void Update(Film film);


        void CreateCopy(FilmCopy filmCopy);


        public Task<IEnumerable<FilmCopy>> GetFilmCopies();


        public Task DeleteFilmCopy(int filmId);


        void UpdateFilmCopy(FilmCopy filmCopy);
        
    }
}
