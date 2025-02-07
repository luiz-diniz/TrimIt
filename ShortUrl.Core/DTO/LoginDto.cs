using System.ComponentModel.DataAnnotations;

namespace ShortUrl.Core.DTO
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        [MaxLength(100)]
        public string Password { get; set; }
    }
}
