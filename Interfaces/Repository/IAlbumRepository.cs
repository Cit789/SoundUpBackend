using SoundUp.Contracts;

namespace SoundUp.Interfaces.Repository
{
    public interface IAlbumRepository
    {
        Task<bool> PostNewAlbum(CreateAlbumRequest RequestAlbum);
    }
}
