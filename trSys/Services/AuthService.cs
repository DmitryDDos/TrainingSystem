using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using trSys.Models;

namespace trSys.Services
{
    public class AuthService
    {
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role ?? "User")
                }),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                Expires = DateTime.UtcNow.AddDays(1), // Токен на 1 день
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string CreateHashPassword(string password)
        {
            // Генерация соли
            var salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Исправленный конструктор с явным указанием алгоритма и итераций
            using (var pbkdf2 = new Rfc2898DeriveBytes(
                password,
                salt,
                100000,  // Увеличенное количество итераций
                HashAlgorithmName.SHA512))  // Более безопасный алгоритм
            {
                var hash = pbkdf2.GetBytes(32);  // 32 байта для SHA512
                var hashBytes = new byte[48];    // 16 (соль) + 32 (хеш)
                Array.Copy(salt, 0, hashBytes, 0, 16);
                Array.Copy(hash, 0, hashBytes, 16, 32);
                return Convert.ToBase64String(hashBytes);
            }
        }

        public bool VerifyPassword(string inputPassword, string storedHash)
        {
            if (string.IsNullOrEmpty(storedHash))
                return false;

            var hashBytes = Convert.FromBase64String(storedHash);
            var salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            using (var pbkdf2 = new Rfc2898DeriveBytes(
                inputPassword,
                salt,
                100000,
                HashAlgorithmName.SHA512))
            {
                var hash = pbkdf2.GetBytes(32);
                for (int i = 0; i < 32; i++)
                {
                    if (hashBytes[i + 16] != hash[i])
                        return false;
                }
            }

            return true;
        }
    }
}
