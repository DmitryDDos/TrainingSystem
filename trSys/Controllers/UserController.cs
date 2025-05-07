using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using trSys.Interfaces;
using trSys.Models;
using trSys.Repos;
using trSys.Services;


namespace trSys.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController<User>
    {
        private readonly UserRepository _repository;
        private readonly IConfiguration _configuration;
        private readonly AuthService _authService;

        public UserController(
            UserRepository repository,
            IConfiguration configuration,
            AuthService authService) : base(repository)
        {
            _repository = repository;
            _configuration = configuration;
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            // Валидация модели
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Проверка, что пользователь с таким email уже не существует
            var existingUser = await _repository.GetByEmailAsync(model.Email);
            if (existingUser != null)
                return Conflict(new { Message = "User with this email already exists" });

            // Генерация хеша пароля с солью
            var passwordHash = _authService.CreateHashPassword(model.Password);

            // Создание пользователя с ролью по умолчанию, если не указана
            var user = new User(
                model.Email,
                passwordHash,
                model.FullName,
                model.Role ?? "User");

            await _repository.AddAsync(user);

            return Ok(new { Message = "User registered successfully" });
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            // Валидация модели
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Поиск пользователя по email (оптимизированный запрос)
            var user = await _repository.GetByEmailAsync(model.Email);
            if (user == null)
                return Unauthorized(new { Message = "Invalid email or password" });

            // Проверка пароля
            if (!_authService.VerifyPassword(model.Password, user.PasswordHash))
                return Unauthorized(new { Message = "Invalid email or password" });

            // Генерация JWT-токена
            var token = _authService.GenerateJwtToken(user);

            return Ok(new
            {
                Token = token,
                UserId = user.Id,
                Email = user.Email,
                Role = user.Role
            });
        }

        [HttpGet("validate-token")]
        public IActionResult ValidateToken()
        {
            return Ok(new
            {
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                IsAuthenticated = User.Identity?.IsAuthenticated,
                Claims = User.Claims.Select(c => new { c.Type, c.Value })
            });
        }

    }
}
