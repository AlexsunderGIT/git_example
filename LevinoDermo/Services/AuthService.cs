using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using ConsoleProject.NET.DTO.User;
using ConsoleProject.NET.Interfaces.Services;
using ConsoleProject.NET.Models;
using ConsoleProject.NET.Utils;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ConsoleProject.NET.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IConfiguration _configuration;
        public AuthService(IUserRepository userRepository, IPasswordHasher passwordHasher, IConfiguration configuration)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }
        public async Task<AuthentificationResponseDto> Login(UserLoginDto request)
        {
            var user = await _userRepository.GetByName(request.Name);
            if (user == null)
                throw new ApplicationException("User not found");

            if (!_passwordHasher.VerifyPasswordHash(
                request.Password,
                user.PasswordHash,
                user.PasswordSalt))
            {
                throw new ApplicationException("Wrong password");
            }
            return new AuthentificationResponseDto(user.Id, user.Name, GenerateJwtToken(user));
        }
        private string GenerateJwtToken(User user)
        {
            // Проверка наличия ключа
            var jwtKey = _configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(jwtKey))
            {
                throw new InvalidOperationException("JWT Key не настроен в конфигурации");
            }

            var claims = new[]
            {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name, user.Name)
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = creds,
                Issuer = _configuration["Jwt:Issuer"], // Добавьте issuer и audience
                Audience = _configuration["Jwt:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}