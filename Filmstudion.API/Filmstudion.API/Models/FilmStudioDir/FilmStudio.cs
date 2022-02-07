using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Filmstudion.API.Models.Film;

namespace Filmstudion.API.Models.FilmStudioDir
{
    public class FilmStudio:IFilmStudio
    {
       
        public int FilmStudioId { get; set; }
        public string FilmStudioName { get; set; }
        public string FilmStudioCity { get; set; }
        public List<FilmCopy> RentedFilmCopies { get; set; }
      
    }
}
