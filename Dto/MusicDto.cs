using SoundUp.Models;

namespace SoundUp.Dto
{

    public class MusicDto(Guid Id, Guid AuthorId, Guid AudioId, string Name, string Avatar, MusicCategories Category, DateTime CreatedAt, DateTime UpdatedAt,List<string> AuthorsNames)
    {

        public Guid Id { get; set; } = Id;
        public Guid AudioId { get; set; } = AudioId;
        public Guid AuthorId { get; set; } = AuthorId;
        public List<string> AuthorsNames { get; set; } = AuthorsNames;
        public string Name { get; set; } = Name;
        public string Avatar { get; set; } = Avatar;
        public MusicCategories Category { get; set; } = Category;
        public DateTime CreatedAt { get; set; } = CreatedAt;
        public DateTime UpdatedAt { get; set; } = UpdatedAt;
    }

}
