using freelanceProjectEgypt03.data.freelanceProjectEgypt03.Data;
using freelanceProjectEgypt03.Dtos.freelanceProjectEgypt03.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace freelanceProjectEgypt03.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public AuthController(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpPost("client-login")]
        public async Task<ActionResult<TokenDto>> ClientLogin([FromBody] LoginDto login)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(c => c.email == login.Email);
            if (client == null || !BCrypt.Net.BCrypt.Verify(login.Password, client.Password))
                return Unauthorized("Invalid client credentials");

            var token = GenerateJwtToken(client.email, "Client");
            return Ok(new TokenDto { Token = token });
        }

        [HttpPost("partner-login")]
        public async Task<ActionResult<TokenDto>> PartnerLogin([FromBody] LoginDto login)
        {
            var partner = await _context.Partners.FirstOrDefaultAsync(p => p.email == login.Email);
            if (partner == null || !BCrypt.Net.BCrypt.Verify(login.Password, partner.Password))
                return Unauthorized("Invalid partner credentials");

            var token = GenerateJwtToken(partner.email, "Partner");
            return Ok(new TokenDto { Token = token });
        }

        private string GenerateJwtToken(string email, string role)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Role, role)
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(int.Parse(_config["Jwt:ExpireDays"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
