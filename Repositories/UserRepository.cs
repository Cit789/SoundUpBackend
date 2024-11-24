using Microsoft.EntityFrameworkCore;
using SoundUp.Interfaces.Repository;
using SoundUp.Models;

namespace SoundUp.Repositories
{
    public class UserRepository(ApplicationDbContext dbcontext) : IUserRepository
    {
        private readonly ApplicationDbContext _dbcontext = dbcontext;
        public async Task<BaseUser?> GetUserById(Guid UserId)
        {
            return await _dbcontext.AllUsers.FirstOrDefaultAsync(x => x.Id == UserId);
        }

        public async Task<T?> GetUserById<T>(Guid UserId) where T : BaseUser
        {
            return await _dbcontext.Set<T>().FirstOrDefaultAsync(u => u.Id == UserId);
        }

        public async Task<UserAuthor?> GetAuthorWithAlbumAndCreatedMusics(Guid Id)
        {
            return await _dbcontext.UserAuthors
                .Include(u => u.CreatedMusics)
                .ThenInclude(m =>m.Album)
                .Include(u => u.CreatedMusics)
                .ThenInclude(m => m.WhoFavorited)
                .FirstOrDefaultAsync(u => u.Id == Id);
        }

    }
}
