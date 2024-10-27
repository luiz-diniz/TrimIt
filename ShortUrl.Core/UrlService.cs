using Base62;
using Microsoft.Extensions.Caching.Memory;
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
        private readonly IMemoryCache _cache;
        private readonly TimeSpan _expirationCacheTime = TimeSpan.FromMinutes(30);

        public UrlService(ILogger<UrlService> logger, IUrlRepository urlRepository, IMemoryCache cache)
        {
            _logger = logger;
            _urlRepository = urlRepository;
            _cache = cache;
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

                    _logger.LogDebug("Short URL created '{shortUrl}' for URL '{url}'.", shortUrl, url.Url);

                    if (_urlRepository.GetUrl(shortUrl) is not null)
                    {
                        _logger.LogDebug("Short URL '{shortUrl}' already found in the database. Another Short URL will be created.", shortUrl);
                        continue;
                    }

                    _logger.LogDebug("Short URL '{shortUrl}' not found in the database, proceeding with persistence flow.", shortUrl);

                    urlEntity.ShortUrl = shortUrl;
                }

                _urlRepository.Create(urlEntity);

                _logger.LogDebug("Short URL '{shortUrl}' created in the database.", urlEntity.ShortUrl);

                _cache.Set(urlEntity.ShortUrl, urlEntity.OriginalUrl, _expirationCacheTime);

                _logger.LogDebug("Short URL '{shortUrl}' added in the cache.", urlEntity.ShortUrl);

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

                var cachedUrl = _cache.Get(shortUrl);

                if (cachedUrl is null)
                {
                    _logger.LogDebug("Short URL '{shortUrl}' not found in the cache.", shortUrl);

                    var url = _urlRepository.GetUrl(shortUrl);

                    if(url is not null)
                    {
                        _logger.LogDebug("Short URL '{shortUrl}' found in the database.", shortUrl);

                        _urlRepository.UpdateClicks(shortUrl);

                        _logger.LogDebug("Short URL '{shortUrl}' clicks updated.", shortUrl);

                        _cache.Set(shortUrl, url, _expirationCacheTime);

                        _logger.LogDebug("Short URL '{shortUrl}' added in the cache with expiration in {expirationTime} minutes.", shortUrl, _expirationCacheTime.Minutes);

                        return url;
                    }

                    _logger.LogDebug("Short URL '{shortUrl}' not found in the database.", shortUrl);

                    return url;
                }

                _logger.LogDebug("Short URL '{shortUrl}' found in the cache.", shortUrl);

                _urlRepository.UpdateClicks(shortUrl);

                _logger.LogDebug("Short URL '{shortUrl}' clicks updated.", shortUrl);

                return cachedUrl.ToString();
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