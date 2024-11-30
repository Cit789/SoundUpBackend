namespace SoundUp.Interfaces.Repository
{
    public interface IRefreshTokenRepository
    {
        Task<bool> IsValidRefreshToken(string token);
        Task<string> UpdateToken(Guid UserId);
    }
}
