using Microsoft.EntityFrameworkCore;
using SoundUp.Contracts;
using SoundUp.Dto;
using SoundUp.Infrastructure;
using SoundUp.Interfaces.Auth;
using SoundUp.Interfaces.Repository;
using SoundUp.Models;

namespace SoundUp.Repositories
{
    public class UserRepository(
        ApplicationDbContext dbcontext,
        IPasswordHasher passwordHasher,
        IRefreshTokenProvider refreshTokenProvider,
        IJwtProvider jwtProvider) : IUserRepository
    {
        private readonly ApplicationDbContext _dbcontext = dbcontext;
        private readonly IPasswordHasher _passwordHasher = passwordHasher;
        private readonly IJwtProvider _jwtProvider = jwtProvider;
        private readonly IRefreshTokenProvider _refreshTokenProvider = refreshTokenProvider;
        public async Task<BaseUser?> GetUserById(Guid UserId)
        {
            return await _dbcontext.AllUsers
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == UserId);
        }

        public async Task<T?> GetUserById<T>(Guid UserId) where T : BaseUser
        {
            return await _dbcontext.Set<T>()
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == UserId);
        }

        

        public async Task<BaseUser?> GetUserByUserName(string UserName)
        {
            return await _dbcontext.AllUsers
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Name == UserName);
        }

        public async Task<TokensDto> CreateUser<T>(CreateAuthorOrUserRequest createUserRequest) where T : BaseUser, new()
        {
            var HashPassword = _passwordHasher.Generate(createUserRequest.Password);
            var UserId = Guid.NewGuid();
            var ListenHistoryId = Guid.NewGuid();
            var RefreshTokenId = Guid.NewGuid();
            var RefreshTokenValue = _refreshTokenProvider.GenerateToken();

            var RefreshToken = new RefreshToken()
            {
                Id = RefreshTokenId,
                Token = RefreshTokenValue,
                UserId = UserId,
                ExpiryDate = DateTime.UtcNow,

            };
            var ListenHistory = new ListenHistory()
            {
                Id = ListenHistoryId,
                UserId = UserId,
   
                
            };
            
            T NewUser = new()
            {
                Name = createUserRequest.Name,
                Password = HashPassword,
                Avatar = createUserRequest.Avatar,
                Id = UserId,

                RefreshToken = RefreshToken,
                RefreshTokenId = RefreshTokenId,

                ListenHistory = ListenHistory,
                ListenHistoryId = ListenHistoryId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,

            };

            await _dbcontext.Set<T>().AddAsync(NewUser);
            var count = await _dbcontext.SaveChangesAsync();

            
            var JwtToken = _jwtProvider.GenerateToken(NewUser);
            return count == 0 ? new TokensDto(string.Empty,string.Empty) : new TokensDto(RefreshTokenValue,JwtToken);
        }

    }
}
