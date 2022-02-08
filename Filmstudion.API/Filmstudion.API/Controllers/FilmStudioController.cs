using AutoMapper;
using Filmstudion.API.Models.CRUD;
using Filmstudion.API.Models.FilmStudioDir;
using Filmstudion.API.Models.User;
using Filmstudion.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
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
        [HttpPost("register")]
        public IActionResult Register([FromBody]RegisterFilmStudio filmStudio)
        {
            var studio = _mapper.Map<FilmStudio>(filmStudio);
            var user = _mapper.Map<User>(filmStudio);

            try
            {
                var registered = _filmStudioService.CreateFilmStudio(studio);
                return Ok(registered);
            }
            catch(System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("Begister")]
        public async Task<IActionResult> Begister([FromBody] RegisterFilmStudio model)
        {
            var userExists = await _userManager.FindByNameAsync(model.UserName);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError);

            var studio = _mapper.Map<FilmStudio>(model);
            var filmStudio = _filmStudioService.CreateFilmStudio(studio);

            User user = new User()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName,
                Role = "filmstudio",
                FilmStudio = studio,
                FilmStudioId = filmStudio.FilmStudioId

            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError);
            _userManager.AddToRoleAsync(user, "filmstudio").Wait();
            return Ok();
        }
    }
}
