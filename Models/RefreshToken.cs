namespace SoundUp.Models
{
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public string Token { get; set; } = string.Empty;
        public BaseUser? User { get; set; }
        public Guid UserId { get; set; }
        public DateTime ExpiryDate { get; set; } 
        public bool IsRevoked { get; set; }
    }
}
