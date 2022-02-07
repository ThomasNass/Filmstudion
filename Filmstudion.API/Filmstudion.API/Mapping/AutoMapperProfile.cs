using AutoMapper;
using Filmstudion.API.Models.CRUD;
using Filmstudion.API.Models.FilmStudioDir;
using Filmstudion.API.Models.User;

namespace Filmstudion.API.Mapping
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<FilmStudio, FilmStudios>();
            CreateMap<RegisterFilmStudio, FilmStudio>();
            CreateMap<RegisterUser, User>();
            CreateMap<RegisterFilmStudio, User>();
        }
    }
}
