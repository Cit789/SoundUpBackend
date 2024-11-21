namespace SoundUp.Contracts
{
    public class AddMusicInPlaylistRequest
    {
        public Guid MusicId { get; set; }
        public Guid PlaylistId { get; set; }
    }
}
