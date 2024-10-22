using System.ComponentModel.DataAnnotations;

namespace ShortUrl.Core.Models
{
    public class UrlModel
    {
        [Required]
        public string Url { get; set; }
        [Required]
        public string Response { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}