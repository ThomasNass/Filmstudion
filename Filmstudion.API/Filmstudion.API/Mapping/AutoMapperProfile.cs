using AutoMapper;
using Filmstudion.API.Models.DTO;
using Filmstudion.API.Models.Film;
using Filmstudion.API.Models.FilmStudioDir;
using Filmstudion.API.Models.User;

namespace Filmstudion.API.Mapping
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            
            CreateMap<RegisterFilmStudio, FilmStudio>();
            CreateMap<RegisterUser, User>();
            CreateMap<RegisterFilmStudio, User>();
            CreateMap<User,UserCreated>();
            CreateMap<CreateFilm,Film>();
            CreateMap<Film,NoAuthFilm>();
            CreateMap<FilmStudio, NoAuthFilmStudio>();
            CreateMap<FilmStudio, FilmStudioCreated>();
            CreateMap<FilmStudio, AuthFilmStudio>();
            CreateMap<Film,AuthFilm>();
            CreateMap<FilmStudio,FilmCopies>();
        }
    }
}
