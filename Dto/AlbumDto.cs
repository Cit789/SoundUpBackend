namespace SoundUp.Dto
{
    public record AlbumDto(Guid Id,Guid AuthorId,string Name,string Description,DateTime CreatedAt,DateTime UpdatedAt);
    
}
