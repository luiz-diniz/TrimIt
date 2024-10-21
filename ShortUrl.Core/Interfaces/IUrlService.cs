using ShortUrl.Core.Models;

namespace ShortUrl.Core.Interfaces
{
    public interface IUrlService
    {
        string Create(UrlModel url);
        string GetUrl(string shortUrl);
    }
}
