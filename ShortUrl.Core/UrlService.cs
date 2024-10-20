using Base62;
using ShortUrl.Core.Exceptions;
using ShortUrl.Core.Extensions;
using ShortUrl.Core.Interfaces;
using ShortUrl.Core.Models;
using ShortUrl.Repository;
using ShortUrl.Repository.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace ShortUrl.Core
{
    public class UrlService : IUrlService
    {
        private readonly IUrlRepository _urlRepository;

        public UrlService(ShortUrlContext context, IUrlRepository urlRepository)
        {
            _urlRepository = urlRepository;
        }

        public string Create(UrlModel url)
        {
            ValidateUrl(url);

            var urlEntity = url.ToUrlEntity();

            urlEntity.ShortUrl = CreateShortUrl(url.OriginalUrl);

            _urlRepository.Create(urlEntity);

            return urlEntity.ShortUrl;
        }

        public string GetUrl(string shortUrl)
        {
            if(string.IsNullOrEmpty(shortUrl))
                throw new InvalidUrlException("Invalid short URL.");

            return _urlRepository.GetUrl(shortUrl);
        }

        private string CreateShortUrl(string originalUrl)
        {
            using var md5 = MD5.Create();
            
            var urlBytes = Encoding.ASCII.GetBytes(originalUrl);

            return md5.ComputeHash(urlBytes).ToBase62();         
        }

        private void ValidateUrl(UrlModel url)
        {
            if(url is null)
                throw new InvalidUrlException("Invalid URL object.");

            if (string.IsNullOrWhiteSpace(url.OriginalUrl))
                throw new InvalidUrlException("Invalid URL.");
        }
    }
}
