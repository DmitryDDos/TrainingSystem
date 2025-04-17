using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using trSys.Interfaces;
using trSys.Models;
using trSys.Repos;

namespace trSys.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController<User>
    {
        private readonly UserRepository _repository;
        private readonly IConfiguration _configuration;


        public UserController(UserRepository repository, IConfiguration configuration) : base(repository) 
        { 
            _repository = repository;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            var hashedPassword = CreateHashPassword(model.Password); // Хешируем пароль
            var user = new User(model.Email, hashedPassword, model.FullName, model.Role); // Передаём хеш
            await _repository.AddAsync(user);
            return Ok(new { Message = "User registered successfully" });
        }


        [HttpPost("login")]//HUITA
        public async Task<IActionResult> Login(LoginModel model)
        {
            var user = (await _repository.GetAllAsync()).FirstOrDefault(u => u.Email == model.Email); // лучше эту хуйню  репозиторий вынести
            if (user == null || user.PasswordHash != CreateHashPassword(model.Password)) // if (!CompareHashes(user.PasswordHash, CreateHashPassword(model.Password)))
            {
                return Unauthorized(new { Message = "Invalid email or password" });
            }

            var token = GenerateJwtToken(user);
            return Ok(new { Token = token });
        }

        private string GenerateJwtToken(User user)
        {
            var jwtKey = _configuration["Jwt:Key"];
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role ?? "User") // Если Role null ? "User"
                }),
                Issuer = issuer,       // "localhost"
                Audience = audience,    // "swagger_ui"
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        private string CreateHashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }
    }
}
