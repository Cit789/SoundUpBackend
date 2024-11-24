using SoundUp.Dto;
using SoundUp.Models;

namespace SoundUp.Interfaces.Repository
{
    public interface IMusicRepository
    {
        Task<List<MusicDto>> GetAllMusicWithPagination(int Page, int PageSize,Guid UserId);
        Task<List<MusicDto>> GetMusicInPlaylist(int Page,int PageSize, Guid PlaylistId,Guid UserId);
        public Task<List<MusicDto>> GetCreatedAuthorMusic(int Page, int PageSize, Guid AuthorId, Guid UserId);
    }
}
