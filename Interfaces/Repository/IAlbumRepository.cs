using SoundUp.Contracts;
using SoundUp.Dto;
using SoundUp.Models;

namespace SoundUp.Interfaces.Repository
{
    public interface IAlbumRepository
    {
        Task<bool> PostNewAlbum(CreateAlbumRequest RequestAlbum);
        Task<Album?> GetAlbumById(Guid AlbumId);
        Task<List<AlbumDto>> GetAllAlbums(int Page, int PageSize);
    }
}
