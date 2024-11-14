namespace SoundUpRes.Models
{
    public class Playlist
    {
        public Guid Id { get; set; }

        public string Avatar { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public Guid CreatorId { get; set; }
        public BaseUser? Creator { get; set; }
        public List<Music> MusicList { get; set; } = [];

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
