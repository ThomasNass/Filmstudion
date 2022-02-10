using Filmstudion.API.Models.Film;
using Filmstudion.API.Models.FilmStudioDir;

using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Filmstudion.API.Models.DTO
{
    public class FilmStudioCreated : IFilmStudio
    {
        public int FilmStudioId { get; set; }
        public string FilmStudioName { get; set; }
        public string FilmStudioCity { get; set; }
        public List<FilmCopy> RentedFilmCopies { get; set; }
    }
}
