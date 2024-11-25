using Microsoft.EntityFrameworkCore;
using SoundUp.Contracts;
using SoundUp.Infrastructure;
using SoundUp.Interfaces.Auth;
using SoundUp.Interfaces.Repository;
using SoundUp.Models;

namespace SoundUp.Repositories
{
    public class UserRepository(ApplicationDbContext dbcontext, IPasswordHasher passwordHasher, IJwtProvider jwtProvider) : IUserRepository
    {
        private readonly ApplicationDbContext _dbcontext = dbcontext;
        private readonly IPasswordHasher _passwordHasher = passwordHasher;
        private readonly IJwtProvider _jwtProvider = jwtProvider;
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

        public async Task<string> CreateUser<T>(CreateAuthorOrUserRequest createUserRequest) where T : BaseUser, new()
        {
            var HashPassword = _passwordHasher.Generate(createUserRequest.Password);
            var UserId = Guid.NewGuid();
            var ListenHistoryId = Guid.NewGuid();
            var ListenHistory = new ListenHistory()
            {
                Id = ListenHistoryId,
                UserId = UserId
            };
            T NewUser = new()
            {
                Name = createUserRequest.Name,
                Password = HashPassword,
                Avatar = createUserRequest.Avatar,
                Id = UserId,
                ListenHistory = ListenHistory,
                ListenHistoryId = ListenHistoryId

            };

            await _dbcontext.Set<T>().AddAsync(NewUser);
            var count = await _dbcontext.SaveChangesAsync();

            
            var token = _jwtProvider.GenerateToken(NewUser);
            return count == 0 ? string.Empty : token;
        }

    }
}
