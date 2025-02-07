using ShortUrl.Entities;

namespace ShortUrl.Repository.Interfaces
{
    public interface IUrlRepository
    {
        void Create(UrlEntity url);
        string? GetUrl(string shortUrl);
        void UpdateClicks(string shortUrl);
        int DeleteExpiredUrls();
    }
}