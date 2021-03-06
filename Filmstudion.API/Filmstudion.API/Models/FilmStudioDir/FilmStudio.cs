using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Filmstudion.API.Models.Film;
using System;

namespace Filmstudion.API.Models.FilmStudioDir
{
    public class FilmStudio:IFilmStudio
    {
       
        public string FilmStudioId { get; set; }
        public string FilmStudioName { get; set; }
        public string FilmStudioCity { get; set; }
        public List<FilmCopy> RentedFilmCopies { get; set; }

    }
}
