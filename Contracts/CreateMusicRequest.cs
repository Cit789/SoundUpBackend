using SoundUp.Models;

namespace SoundUp.Contracts
{
    public record CreateMusicRequest(Guid AuthorId,Guid AlbumId,string Name,string Avatar,string Audio,MusicCategories Category);
}
