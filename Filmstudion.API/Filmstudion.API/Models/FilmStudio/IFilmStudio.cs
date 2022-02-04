using Filmstudion.API.Models.Film;
using System.Collections.Generic;

namespace Filmstudion.API.Models.FilmStudio
{
    public interface IFilmStudio
    {
        public int FilmStudioId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        //public List<IFilm> RentedFilmCopies { get; set; }
    }
}
