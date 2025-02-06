using ShortUrl.Entities;
using ShortUrl.Repository.Interfaces;

namespace ShortUrl.Repository
{
    public class UrlRepository : IUrlRepository
    {
        private readonly AppDbContext _context;

        public UrlRepository(AppDbContext context)
        {
            _context = context;
        }
        public void Create(Url url)
        {
            _context.Add(url);
            _context.SaveChanges();         
        }

        public string? GetUrl(string shortUrl)
        {
            var url = _context.Urls.FirstOrDefault(x => x.ShortUrl == shortUrl);

            if(url is null)
                return null;

            url.Clicks += 1;
            url.LastClick = DateTime.UtcNow;

            _context.SaveChanges();

            return url.OriginalUrl;
        }

        public void UpdateClicks(string shortUrl)
        {
            var url = _context.Urls.FirstOrDefault(x => x.ShortUrl == shortUrl);

            url.Clicks += 1;
            url.LastClick = DateTime.UtcNow;

            _context.SaveChanges();
        }

        public int DeleteExpiredUrls()
        {
            var expiredUrls = _context.Urls.Where(x => x.ExpiryDate < DateTime.UtcNow).ToList();

            var expiredUrlsCount = expiredUrls.Count();

            if(expiredUrls is not null && expiredUrls.Count > 0)
            {
                _context.RemoveRange(expiredUrls);
                _context.SaveChanges();
            }   
            
            return expiredUrlsCount;
        }
    }
}
