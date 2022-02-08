using AutoMapper;
using Filmstudion.API.Helpers;
using Filmstudion.API.Models.CRUD;
using Filmstudion.API.Models.User;
using Filmstudion.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Filmstudion.API.Controllers
{   [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserService _userService;
        private readonly FilmStudioService _filmStudioService;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        //private readonly AppSettings _appSettings; //This belongs to jasons solution for JWT


        public UserController(UserService userService, IMapper mapper, FilmStudioService filmStudioService,UserManager<User> userManager, IConfiguration configuration )//IOptions<AppSettings> appSettings
        {
            _mapper = mapper;
            _userService = userService;
            _filmStudioService = filmStudioService;
            _userManager = userManager;
            _configuration = configuration;
            // _appSettings = appSettings.Value;
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
      // [AllowAnonymous]
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
            var user = await _userManager.FindByNameAsync(model.UserName);
            if(user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return Ok(new
                {
                    username = user.UserName,
                    role = user.Role,
                    id = user.UserId,
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("Begister")]
        public async Task<IActionResult> Begister([FromBody] RegisterUser model)
        {
            var userExists = await _userManager.FindByNameAsync(model.UserName);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError);

            User user = new User()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName,
                Role = "Admin",
                IsAdmin = model.IsAdmin
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError);

            return Ok();
        }

        /* var user = await _userService.Authenticate(model.UserName, model.Password);

         if (user == null) return BadRequest();

         //Jasons solution
         var tokenHandler = new JwtSecurityTokenHandler();
         var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
         var tokenDescriptor = new SecurityTokenDescriptor
         {
             Subject = new ClaimsIdentity(new Claim[]
             {
                 new Claim(ClaimTypes.Name, user.Id.ToString())
             }),
             Expires = DateTime.UtcNow.AddDays(7),
             SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
         };
         var token = tokenHandler.CreateToken(tokenDescriptor);
         var tokenString = tokenHandler.WriteToken(token);

         // return basic user info and authentication token
         return Ok(new
         {
             Id = user.UserId,
             Username = user.UserName,
             Role = user.Role,
             Token = tokenString
         });*/
    }

    
}

