using System.ComponentModel.DataAnnotations;

namespace ShortUrl.Core.DTO
{
    public class UserRegisterDTO
    {
        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        [MaxLength(100)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "The password and confirmation doesn't match.")]
        public string ConfirmPassword { get; set; }
    }
}
