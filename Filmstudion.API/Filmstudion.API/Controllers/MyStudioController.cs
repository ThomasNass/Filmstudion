using Filmstudion.API.Models.User;
using Filmstudion.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Filmstudion.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class MyStudioController : ControllerBase
    {
        private readonly FilmService _filmService;
        private readonly UserManager<User> _userManager;
        private readonly FilmStudioService _filmStudioService;
        private readonly UserService _userService;

        public MyStudioController(FilmService filmService, UserManager<User> userManager, FilmStudioService filmStudioService, UserService userService)
        {
           _filmService = filmService;
           _userManager = userManager;
           _filmStudioService = filmStudioService;
           _userService = userService;
        }

       /* [HttpGet]
        public async Task<IActionResult> GetRentedFilms()
        {
            var userName = User.Identity.Name;
            var user = _userService.GetByName(userName);
            var b = user.FilmStudioId;
            var filmstudioUser = _userManager.FindByIdAsync(user.);
            await _filmStudioService.GetFilmStudio();
        }*/
    }
}
