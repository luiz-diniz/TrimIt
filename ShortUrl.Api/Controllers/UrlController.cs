using Microsoft.AspNetCore.Mvc;
using ShortUrl.Core.Exceptions;
using ShortUrl.Core.Interfaces;
using ShortUrl.Core.Models;
using System.Net;

namespace ShortUrl.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UrlController : ApiControllerBase
    {
        private readonly ILogger<UrlController> _logger;
        private readonly IUrlService _urlService;

        public UrlController(ILogger<UrlController> logger, IUrlService urlService)
        {
            _logger = logger;
            _urlService = urlService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] UrlModel url)
        {
            try
            {
                var shortUrl = _urlService.Create(url);

                return ReturnOk("domain.com/" + shortUrl);
            }
            catch(InvalidUrlException ex)
            {
                return ReturnError(_logger, ex);
            }
            catch (Exception ex)
            {
                return ReturnError(_logger, ex);

            }
        }

        [HttpGet]
        [Route("/{shortUrl}")]
        public IActionResult Get(string shortUrl)
        {
            try
            {
                var url = _urlService.GetUrl(shortUrl);

                return ReturnOk(url);
            }
            catch (InvalidUrlException ex)
            {
                return ReturnError(_logger, ex);
            }
            catch (Exception ex)
            {
                return ReturnError(_logger, ex);

            }
        }
    }
}