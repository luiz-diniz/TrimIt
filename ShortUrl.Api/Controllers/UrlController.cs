using Base62;
using Microsoft.AspNetCore.Mvc;
using ShortUrl.Core.Interfaces;
using ShortUrl.Core.Models;
using ShortUrl.Entities;
using ShortUrl.Repository;
using ShortUrl.Repository.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace ShortUrl.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UrlController : ControllerBase
    {
        private readonly ILogger<UrlController> _logger;
        private readonly IUrlService _urlService;

        public UrlController(ILogger<UrlController> logger, IUrlService urlService)
        {
            _logger = logger;
            _urlService = urlService;
        }

        [HttpGet]
        [Route("/{shortUrl}")]
        public IActionResult Get(string shortUrl)
        {
            try
            {
                var url = _urlService.GetUrl(shortUrl);

                return Ok(url);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] UrlModel url)
        {
            try
            {
                var shortUrl = _urlService.Create(url);

                return Ok("yoururl.io/" + shortUrl);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest();
            }
        }
    }
}