using Microsoft.AspNetCore.Mvc;
using Google.Apis.Auth;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using cinema.Models;
using cinema.Services;
using System.Security.Principal;
using System;

namespace cinema.Controllers
{
    [Route("api/google")]
    public class AuthController : Controller
    {
        private readonly IConfiguration _config;
        private readonly AccountService _accountService;

        public AuthController(IConfiguration config, AccountService accountService)
        {
            _config = config;
            _accountService = accountService;
        }

        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginDto dto)
        {
            GoogleJsonWebSignature.Payload payload;
            try
            {
                payload = await GoogleJsonWebSignature.ValidateAsync(dto.IdToken);
            }
            catch (Exception ex)
            {
                // Log the exception for further investigation
                return Unauthorized(new { message = "Invalid Google ID token", error = ex.Message });
            }

            var token = GenerateJwtToken(payload);
            return Ok(new { token });
        }


        [HttpPost("google-register")]
        public async Task<IActionResult> GoogleRegister([FromBody] GoogleLoginDto dto)
        {
            GoogleJsonWebSignature.Payload payload;
            try
            {
                payload = await GoogleJsonWebSignature.ValidateAsync(dto.IdToken);
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = "Invalid Google ID token", error = ex.Message });
            }

            var existingAccount = await _accountService.GetByEmailAsync(payload.Email);
            if (existingAccount == null)
            {
                var random = new Random();
                var newAccount = new Account
                {
                    Username = payload.Name,
                    Email = payload.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword("123"),
                    Role = 1,
                    Created = DateTime.Now,
                    Verify = 1,
                    Phone = "",
                    Gender = "",
                    Birthday = DateTime.Now,
                    Securitycode = random.Next(100000, 1000000).ToString("D6"),
                };

                await _accountService.CreateAsync(newAccount);
                existingAccount = newAccount;
            }

            var token = GenerateJwtToken(payload);
            return Ok(new { token });
        }

        private string GenerateJwtToken(GoogleJsonWebSignature.Payload payload)
        {
            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub, payload.Subject),
        new Claim(JwtRegisteredClaimNames.Email, payload.Email),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }

    public class GoogleLoginDto
    {
        public string IdToken { get; set; }
    }
}
