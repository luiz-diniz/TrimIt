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
                return ReturnError(HttpStatusCode.BadRequest, _logger, ex);
            }
            catch (Exception ex)
            {
                return ReturnError(HttpStatusCode.InternalServerError, _logger, ex);

            }
        }

        [HttpGet]
        [Route("/{shortUrl}")]
        public IActionResult Get(string shortUrl)
        {
            try
            {
                var originalUrl = _urlService.GetUrl(shortUrl);

                return ReturnOk(originalUrl);
            }
            catch (InvalidUrlException ex)
            {
                return ReturnError(HttpStatusCode.BadRequest, _logger, ex);
            }
            catch (Exception ex)
            {
                return ReturnError(HttpStatusCode.InternalServerError, _logger, ex);

            }
        }
    }
}