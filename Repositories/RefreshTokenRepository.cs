using Microsoft.EntityFrameworkCore;
using SoundUp.Interfaces.Auth;
using SoundUp.Interfaces.Repository;



namespace SoundUp.Repositories
{
    public class RefreshTokenRepository(ApplicationDbContext dbContext, IRefreshTokenProvider refreshTokenProvider) : IRefreshTokenRepository
    {
        private readonly IRefreshTokenProvider _refreshTokenProvider = refreshTokenProvider;
        private readonly ApplicationDbContext _dbcontext = dbContext;

        public async Task<string> UpdateToken(Guid UserId)
        {
            var UserTask = _dbcontext.AllUsers
                .Include(u => u.RefreshToken)
                .FirstOrDefaultAsync(u => u.Id == UserId);

           string NewToken = _refreshTokenProvider.GenerateToken();

           var User = await UserTask;

           if (User == null) return string.Empty;

           if (User.RefreshToken == null) return string.Empty;


            
           User.RefreshToken.Token = NewToken;
           User.RefreshToken.ExpiryDate = DateTime.UtcNow.AddDays(7);
           await _dbcontext.SaveChangesAsync();
           return NewToken;

        }
        public async Task<bool> IsValidRefreshToken(string token)
        {
            var Token = await _dbcontext.RefreshTokens
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Token == token);
            if(Token == null) return false;

            return Token.ExpiryDate > DateTime.UtcNow;
        }

    }
}
