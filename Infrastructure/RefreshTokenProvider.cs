using SoundUp.Interfaces.Auth;
using System.Security.Cryptography;

namespace SoundUp.Infrastructure
{
    public class RefreshTokenProvider : IRefreshTokenProvider
    {
        public string GenerateToken()
        {
            var randomNumber = new byte[64];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
            }

            
            return Convert.ToBase64String(randomNumber);
        }
    }
}
