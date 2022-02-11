using AutoMapper;
using Filmstudion.API.Models.DTO;
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
using System.Linq;
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
        
       [HttpPut]
       public async Task<IActionResult> CreateFilm([FromBody]CreateFilm createFilm)
        {
            if (User.IsInRole("admin")) { 
            var film = _mapper.Map<Film>(createFilm);
            var films = await _filmService.GetFilmsAsync();
            film.FilmId = films.Count() +1;
            var created = _filmService.CreateFilm(film);
            _filmService.AddCopies(film.FilmId, createFilm.NumberOfCopies);
            return Ok(created);
            }
            return Unauthorized();
        }
        
        [HttpPut("{FilmId}")]
        public async Task<IActionResult> ChangeNumberOfFilmCopies(int filmId,[FromBody] ChangeFilmCopies copies)
        {
            if (User.IsInRole("admin")) 
           {
                var film = await _filmService.GetFilm(filmId);
                if(film==null) return NotFound();
                await _filmService.ChangeFilmCopies(filmId, copies.DesiredNumberOfCopies);
                
                return Ok();
            }
            return Unauthorized();
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetFilms()
        {
            if (User.IsInRole("admin") || User.IsInRole("filmstudio"))
            {
                var films = await _filmService.GetFilmsAsync();
                var AuthFilms = _mapper.Map<IList<AuthFilm>>(films);
                return Ok(AuthFilms);
            }
            else 
            { 
                var films = await _filmService.GetFilmsAsync();
                var noAuthFilms = _mapper.Map<IList<NoAuthFilm>>(films);
                return Ok(noAuthFilms);
            }
        }
        [HttpPatch("{FilmId}")]
        public async Task<IActionResult> UpdateFilm(int filmId, [FromBody] JsonPatchDocument<Film> patchEntity)
        {
            if (User.IsInRole("admin")) 
            { 
            var film = await _filmService.UpdateFilm(filmId, patchEntity);
                if(film != null) 
                { 
                return Ok(film);
                }
                else return NotFound();
            }
            return Unauthorized();
        }

        [AllowAnonymous]
        [HttpGet("{FilmId}")]
        public async Task<IActionResult> GetFilm(int filmId)
        {
            var film = await _filmService.GetFilm(filmId);
            if(film == null) return NotFound();
            if (User.IsInRole("admin") || User.IsInRole("filmstudio"))
            {
                var AuthFilm = _mapper.Map<AuthFilm>(film);
                return Ok(AuthFilm);
            }
            else 
            {
                var noAuthFilm = _mapper.Map<NoAuthFilm>(film);
                return Ok(noAuthFilm);
            }
        }

        [HttpGet]
        [Route("filmcopies")]
        public async Task<IActionResult> GetFilmCopies()
        {
            var copies = await _filmService.GetFilmCopies();
            return Ok(copies);
        }

          
         [HttpPost]
         [Route("rent")]
         public async Task <IActionResult> RentFilm([FromQuery] int id ,int studioId )
         {
             await _filmService.RentFilm(id,studioId);//Denna måste jobbas mer på
             return Ok();
         }
        //TODO 13.
        //Ett lyckat anrop ska returnera statuskod 200.
       /* Om anropet görs av en icke-autentiserad admin, eller av en autentiserad filmstudio vars id inte överensstämmer 
        * med id:t som anges i anropet, ska anropet returnera statuskod 401 eller annan felkod(absolut inte 200) och lånet ska inte godkännas.
       Om filmen inte finns ska statuskod 409 returneras.
       Om filmen inte har några lediga kopior ska statuskod 409 returneras.
       Om filmstudion redan hyr en kopia av samma film ska statuskod 403 returneras och lånet ska inte gå igenom.*/
       

        [HttpPost]
        [Route("return")]
        public async Task <IActionResult> ReturnFilm([FromQuery]int id, int studioId)
        {
            await _filmService.ReturnFilm(id, studioId);
            return Ok();
        }

        //TODO 14.
        //Ett lyckat anrop ska returnera statuskod 200.
        /*Om anropet görs av en icke-autentiserad användare, 
         * eller om filmstudion som är inloggad inte överensstämmer med filmstudion vars id anges i anropet,
         * ska anropet returnera statuskod 401.
         Om filmen inte finns ska statuskod 409 returneras.*/

    }
}
