namespace SoundUpRes.Models
{
    public class UserAuthor : BaseUser
    {

        public List<Music> CreatedMusics { get; set; } = [];
        public List<Album> Albums { get; set; } = [];
    }
}
