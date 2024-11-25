using Microsoft.EntityFrameworkCore;
using SoundUp.Interfaces.Repository;
using SoundUp.Models;

namespace SoundUp.Repositories
{
    public class AuthorRepository(ApplicationDbContext dbcontext, IUserRepository usersRepository) : IAuthorRepository
    {
        private readonly ApplicationDbContext _dbcontext = dbcontext;
        private readonly IUserRepository _usersRepository = usersRepository;
    
        public async Task<UserAuthor?> GetAuthorWithAlbumAndCreatedMusics(Guid Id)
        {
            return await _dbcontext.UserAuthors
                .AsNoTracking()
                .Include(u => u.CreatedMusics)
                .ThenInclude(m => m.Album)
                .Include(u => u.CreatedMusics)
                .ThenInclude(m => m.WhoFavorited)
                .FirstOrDefaultAsync(u => u.Id == Id);
        }
        public async Task<bool> IsAuthorHasAlbum(Guid AuthorId, string AlbumName)
        {
            var Author = await _dbcontext.UserAuthors
                .AsNoTracking()
                .Include(a => a.Albums)
                .FirstOrDefaultAsync(a => a.Id == AuthorId);

            if (Author == null) return false;

            return Author.Albums.Any(a => a.Name == AlbumName);
        }

        public async Task<bool> IsAuthorCreateThisMusic(Guid AuthorId,string MusicName)
        {
            var Author = await _dbcontext.UserAuthors
                .AsNoTracking()
                .Include(a => a.CreatedMusics)
                .FirstOrDefaultAsync(a => a.Id == AuthorId);

            if (Author == null) return false;

            return Author.Albums.Any(a => a.Name == MusicName);
        }
    }
}
