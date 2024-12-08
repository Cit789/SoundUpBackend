using SoundUp.Contracts;
using SoundUp.Dto;
using SoundUp.Models;

namespace SoundUp.Interfaces.Repository
{
    public interface IAlbumRepository
    {
        Task<Album?> PostNewAlbum(CreateAlbumRequest RequestAlbum);
        Task<Album?> GetAlbumById(Guid AlbumId);
        Task<List<AlbumDto>> GetAllAlbums(int Page, int PageSize);
        Task<List<AlbumDto>> GetAlbumsFromAuthor(int Page, int PageSize, Guid AuthorId);
    }
}
