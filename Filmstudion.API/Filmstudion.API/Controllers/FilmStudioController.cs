using AutoMapper;
using Filmstudion.API.Models.DTO;
using Filmstudion.API.Models.FilmStudioDir;
using Filmstudion.API.Models.User;
using Filmstudion.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Filmstudion.API.Controllers
{   [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class FilmStudioController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly FilmStudioService _filmStudioService;

        public FilmStudioController(FilmStudioService filmStudioService, IMapper mapper, UserManager<User> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _filmStudioService = filmStudioService;
        }

       
        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterFilmStudio model)
        {
            var userExists = await _userManager.FindByNameAsync(model.FilmStudioName);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError);

            var studio = _mapper.Map<FilmStudio>(model);
            var filmStudio = _filmStudioService.CreateFilmStudio(studio);

            User user = new User()
            {   
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.FilmStudioName,
                Role = "filmstudio",
                FilmStudio = studio,
                FilmStudioId = model.FilmStudioName               
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError);
            _userManager.AddToRoleAsync(user, "filmstudio").Wait();
            var authFilmStudio = _mapper.Map<FilmStudioCreated>(filmStudio);
            return Ok(authFilmStudio);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFilmStudios()
        {
            var filmStudios = await _filmStudioService.GetAllFilmStudios();
            var model = _mapper.Map<IList<FilmStudios>>(filmStudios);
            return Ok(model);
        }
        
        [HttpGet("{filmStudioId}")]
        public async Task<IActionResult> GetFilmStudio(string filmstudioId)
        {
            var filmStudio = await _filmStudioService.GetFilmStudio(filmstudioId);
            if (User.IsInRole("admin"))
            {
                var displayFilmStudio = _mapper.Map<AuthFilmStudio>(filmStudio);
                return Ok(displayFilmStudio);
            }
          
            var noAuthFilmStudio = _mapper.Map<NoAuthFilmStudio>(filmStudio);
            return Ok(noAuthFilmStudio);
        }
    }
}
