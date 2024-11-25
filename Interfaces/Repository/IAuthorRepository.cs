using SoundUp.Models;

namespace SoundUp.Interfaces.Repository
{
    public interface IAuthorRepository
    {
        Task<UserAuthor?> GetAuthorWithAlbumAndCreatedMusics(Guid Id);
        Task<bool> IsAuthorHasAlbum(Guid AuthorId,string AlbumName);
        Task<bool> IsAuthorCreateThisMusic(Guid AuthorId, string MusicName);
    }
}
