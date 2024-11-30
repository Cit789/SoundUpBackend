namespace SoundUp.Contracts
{
    public record CreateAlbumRequest(string AlbumName,string Description,string Avatar,Guid AuthorId);
    
    
}
