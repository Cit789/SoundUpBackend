namespace SoundUp.Contracts
{
    public record CreateAlbumRequest(string Name,string Description,string Avatar,Guid AuthorId);
    
    
}
