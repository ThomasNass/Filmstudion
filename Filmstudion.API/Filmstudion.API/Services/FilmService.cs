using Filmstudion.API.Models.Film;
using Filmstudion.API.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Filmstudion.API.Services
{
    public class FilmService
    {
        private readonly FilmRepository _filmRepository;

        public FilmService(FilmRepository filmRepository)
        {
           _filmRepository = filmRepository;
        }

        public Film CreateFilm(Film film, int copies)
        {
            for(int i = 0; i< copies; i++) 
            {
                var filmCopy = new FilmCopy
                {
                    FilmCopyId = i,
                    FilmId = film.FilmId
                };
                film.FilmCopies.Add(filmCopy);
            }
          _filmRepository.Create(film);
            
            return film;
        }   
        public async Task<IEnumerable<Film>> GetFilmsAsync()
        {
            return await _filmRepository.ListAsync();
        }

        public async Task<Film> UpdateFilm(int filmId)
        {
           var films = await _filmRepository.ListAsync();
           var film = films.FirstOrDefault(x => x.FilmId == filmId);
            return film;
        }
    }
}
