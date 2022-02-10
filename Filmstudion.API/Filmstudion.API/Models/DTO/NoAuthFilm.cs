using Filmstudion.API.Models.Film;
using Newtonsoft.Json;
using System.Collections.Generic;


namespace Filmstudion.API.Models.DTO
{
    public class NoAuthFilm:IFilm
    {
        public int FilmId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Director { get; set; }
        [JsonIgnore]
        public List<FilmCopy> FilmCopies { get; set; }
    }
}
