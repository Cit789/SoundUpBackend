using SoundUp.Contracts;
using SoundUp.Dto;
using SoundUp.Models;

namespace SoundUp.Interfaces.Repository
{
    public interface IPlaylistRepository
    {
        Task<bool> CreatePlaylist(CreatePlaylistRequest createPlaylistRequest);
        Task<Playlist?> GetPlayListById(Guid PlaylistId);
        Task<List<PlaylistDto>> GetUserPlaylists(int Page, int PageSize, Guid UserId);
    }
}
