using Base62;
using Microsoft.AspNetCore.Mvc;
using ShortUrl.Entities;
using ShortUrl.Repository;
using System.Security.Cryptography;
using System.Text;

namespace ShortUrl.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UrlController : ControllerBase
    {
        private readonly ILogger<UrlController> _logger;

        public UrlController(ILogger<UrlController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("/{shortUrl}")]
        public IActionResult Get(string shortUrl)
        {
            var repository = new UrlRepository();

            var url = repository.GetUrl(shortUrl);

            return Ok(url);
        }

        [HttpPost]
        public IActionResult Create([FromBody] string url)
        {
            var repository = new UrlRepository();
            var shortUrl = string.Empty;

            using (var md5 = MD5.Create())
            {
                var urlBytes = Encoding.ASCII.GetBytes(url);
                shortUrl = md5.ComputeHash(urlBytes).ToBase62();
            }

            repository.Create(new Url
            {
                ExpiryDate = DateTime.UtcNow,
                OriginalUrl = url,
                ShortUrl = shortUrl
            });

            return Ok("yoururl.io/" + shortUrl);
        }
    }
}
