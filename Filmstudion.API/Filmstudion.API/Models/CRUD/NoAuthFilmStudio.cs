using Filmstudion.API.Models.Film;
using Filmstudion.API.Models.FilmStudioDir;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Filmstudion.API.Models.CRUD
{
    public class NoAuthFilmStudio : IFilmStudio
    {
        public int FilmStudioId { get; set; }
        public string FilmStudioName { get; set; }
        [JsonIgnore]
        public string FilmStudioCity { get; set; }
        [JsonIgnore]
        public List<FilmCopy> RentedFilmCopies { get; set; }
    }
}
