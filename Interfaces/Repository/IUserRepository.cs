using SoundUp.Models;

namespace SoundUp.Interfaces.Repository
{
    public interface IUserRepository
    {
        Task<BaseUser?> GetUserById(Guid Id);
        Task<T?> GetUserById<T>(Guid Id) where T : BaseUser;
        Task<UserAuthor?> GetAuthorWithAlbumAndCreatedMusics(Guid Id);
    }
}
