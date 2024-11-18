using SoundUp.Models;

namespace SoundUp.Models
{
    public class MusicAudio
    {
        public Guid Id { get; set; }
        public string Audio { get; set; } = string.Empty;
        public Guid MusicId { get; set; }
        public Music? Music { get; set; }
    }
}
