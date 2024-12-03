using SoundUp.Contracts;
using SoundUp.Models;

namespace SoundUp.Interfaces.Repository
{
    public interface IAlbumRepository
    {
        Task<bool> PostNewAlbum(CreateAlbumRequest RequestAlbum);
        Task<Album?> GetAlbumById(Guid AlbumId);
    }
}
