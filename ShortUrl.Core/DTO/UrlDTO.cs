using System.ComponentModel.DataAnnotations;

namespace ShortUrl.Core.DTO
{
    public class UrlDTO
    {
        [Required]
        [MinLength(2)]
        public string Url { get; set; }

        [Required]
        public string CaptchaResponse { get; set; }

        public DateTime? ExpirationDateTime { get; set; }

        public int? IdUser { get; set; }
    }
}