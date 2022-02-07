
using System;
using System.Collections.Generic;

namespace Filmstudion.API.Models.Film
{
    public class Film: IFilm
    {
        public int FilmId { get; set; }
        public string Name { get; set; }
        public DateTime RealeaseDate { get; set; }
        public string Country { get; set; }
        public string Director { get; set; }
        public List<FilmCopy> FilmCopies { get; set; }
    }
}
