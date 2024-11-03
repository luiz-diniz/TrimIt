using ShortUrl.Entities;

namespace ShortUrl.Repository.Interfaces
{
    public interface IUrlRepository
    {
        void Create(Url url);
        string? GetUrl(string shortUrl);
        void UpdateClicks(string shortUrl);
        int DeleteExpiredUrls();
    }
}