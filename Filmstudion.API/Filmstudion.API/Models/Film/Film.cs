﻿
namespace Filmstudion.API.Models.Film
{
    public class Film: IFilm
    {
        public int FilmId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int FilmCopies { get; set; }
    }
}
