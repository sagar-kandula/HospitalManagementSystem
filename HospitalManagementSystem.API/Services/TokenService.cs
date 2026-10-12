using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HospitalManagementSystem.API.Models;

namespace HospitalManagementSystem.API.Services
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(User user)
        {
            string key = _configuration["Jwt:Key"]
                ?? throw new InvalidOperationException(
                    "JWT Key is not configured.");

            string issuer = _configuration["Jwt:Issuer"]
                ?? throw new InvalidOperationException(
                    "JWT Issuer is not configured.");

            string audience = _configuration["Jwt:Audience"]
                ?? throw new InvalidOperationException(
                    "JWT Audience is not configured.");

            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(key));

            var credentials = new SigningCredentials(
                securityKey,
                SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,
                    user.UserId.ToString()),

                new Claim(ClaimTypes.Name, user.Name),

                new Claim(ClaimTypes.Email, user.Email),

                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    Convert.ToDouble(
                        _configuration["Jwt:ExpiryMinutes"] ?? "60")),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }
    }
}
