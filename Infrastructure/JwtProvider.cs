using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SoundUp.Interfaces.Auth;
using SoundUp.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SoundUp.Infrastructure
{
    public class JwtProvider(IOptions<JwtOptions> options) : IJwtProvider
    {
        private readonly JwtOptions _options = options.Value;

        public string GenerateToken(BaseUser user)
        {
            Claim[] claims = { new("UserId",user.Id.ToString()) , new("UserName",user.Name)};

            var SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: SigningCredentials,
                expires: DateTime.UtcNow.AddHours(_options.ExpiresHours));
            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenValue;
        }
    }
}
