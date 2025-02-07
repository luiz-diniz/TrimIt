namespace ShortUrl.Entities
{
    public class Url
    {
        public int Id { get; set; }
        public string OriginalUrl { get; set; }
        public string ShortUrl { get; set; }
        public int Clicks { get; set; }
        public DateTime? LastClick { get; set; }
        public DateTime ExpiryDate { get; set; }

        public int? IdUser { get; set; }
        public User User { get; set; }
    }
}
