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
            var url = _context.Urls.Where(x => x.ShortUrl == shortUrl)
                    .Select(x => x.OriginalUrl)
                    .FirstOrDefault();

            return url;
        }
    }
}
