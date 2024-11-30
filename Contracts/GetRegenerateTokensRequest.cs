using System.ComponentModel.DataAnnotations;

namespace SoundUp.Contracts
{
    public class GetRegenerateTokensRequest
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public string RefreshToken { get; set; } =string.Empty;
    }
}
