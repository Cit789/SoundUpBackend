using SoundUp.Models;

namespace SoundUp.Models
{
    public class MusicAudio
    {
        public MusicAudio()
        {
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }
        public Guid Id { get; set; }
        public string Audio { get; set; } = string.Empty;
        public Guid MusicId { get; set; }
        public Music? Music { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
