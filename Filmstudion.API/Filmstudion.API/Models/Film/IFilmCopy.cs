﻿namespace Filmstudion.API.Models.Film
{
    public interface IFilmCopy
    {
        public string FilmCopyId { get; set; }
        public string FilmId { get; set; }
        public bool RentedOut { get; set; }
        public int FilmStudioId { get; set; }
    }
}