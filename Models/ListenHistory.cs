namespace SoundUp.Models
{
    public class ListenHistory
    {
        
        public Guid Id { get; set; }
        public List<Music> MusicHistory { get; set; } = [];
        public BaseUser? User { get; set; }
        public Guid UserId { get; set; }
    }
}
