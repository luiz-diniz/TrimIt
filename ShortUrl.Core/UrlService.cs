using Base62;
using Microsoft.Extensions.Logging;
using ShortUrl.Core.Exceptions;
using ShortUrl.Core.Extensions;
using ShortUrl.Core.Interfaces;
using ShortUrl.Core.Models;
using ShortUrl.Repository.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace ShortUrl.Core
{
    public class UrlService : IUrlService
    {
        private readonly ILogger _logger;
        private readonly IUrlRepository _urlRepository;

        public UrlService(ILogger<UrlService> logger, IUrlRepository urlRepository)
        {
            _logger = logger;
            _urlRepository = urlRepository;
        }

        public string Create(UrlModel url)
        {
            try
            {
                ValidateUrl(url);

                var urlEntity = url.ToUrlEntity();

                while (string.IsNullOrWhiteSpace(urlEntity.ShortUrl))
                {
                    var shortUrl = CreateShortUrl(url.Url);

                    if (_urlRepository.GetUrl(shortUrl) is not null)
                        continue;

                    urlEntity.ShortUrl = shortUrl;
                }

                _urlRepository.Create(urlEntity);

                return urlEntity.ShortUrl;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public string GetUrl(string shortUrl)
        {
            try
            {
                if (string.IsNullOrEmpty(shortUrl))
                    throw new InvalidUrlException("Invalid URL.");

                return _urlRepository.GetUrl(shortUrl);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        private string CreateShortUrl(string originalUrl)
        {
            using var md5 = MD5.Create();

            var urlBytes = Encoding.ASCII.GetBytes($"{originalUrl}{Guid.NewGuid()}");

            return md5.ComputeHash(urlBytes).ToBase62().Substring(0, 8);         
        }

        private void ValidateUrl(UrlModel url)
        {
            if(url is null)
                throw new InvalidUrlException("Invalid URL model object.");

            if (string.IsNullOrWhiteSpace(url.Url))
                throw new InvalidUrlException("URL null or empty.");
                       
            if (!Uri.TryCreate(url.Url, UriKind.Absolute, out Uri? uriResult))
                throw new InvalidUrlException("Invalid URL.");
        }
    }
}