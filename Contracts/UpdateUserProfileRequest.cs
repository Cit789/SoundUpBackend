namespace SoundUp.Contracts
{
    public record UpdateUserProfileRequest(Guid UserId,string NewAvatar,string NewUserName,string NewPassword);
    
}
