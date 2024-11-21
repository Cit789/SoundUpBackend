namespace SoundUp.Contracts
{
    public class AddMusicInFavouritesRequest
    {
        public Guid MusicId { get; set; }
        public Guid UserId { get; set; }
    }
}
