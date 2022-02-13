using Filmstudion.API.Models.Film;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Filmstudion.API.Services
{
    public interface IFilmService
    {
        public Film CreateFilm(Film film);
        
        public Task<IEnumerable<Film>> GetFilmsAsync();


        public Task<Film> UpdateFilm(int filmId, JsonPatchDocument<Film> patchEntity);


        public Task<IEnumerable<FilmCopy>> GetFilmCopies();


        void AddCopies(int filmId, int copies);


       public Task ChangeFilmCopies(int filmId, int desiredNumberOfCopies);


        public Task RentFilm(int id, string studioId);


        public Task<Film> GetFilm(int filmId);


        public Task<bool> ReturnFilm(int id, string studioId);
        
    }
}
