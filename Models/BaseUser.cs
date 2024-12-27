namespace SoundUp.Models
{
    public abstract class BaseUser
    {
        public BaseUser()
        {
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }
        public Guid Id { get; set; }
        public string Avatar { get; set; } = string.Empty;
       
        public string Name { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public RefreshToken? RefreshToken { get; set; }
        public Guid RefreshTokenId {  get; set; }
        public List<Album> FavoriteAlbums { get; set; } = [];
        public List<UserMusicFavorites> Favorites { get; set; } = [];
        public List<Playlist> Playlists { get; set; } = [];
        public ListenHistory? ListenHistory { get; set; }
        public Guid ListenHistoryId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
