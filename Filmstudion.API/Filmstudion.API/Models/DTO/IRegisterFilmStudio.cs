using Filmstudion.API.Models.Film;
using System.Collections.Generic;

namespace Filmstudion.API.Models.DTO
{
    public interface IRegisterFilmStudio
    {
        public string FilmStudioName { get; set; }
        public string FilmStudioCity { get; set; }
        public string Password { get; set; }
        public List<FilmCopy> RentedFilmCopies { get; set; }
    }
}
