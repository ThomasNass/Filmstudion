using AutoMapper;
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

        public UserController(UserService userService, IMapper mapper, FilmStudioService filmStudioService,UserManager<User> userManager, IConfiguration configuration )
        {
            _mapper = mapper;
            _userService = userService;
            _filmStudioService = filmStudioService;
            _userManager = userManager;
            _configuration = configuration;
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
                if(user.Role == "admin") { 
                return Ok(new
                {
                    username = user.UserName,
                    role = user.Role,
                    id = user.Id,
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
                }
               else if (user.Role == "filmstudio")
                {
                    return Ok(new
                    {
                        username = user.UserName,
                        role = user.Role,
                        id = user.Id,
                        filmstudioId = user.FilmStudioId,
                        filmstudio = user.FilmStudio,
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo
                    }); ;
                }
            }
            return Unauthorized();
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUser model)
        {
            var userExists = await _userManager.FindByNameAsync(model.UserName);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError);

            User user = new User()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName,
                Role = "admin",
                IsAdmin = model.IsAdmin
            };
           
            var result = await _userManager.CreateAsync(user, model.Password);
            
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError);
            

            var Created = await _userManager.FindByNameAsync(model.UserName);
            var userDisplay = _mapper.Map<UserCreated>(Created);
            _userManager.AddToRoleAsync(Created, "admin").Wait();
            return Ok(userDisplay);
        }

       
    }

    
}

