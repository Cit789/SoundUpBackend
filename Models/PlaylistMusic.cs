namespace SoundUp.Models
{
    public class PlaylistMusic
    {
        public Guid PlaylistId { get; set; }
        public Playlist Playlist { get; set; } = null!;

        public Guid MusicId { get; set; }
        public Music Music { get; set; } = null!;
    }
}
