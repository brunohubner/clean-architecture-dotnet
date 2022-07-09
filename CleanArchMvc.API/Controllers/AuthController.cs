using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions;
using CleanArchMvc.Domain.Account;
using CleanArchMvc.API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CleanArchMvc.API.Controllers
{
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticate _authentication;
        private readonly IConfiguration _configuration;

        public AuthController(
            IAuthenticate authentication,
            IConfiguration configuration
        )
        {
            _authentication = authentication;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(
            [FromBody] RegisterModel userInfo
        )
        {
            var result = await _authentication.RegisterUser(
                userInfo.Email,
                userInfo.Password
            );

            if (!result)
            {
                ModelState.AddModelError(string.Empty, "Invalid Register attempt.");
                return BadRequest(ModelState);
            }

            return Ok($"User {userInfo.Email} was create successfully");
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserToken>> Login(
            [FromBody] LoginModel userInfo
        )
        {
            var result = await _authentication.Authenticate(
                userInfo.Email,
                userInfo.Password
            );

            if (!result)
            {
                ModelState.AddModelError(string.Empty, "Invalid Login attempt.");
                return BadRequest(ModelState);
            }

            return GenerateToken(userInfo);
        }

        private UserToken GenerateToken(LoginModel userInfo)
        {
            var claims = new[]
            {
                new Claim("email", userInfo.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var privateKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"])
            );

            var credentials = new SigningCredentials(
                privateKey,
                SecurityAlgorithms.HmacSha256
            );

            var expiration = DateTime.UtcNow.AddMinutes(10);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
            );

            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}