using AutoMapper;
using Filmstudion.API.Models.CRUD;
using Filmstudion.API.Models.FilmStudio;

namespace Filmstudion.API.Mapping
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterFilmStudio, FilmStudio>();
        }
    }
}
