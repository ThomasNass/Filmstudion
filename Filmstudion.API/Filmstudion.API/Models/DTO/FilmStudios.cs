using Filmstudion.API.Models.Film;
using Filmstudion.API.Models.FilmStudioDir;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Filmstudion.API.Models.DTO
{
    public class FilmStudios: IFilmStudio
    {
        public string FilmStudioName { get; set; }
        public string FilmStudioCity { get; set; }
        [JsonIgnore]
        public string FilmStudioId { get; set; }
        public List<FilmCopy> RentedFilmCopies { get; set; }
    }
}
