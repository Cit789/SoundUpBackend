namespace SoundUp.Dto
{
    public record PlaylistDto(Guid Id,string Avatar,string Name,Guid CreatorId,DateTime CreatedAt,DateTime UpdatedAt);
    
}
