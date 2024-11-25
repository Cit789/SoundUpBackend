using SoundUp.Contracts;
using SoundUp.Interfaces.Repository;
using SoundUp.Models;

namespace SoundUp.Repositories
{
    public class PlaylistRepository(ApplicationDbContext dbcontext, IUserRepository userRepository) : IPlaylistRepository
    {
        private readonly ApplicationDbContext _dbcontext = dbcontext;
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<bool> CreatePlaylist(CreatePlaylistRequest createPlaylistRequest)
        {
            if(_userRepository.GetUserById(createPlaylistRequest.CreatorId) == null) return false;

            var playlist = new Playlist()
            {
                Id = Guid.NewGuid(),
                Name = createPlaylistRequest.Name,
                CreatorId = createPlaylistRequest.CreatorId,
                Avatar = createPlaylistRequest.Avatar
            };
            await _dbcontext.PlayLists.AddAsync(playlist);
            var Count = await _dbcontext.SaveChangesAsync();
            return Count != 0;
        }
    }
}