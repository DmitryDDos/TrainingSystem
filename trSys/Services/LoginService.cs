using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using trSys.DTOs;
using trSys.Interfaces;
using trSys.Models;

namespace trSys.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly JwtSettings _jwtSettings;

        public LoginService(
            IUserService userService,
            IHttpContextAccessor httpContextAccessor,
            IOptions<JwtSettings> jwtSettings)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<AuthResult> LoginAsync(LoginDto loginDto)
        {
            var authResult = await _userService.LoginAsync(loginDto);

            if (authResult.Success)
            {
                await SignInUser(authResult.User, authResult.Token);
                return new AuthResult(true, "Успешный вход", authResult.Token);
            }

            return new AuthResult(false, authResult.Message);
        }

        private async Task SignInUser(UserDto user, string token)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("JWT", token)
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(_jwtSettings.ExpiryInMinutes)
            };

            await _httpContextAccessor.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }

        public async Task LogoutAsync()
        {
            await _httpContextAccessor.HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
