using AutoMapper;
using Filmstudion.API.Models.CRUD;
using Filmstudion.API.Models.FilmStudio;
using Filmstudion.API.Models.User;

namespace Filmstudion.API.Mapping
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterFilmStudio, FilmStudio>();
            CreateMap<RegisterUser, User>();
        }
    }
}
