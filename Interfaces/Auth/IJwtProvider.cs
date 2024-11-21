using SoundUp.Models;

namespace SoundUp.Interfaces.Auth
{
    public interface IJwtProvider
    {
       string GenerateToken(BaseUser user);
    }
}
