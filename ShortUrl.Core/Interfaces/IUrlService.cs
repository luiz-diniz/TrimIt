using ShortUrl.Core.DTO;

namespace ShortUrl.Core.Interfaces
{
    public interface IUrlService
    {
        string Create(UrlDTO url);
        string GetUrl(string shortUrl);
    }
}
