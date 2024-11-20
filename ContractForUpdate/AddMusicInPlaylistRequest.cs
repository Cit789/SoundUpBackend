namespace SoundUp.DtoForUpdate
{
    public class AddMusicInPlaylistRequest
    {
        public Guid MusicId { get; set; }
        public Guid PlaylistId { get; set; }
    }
}
