namespace SoundUpRes.Models
{
    public class Music
    {
        public Guid Id { get; set; }
        public Guid AuthorId { get; set; }
        public UserAuthor? Author { get; set; }
        public string Avatar { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Audio { get; set; } = string.Empty;

        public MusicCategories Category { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<Playlist> MusicInPlaylists { get; set; } = [];
        public List<BaseUser> WhoFavorited { get; set; } = [];

        public Album? Album { get; set; }
        public Guid AlbumId { get; set; }

    }
}
