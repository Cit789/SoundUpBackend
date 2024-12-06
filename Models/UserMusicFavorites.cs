namespace SoundUp.Models
{
    public class UserMusicFavorites
    {
        public Guid UserId { get; set; }
        public BaseUser User { get; set; } = null!;

        public Guid MusicId { get; set; }
        public Music Music { get; set; } = null!;
    }
}
