using Filmstudion.API.Models.Film;
using System.Collections.Generic;

namespace Filmstudion.API.Models.DTO
{
    public class FilmCopies
    {
        public List<FilmCopy> RentedFilmCopies { get; set; }
    }
}
