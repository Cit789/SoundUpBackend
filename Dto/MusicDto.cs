using SoundUp.Models;

namespace SoundUp.Dto
{

    public record MusicDto(Guid Id, Guid AuthorId, Guid AudioId,Guid AlbumId,string AlbumName, string Name, string Avatar, MusicCategories Category,bool IsFavourited, DateTime CreatedAt, DateTime UpdatedAt,List<string> AuthorsNames);
    

}
