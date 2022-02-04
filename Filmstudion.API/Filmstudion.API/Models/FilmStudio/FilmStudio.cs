using System.Collections.Generic;
using Filmstudion.API.Models.Film;
using Microsoft.AspNetCore.Identity;

namespace Filmstudion.API.Models.FilmStudio
{
    public class FilmStudio: IdentityUser,IFilmStudio
    {
        public int FilmStudioId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public List<IFilm> RentedFilmCopies { get; set; }
    }
}
