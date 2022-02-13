using Filmstudion.API.Models.FilmStudioDir;
using Filmstudion.API.Models.User;
using Filmstudion.API.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Filmstudion.API.Services
{
    public class FilmStudioService:IFilmStudioService
    {
        private readonly IFilmStudioRepository _filmStudioRepository;
        private readonly IFilmRepository _filmRepository;

        public FilmStudioService(IFilmStudioRepository filmStudioRepository, IFilmRepository filmRepository)
        {
            _filmStudioRepository = filmStudioRepository;
            _filmRepository = filmRepository;
        }

        public FilmStudio CreateFilmStudio(FilmStudio filmStudio)
        {
            filmStudio.FilmStudioId = filmStudio.FilmStudioName;
            _filmStudioRepository.Create(filmStudio);
           
            return filmStudio;
          
        }
        public async Task<IEnumerable<FilmStudio>> GetAllFilmStudios()
        {
            var filmStudios = await _filmStudioRepository.ListAsync();
            var allFilmCopies = await _filmRepository.GetFilmCopies();//Samma här som nedanför, det sker en automatisk mappning av att de hämtas in i samma metod. Blir dubletter om jag försöker sammanföra dem själv
            return filmStudios;
        }
        public async Task<FilmStudio> GetFilmStudio(string filmStudioId)
        {
            var filmStudios = await _filmStudioRepository.ListAsync();
            var filmStudio = filmStudios.FirstOrDefault(x => x.FilmStudioId == filmStudioId);
            var allFilmCopies = await _filmRepository.GetFilmCopies();
            //var filmCopies = allFilmCopies.Where(f => f.FilmStudioId == filmStudio.FilmStudioId).ToList();//Här sker något teknomagiskt som gör att filmcopies blir mappade till studion
           /* if(filmCopies != null)//Blev därför tvungen att ta bort denna loopen då det blev dubbletter annars
            {
                foreach(var filmCopy in filmCopies)
                {
                    filmStudio.RentedFilmCopies.Add(filmCopy);
                }
            }*/
            return filmStudio;
        }

    }
}
