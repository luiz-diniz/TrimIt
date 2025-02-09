using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ShortUrl.Api
{
    public class ApiControllerBase : ControllerBase
    {
        protected IActionResult ReturnError(HttpStatusCode status, string message)
        {
            return StatusCode((int)status, new
            {
                error = message
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

        protected IActionResult ReturnError(HttpStatusCode status, ILogger logger, Exception ex, string message)
        {
            logger.LogError(ex, ex.Message);
            return StatusCode((int)status, new
            {
                error = message
            });
        }
    }
}
