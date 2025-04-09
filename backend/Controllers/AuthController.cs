using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Entity;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IRepository<User> userRepo;
        private readonly IConfiguration configuration;

        public AuthController(IRepository<User> userRepo, IConfiguration configuration)
        {
            this.userRepo = userRepo;
            this.configuration = configuration;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthDto model)
        {
            var user = (await userRepo.GetAll(x => x.Email == model.Email)).FirstOrDefault();

            if (user == null)
            {
                return new BadRequestObjectResult(new { Message = "Usuario ou senha invalidos" });
            }

            var passwordHelper = new PasswordHelper();

            if (!passwordHelper.VerifyPassword(user.Password,model.Password))
            {
                return new BadRequestObjectResult(new { Message = "Usuario ou senha invalidos" });
            }

            var token = GenerateToken(user.Email, user.Role); 
            return Ok(new AuthTokenDto()
            {
                Id = user.Id,
                Email = user.Email,
                Token = token,
                Role = user.Role,
            });
        }
        private string GenerateToken(string email, string role)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtKey"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name,email),
                new Claim(ClaimTypes.Role,role)

            };
            var token = new JwtSecurityToken(claims: claims, expires: DateTime.UtcNow.AddHours(1), signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
