namespace SoundUp.Dto
{
    public class UserDto(Guid Id, string Name, string Password, string UserType, string Avatar, DateTime CreatedAt, DateTime UpdatedAt)
    {

        public Guid Id { get; set; } = Id;
        public string Name { get; set; } = Name;
        public string Password { get; set; } = Password;
        public string UserType { get; set; } = UserType;
        public string Avatar { get; set; } = Avatar;
        public DateTime CreatedAt { get; set; } = CreatedAt;
        public DateTime UpdatedAt { get; set; } = UpdatedAt;
    }
}
//string Id 
//string Avatar 
//string Name 
//string Password 
//"User" | "UserAuthor" UserType
//DateTime CreatedAt 
//DateTime UpdatedAt 