using ShortUrl.Entities;
using System;

namespace ShortUrl.Repository
{
    public class UrlRepository
    {
        public void Create(Url url)
        {
            using (var context = new ShortUrlContext())
            {
                context.Add(url);
                context.SaveChanges();
            }
        }

        public string GetUrl(string shortUrl)
        {
            using (var context = new ShortUrlContext())
            {
                var url = context.Urls.Where(x => x.ShortUrl == shortUrl)
                    .Select(x => x.OriginalUrl)
                    .FirstOrDefault();

                return url;
            }
        }
    }
}
