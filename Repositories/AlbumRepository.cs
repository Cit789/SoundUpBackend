using SoundUp.Contracts;
using SoundUp.Interfaces.Repository;
using SoundUp.Models;

namespace SoundUp.Repositories
{
    public class AlbumRepository(ApplicationDbContext dbcontext, IUserRepository usersRepository) : IAlbumRepository
    {
        private readonly ApplicationDbContext _dbcontext = dbcontext;
        private readonly IUserRepository _usersRepository = usersRepository;

        public async Task<bool> PostNewAlbum(CreateAlbumRequest RequestAlbum)
        {
            var NewAlbum = new Album()
            {
                Id = Guid.NewGuid(),
                Name = RequestAlbum.AlbumName,
                Avatar = RequestAlbum.Avatar,
                Description = RequestAlbum.Description,
                AuthorId = RequestAlbum.AuthorId,

            };
            await _dbcontext.Albums.AddAsync(NewAlbum);
            var Count = await _dbcontext.SaveChangesAsync();

            return Count != 0;
        }
    }
}