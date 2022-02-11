using Filmstudion.API.Models.Film;
using System.Collections.Generic;

namespace Filmstudion.API.Models.FilmStudioDir
{
    public interface IFilmStudio
    {
        public string FilmStudioId { get; set; }
        public string FilmStudioName { get; set; }
        public string FilmStudioCity { get; set; }
        public List<FilmCopy> RentedFilmCopies { get; set; }
    }
}
