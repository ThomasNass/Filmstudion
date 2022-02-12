using Filmstudion.API.Models.DTO;
using Filmstudion.API.Models.Film;
using Filmstudion.API.Models.FilmStudioDir;
using Filmstudion.API.Persistence.Repositories;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Filmstudion.API.Services
{
    public class FilmService
    {
        private readonly FilmRepository _filmRepository;
        private readonly FilmStudioRepository _filmStudioRepository;

        public FilmService(FilmRepository filmRepository, FilmStudioRepository filmStudioRepository)
        {
           _filmRepository = filmRepository;
            _filmStudioRepository = filmStudioRepository;
        }

        public Film CreateFilm(Film film)
        {
            
          _filmRepository.Create(film);
            
            return film;
        }   
        public async Task<IEnumerable<Film>> GetFilmsAsync()
        {
             var films = await _filmRepository.ListAsync();
            var filmCopies = await _filmRepository.GetFilmCopies();
            return films;
        }

        public async Task<Film> UpdateFilm(int filmId, JsonPatchDocument<Film> patchEntity)
        {
           var films = await _filmRepository.ListAsync();
           var film = films.FirstOrDefault(x => x.FilmId == filmId);
            if(film == null)
            {
                return null;
            }
            patchEntity.ApplyTo(film);
           _filmRepository.Update(film);
            return film;
        }

        public async Task<IEnumerable<FilmCopy>> GetFilmCopies()
        {
            return await _filmRepository.GetFilmCopies();
        }

        public void AddCopies(int filmId, int copies)
        {
            for (int i = 0; i < copies; i++)
            {
                var filmCopy = new FilmCopy
                {
                    FilmId = filmId,
                    RentedOut = false 
                };
                _filmRepository.CreateCopy(filmCopy);           
            }
        }

        public async Task ChangeFilmCopies(int filmId, int desiredNumberOfCopies)
        {
            var allFilmCopies = await _filmRepository.GetFilmCopies();
            var idFilmCopies = allFilmCopies.Where(x => x.FilmId == filmId).Count();
            if(idFilmCopies > desiredNumberOfCopies)
            {
                var difference = idFilmCopies - desiredNumberOfCopies;
                for(int i = 0; i < difference; i++)
                {
                    await _filmRepository.DeleteFilmCopy(filmId);
                }
            }
            else
            {
                var difference = desiredNumberOfCopies - idFilmCopies;
                for(int i = 0;i < difference; i++)
                {
                    var filmCopy = new FilmCopy { FilmId = filmId };
                     _filmRepository.CreateCopy(filmCopy);
                }
            }
        }

        public async Task RentFilm(int id, string studioId)
        { 
            var filmCopies = await _filmRepository.GetFilmCopies();
            var filmStudios = await _filmStudioRepository.ListAsync();
            var filmCopy = filmCopies.FirstOrDefault(x => x.FilmId == id && x.RentedOut == false); 
            var filmStudio = filmStudios.FirstOrDefault(x => x.FilmStudioId == studioId);
            filmCopy.FilmStudioId = filmStudio.FilmStudioId;
            filmCopy.RentedOut = true;
            _filmRepository.UpdateFilmCopy(filmCopy);
            
       }

        public async Task<Film> GetFilm(int filmId)
        {
            var films = await _filmRepository.ListAsync();
            var film = films.FirstOrDefault(f => f.FilmId == filmId);
            var filmCopies = _filmRepository.GetFilmCopies();//Detta anrop automappar filmcopies till film
            return film;
        }

        public async Task<bool> ReturnFilm(int id, string studioId)
        {
            var filmCopies = await _filmRepository.GetFilmCopies();
            var copy = filmCopies.FirstOrDefault(x => x.FilmId == id && x.RentedOut == true&&x.FilmStudioId == studioId);
            if (copy == null) return false;
            copy.FilmStudioId = null;
            copy.RentedOut = false;
            _filmRepository.UpdateFilmCopy(copy);
            return true;
        }
    }
}
