using Application.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using Microsoft.AspNetCore.Http;
using WebAPI.Wrappers;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WebAPI.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        public IdentityController(UserManager<ApplicationUser> userManager,IConfiguration configuration) 
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            
            
                var userExists = await _userManager.FindByNameAsync(registerModel.Username);
                if (userExists != null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response<bool>
                    {
                        Succeeded = false,
                        Message = "User already exists!"
                    });
                }

                ApplicationUser applicationUser = new ApplicationUser()
                {
                    Email = registerModel.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = registerModel.Username
                };
                var result = await _userManager.CreateAsync(applicationUser, registerModel.Password);
                if (!result.Succeeded)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response<bool>
                    {
                        Succeeded = false,
                        Message = "User creation failed! Please check user details and try again.",
                        Errors = result.Errors.Select(x => x.Description)
                    }) ;
                }
            
            return Ok(new Response<bool> {
                Succeeded=true,
                Message = "User created successfully!"
            });
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var user = await _userManager.FindByNameAsync(loginModel.Username);
            if(user!=null && await _userManager.CheckPasswordAsync(user, loginModel.Password))
            {
                var authClaims = new List<Claim>
                {
                    new Claim (ClaimTypes.NameIdentifier,user.Id),
                    new Claim(ClaimTypes.Name,user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    expires: DateTime.Now.AddHours(2),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }

            return Unauthorized();
        }


    }
}
