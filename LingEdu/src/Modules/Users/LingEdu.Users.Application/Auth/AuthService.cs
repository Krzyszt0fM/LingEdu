using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LingEdu.Users.Application.Users;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
namespace LingEdu.Users.Application.Auth
{
    public class AuthService : IAuthService
    {
        private readonly JwtOptions _options;

        public AuthService(IOptions<JwtOptions> options)
        {
            _options = options.Value;
        }

        public string GenerateToken(UserDto user)
        {
            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim("isActive", user.IsActive.ToString().ToLowerInvariant())
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_options.ExpirationMinutes),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
