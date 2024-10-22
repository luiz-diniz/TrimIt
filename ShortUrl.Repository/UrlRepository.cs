using Microsoft.EntityFrameworkCore;
using ShortUrl.Entities;
using ShortUrl.Repository.Interfaces;

namespace ShortUrl.Repository
{
    public class UrlRepository : IUrlRepository
    {
        private readonly ShortUrlContext _context;

        public UrlRepository(ShortUrlContext context)
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
    }
}
