using AutoMapper;
using Filmstudion.API.Models.CRUD;
using Filmstudion.API.Models.FilmStudioDir;
using Filmstudion.API.Models.User;
using Filmstudion.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Filmstudion.API.Controllers
{   [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class FilmStudioController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly FilmStudioService _filmStudioService;

        public FilmStudioController(FilmStudioService filmStudioService, IMapper mapper)
        {
            _mapper = mapper;
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
                var registered = _filmStudioService.CreateFilmStudio(studio,user);
                return Ok(registered);
            }
            catch(System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
