using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using OdontoPrev.Services;
using OdontoPrev.ViewModels;

namespace OdontoPrev.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthorService _authorService;

        public AuthController(IConfiguration configuration, IAuthorService authorService)
        {
            _configuration = configuration;
            _authorService = authorService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            var author = await _authorService.AuthenticateAsync(model.Username, model.Password);

            if (author == null)
                return Unauthorized();

            var token = GenerateJwtToken(author);

            return Ok(new { Token = token });
        }

        [HttpGet("authors")]
        public async Task<IActionResult> GetAllAuthors()
        {
            var authors = await _authorService.GetAllAsync();
            return Ok(authors);
        }

        private string GenerateJwtToken(AuthorViewModel author)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, author.Username),
                new Claim(JwtRegisteredClaimNames.Email, author.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, author.Id.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}