using System.ComponentModel.DataAnnotations;

namespace ShortUrl.Core.DTO
{
    public class ForgotPasswordDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
