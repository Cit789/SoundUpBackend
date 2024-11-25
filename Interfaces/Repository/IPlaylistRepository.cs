using SoundUp.Contracts;

namespace SoundUp.Interfaces.Repository
{
    public interface IPlaylistRepository
    {
        Task<bool> CreatePlaylist(CreatePlaylistRequest createPlaylistRequest);
    }
}
