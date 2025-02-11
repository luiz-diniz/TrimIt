using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShortUrl.Api.Interfaces;
using ShortUrl.Core.DTO;
using ShortUrl.Core.Exceptions;
using ShortUrl.Core.Interfaces;
using System.Net;

namespace ShortUrl.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UrlController : ApiControllerBase
{
    private readonly ILogger<UrlController> _logger;
    private readonly IUrlService _urlService;
    private readonly IReCaptchaValidator _reCaptchaValidator;

    public UrlController(ILogger<UrlController> logger, IUrlService urlService, IReCaptchaValidator reCaptchaValidator)
    {
        _logger = logger;
        _urlService = urlService;
        _reCaptchaValidator = reCaptchaValidator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UrlDTO url)
    {
        try
        {
            if (!await _reCaptchaValidator.ValidateReCaptcha(url.CaptchaResponse))
                return ReturnError(HttpStatusCode.BadRequest, "Invalid Captcha.");

            var shortUrl = _urlService.Create(url);

            return Ok(new
            {
                shortUrl
            });
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

    [HttpGet]
    [Route("{shortUrl}")]
    public IActionResult Get(string shortUrl)
    {
        try
        {
            var originalUrl = _urlService.GetUrl(shortUrl);

            return Ok(new
            {
                url = originalUrl
            });
        }
        catch (InvalidUrlException ex)
        {
            return ReturnError(HttpStatusCode.BadRequest, _logger, ex);
        }
        catch (Exception ex)
        {
            return ReturnError(HttpStatusCode.InternalServerError, _logger, ex, "Error getting URL.");
        }
    }
}