using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManager.Api.Repositories;

namespace TaskManager.Api.Services
{
    public class AuthService
    {
        private readonly UserRepository _userRepository;
        private readonly IConfiguration _config;

        public AuthService(UserRepository userRepository, IConfiguration config)
        {
            _userRepository = userRepository;
            _config = config;
        }

        public string? Login(string email, string password)
        {
            var user = _userRepository.GetUserByEmail(email);

            if (user == null || user.Password != password)
            {
                return null; 
            }

            return GenerateToken(user.Id, user.Email);
        }

        private string GenerateToken(int userId, string email)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Email, email)
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
