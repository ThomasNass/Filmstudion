using Filmstudion.API.Models.Film;
using Filmstudion.API.Models.FilmStudioDir;
using Newtonsoft.Json;
using System.Collections.Generic;


namespace Filmstudion.API.Models.DTO
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
