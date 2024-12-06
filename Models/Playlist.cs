namespace SoundUp.Models
{
    public class Playlist
    {
        public Playlist()
        {
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }
        public Guid Id { get; set; }

        public string Avatar { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public Guid CreatorId { get; set; }
        public BaseUser? Creator { get; set; }
        public List<PlaylistMusic> MusicList { get; set; } = [];

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
