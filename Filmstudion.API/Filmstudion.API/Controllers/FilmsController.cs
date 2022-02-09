using AutoMapper;
using Filmstudion.API.Models.CRUD;
using Filmstudion.API.Models.Film;
using Filmstudion.API.Models.User;
using Filmstudion.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Filmstudion.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class FilmsController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly FilmService _filmService;

        public FilmsController(UserManager<User> userManager, IMapper mapper, FilmService filmService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _filmService = filmService;
        }
        [AllowAnonymous]//Remove this and uncomment if-statement when the filmcopy issue has been resolved 
       [HttpPut]
       public IActionResult CreateFilm([FromBody]CreateFilm createFilm)
        {
            //if (User.IsInRole("admin")) { 
            var film = _mapper.Map<Film>(createFilm);
            var created = _filmService.CreateFilm(film, createFilm.NumberOfCopies);
            return Ok(created);
            //}
           // return Unauthorized();
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetFilms()
        {
            if (User.IsInRole("admin") || User.IsInRole("filmstudio"))
            {
                var films = await _filmService.GetFilmsAsync();
                return Ok(films);
            }
            else 
            { 
                var films = await _filmService.GetFilmsAsync();
                var noAuthFilms = _mapper.Map<IList<NoAuthFilms>>(films);
                return Ok(noAuthFilms);
            }
        }
        [HttpPatch("{FilmId}")]
        public async Task<IActionResult> UpdateFilm(int filmId, [FromBody] JsonPatchDocument<Film> patchEntity)
        {

            var film = await _filmService.UpdateFilm(filmId, patchEntity);
            
            return Ok(film);

        }
        [AllowAnonymous]
        [HttpGet]
        [Route("filmcopies")]
        public async Task<IActionResult> GetFilmCopies()
        {
            var copies = await _filmService.GetFilmCopies();
            return Ok(copies);
        }

    }
}
