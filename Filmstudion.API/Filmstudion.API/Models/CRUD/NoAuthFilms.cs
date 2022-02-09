using Filmstudion.API.Models.Film;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Filmstudion.API.Models.CRUD
{
    public class NoAuthFilms:IFilm
    {
        public int FilmId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Director { get; set; }
        [JsonIgnore]
        public List<FilmCopy> FilmCopies { get; set; }
    }
}
