namespace SoundUp.Interfaces.Auth
{
    public interface IRefreshTokenProvider
    {
        string GenerateToken();
    }
}
