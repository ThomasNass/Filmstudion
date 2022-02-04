using AutoMapper;
using Filmstudion.API.Models.CRUD;
using Filmstudion.API.Models.FilmStudio;
using Filmstudion.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Filmstudion.API.Controllers
{   [Authorize]
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

            try
            {
                _filmStudioService.Create(studio);
                return Ok();
            }
            catch(System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
