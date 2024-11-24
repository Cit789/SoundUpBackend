using SoundUp.Models;

namespace SoundUp.Dto
{

    public class MusicDto(Guid Id, Guid AuthorId, Guid AudioId,Guid AlbumId,string AlbumName, string Name, string Avatar, MusicCategories Category,bool IsFavourited, DateTime CreatedAt, DateTime UpdatedAt,List<string> AuthorsNames)
    {

        public Guid Id { get; set; } = Id;
        public Guid AudioId { get; set; } = AudioId;
        public Guid AuthorId { get; set; } = AuthorId;
        public Guid AlbumId { get; set; } = AlbumId;
        public bool IsFavourited { get; set; } = IsFavourited;
        public string AlbumName { get; set; } = AlbumName;
        public List<string> AuthorsNames { get; set; } = AuthorsNames;
        public string Name { get; set; } = Name;
        public string Avatar { get; set; } = Avatar;
        public MusicCategories Category { get; set; } = Category;
        public DateTime CreatedAt { get; set; } = CreatedAt;
        public DateTime UpdatedAt { get; set; } = UpdatedAt;
    }

}
