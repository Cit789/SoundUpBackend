namespace SoundUpRes.Models
{
    public class Album
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public UserAuthor? Author { get; set; }
        public Guid AuthorId { get; set; }
        public List<Music> AlbumMusic { get; set; } = [];
    }
}
