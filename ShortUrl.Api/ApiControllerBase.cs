using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ShortUrl.Api
{
    public class ApiControllerBase : ControllerBase
    {
        protected IActionResult ReturnOk(string urlContent)
        {
            return StatusCode((int)HttpStatusCode.OK, new { 
                url = urlContent
            });
        }

        protected IActionResult ReturnError(HttpStatusCode status, ILogger logger, Exception ex)
        {
            logger.LogError(ex, ex.Message);
            return StatusCode((int)status, new
            {
                error = ex.Message
            });
        }
    }
}
