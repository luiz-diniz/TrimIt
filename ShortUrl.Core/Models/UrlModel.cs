using System.ComponentModel.DataAnnotations;

namespace ShortUrl.Core.Models
{
    public class UrlModel
    {
        [Required]
        public string OriginalUrl { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}