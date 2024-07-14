using System.ComponentModel.DataAnnotations;

namespace MoneyDreamAPI.Dto.AuthDto
{
    public class AuthRequest
    {
        [Required]
        public string? Username { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
