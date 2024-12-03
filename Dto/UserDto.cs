namespace SoundUp.Dto
{
    public record UserDto(Guid Id, string Name, string Password, string UserType, string Avatar, DateTime CreatedAt, DateTime UpdatedAt);
   
}
//string Id 
//string Avatar 
//string Name 
//string Password 
//"User" | "UserAuthor" UserType
//DateTime CreatedAt 
//DateTime UpdatedAt 