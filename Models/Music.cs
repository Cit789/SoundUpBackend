

namespace SoundUp.Models
{
    public class Music
    {
        public Music()
        {
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }
        public Guid Id { get; set; }
        public Guid AuthorId { get; set; }
        public UserAuthor? Author { get; set; }
        public string Avatar { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public MusicCategories Category { get; set; }
        public MusicAudio? MusicAudio { get; set; }
        public Guid MusicAudioId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<ListenHistory> ListenHistories { get; set; } = [];
        public List<Playlist> MusicInPlaylists { get; set; } = [];
        public List<BaseUser> WhoFavorited { get; set; } = [];

        public Album? Album { get; set; }
        public Guid AlbumId { get; set; }

    }
}
