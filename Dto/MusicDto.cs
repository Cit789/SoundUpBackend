using SoundUp.Models;

namespace SoundUp.Dto
{
    
        public class MusicDto
        {
            public Guid Id { get; set; }
            public Guid AudioId { get; set; }
            public string Name { get; set; } = string.Empty;
            public string Avatar { get; set; } = string.Empty;
            public MusicCategories Category { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime UpdatedAt { get; set; }
        }
    
}
