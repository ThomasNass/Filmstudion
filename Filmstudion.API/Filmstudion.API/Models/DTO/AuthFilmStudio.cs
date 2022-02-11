using Filmstudion.API.Models.Film;
using Filmstudion.API.Models.FilmStudioDir;
using System.Collections.Generic;

namespace Filmstudion.API.Models.DTO
{
    public class AuthFilmStudio:IFilmStudio
    {
        public string FilmStudioId { get; set; }
        public string FilmStudioName { get; set; }
        public string FilmStudioCity { get; set; }
        public List<FilmCopy> RentedFilmCopies { get; set; }      

    }
}
