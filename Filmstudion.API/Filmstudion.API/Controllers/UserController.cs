using AutoMapper;
using Filmstudion.API.Models.CRUD;
using Filmstudion.API.Models.User;
using Filmstudion.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Filmstudion.API.Controllers
{   [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserService _userService;
        private readonly FilmStudioService _filmStudioService;

        public UserController(UserService userService, IMapper mapper, FilmStudioService filmStudioService)
        {
            _mapper = mapper;
            _userService = userService;
            _filmStudioService = filmStudioService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterUser user)
        {
            var _user = _mapper.Map<User>(user);

            try
            {
                var created = _userService.CreateUser(_user);
                return Ok(created);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
       [AllowAnonymous]
        [HttpGet("filmstudios")]
        public async Task<IActionResult> GetAllFilmStudios()
        {
            var filmStudios = await _filmStudioService.GetAllFilmStudios();
            var model = _mapper.Map<IList<FilmStudios>>(filmStudios);
            return Ok(model);
        }
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task <IActionResult> Authenticate([FromBody]UserAuthenticate model)
        {
            var user = await _userService.Authenticate(model.UserName, model.Password);
        }
    }
}
