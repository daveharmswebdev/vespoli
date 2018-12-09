using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Vespoli.Data;
using Vespoli.Domain;
using Vespoli.Api.Dtos;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Logging;

namespace Vespoli.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthRepository repo, IConfiguration config, ILogger<AuthController> logger)
        {
            _repo = repo;
            _config = config;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RowerForRegisterDto rowerForRegisterDto)
        {
            try
            {
                rowerForRegisterDto.UserName = rowerForRegisterDto.UserName.ToLower();

                if (await _repo.UserExists(rowerForRegisterDto.UserName))
                    return BadRequest("Username already exists");

                var rowerToCreate = new Rower
                {
                    UserName = rowerForRegisterDto.UserName
                };

                var createdUser = await _repo.Register(rowerToCreate, rowerForRegisterDto.Password);

                return StatusCode(201);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to register new user: {ex}");
                return BadRequest($"Failed to register new user.");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(RowerForLoginDto rowerForLoginDto)
        {
            try
            {
                var rowerFromRepo = await _repo.Login(rowerForLoginDto.UserName.ToLower(), rowerForLoginDto.Password);

                if (rowerFromRepo == null)
                    return Unauthorized();

                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, rowerFromRepo.Id.ToString()),
                    new Claim(ClaimTypes.Name, rowerFromRepo.UserName)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = creds
                };

                var tokenHandler = new JwtSecurityTokenHandler();

                var token = tokenHandler.CreateToken(tokenDescriptor);

                return Ok(new
                {
                    token = tokenHandler.WriteToken(token)
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to login user: {ex}");
                return BadRequest($"Failed to login user.");
            }
        }
    }
}
