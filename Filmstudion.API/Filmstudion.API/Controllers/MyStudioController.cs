using AutoMapper;
using Filmstudion.API.Models.DTO;
using Filmstudion.API.Models.User;
using Filmstudion.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Filmstudion.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class MyStudioController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IFilmStudioService _filmStudioService;
        private readonly IMapper _mapper;

        public MyStudioController(UserManager<User> userManager, IFilmStudioService filmStudioService, IMapper mapper)
        {
           _userManager = userManager;
           _filmStudioService = filmStudioService;
            _mapper = mapper;
        }

        [HttpGet("rentals")]
        public async Task<IActionResult> GetRentedFilms()
        {
            var user = User.Identity.Name;
            if (User.IsInRole("filmstudio") && User.Identity.Name == user) { 
            var filmStudio =await _filmStudioService.GetFilmStudio(user);
            var copies = _mapper.Map<FilmCopies>(filmStudio);
            return Ok(copies);
            }
            return Unauthorized();
        }
    }
}
