using AutoMapper;
using Filmstudion.API.Models.CRUD;
using Filmstudion.API.Models.User;
using Filmstudion.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Filmstudion.API.Controllers
{   [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserService _userService;

        public UserController(UserService userService, IMapper mapper)
        {
            _mapper = mapper;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterUser user)
        {
            var _user = _mapper.Map<User>(user);

            try
            {
                _userService.Create(_user);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
