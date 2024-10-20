using ShortUrl.Core.Models;
using ShortUrl.Entities;

namespace ShortUrl.Core.Interfaces
{
    public interface IUrlService
    {
        string Create(UrlModel url);
        string GetUrl(string shortUrl);
    }
}
