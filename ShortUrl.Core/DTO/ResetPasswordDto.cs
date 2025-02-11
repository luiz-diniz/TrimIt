using System.ComponentModel.DataAnnotations;

namespace ShortUrl.Core.DTO
{
    public class ResetPasswordDto
    {
        [Required]
        public string Token { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "The password and confirmation doesn't match.")]
        public string ConfirmPassword { get; set; }
    }
}
