namespace SoundUpRes.Models
{
    public abstract class BaseUser
    {
        public Guid Id { get; set; }
        public string Avatar { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;


        public List<Music> Favorites { get; set; } = [];
        public List<Playlist> Playlists { get; set; } = [];

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
