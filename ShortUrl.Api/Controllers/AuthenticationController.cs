using Microsoft.AspNetCore.Mvc;
using ShortUrl.Core.DTO;
using ShortUrl.Core.Exceptions;
using ShortUrl.Core.Interfaces;
using System.Net;

namespace ShortUrl.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ApiControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(ILogger<AuthenticationController> logger, IAuthenticationService authenticationService)
        {
            _logger = logger;
            _authenticationService = authenticationService;
        }

        [HttpPost]
        public IActionResult Authenticate([FromBody] LoginDto loginDto)
        {
            try
            {
                var result = _authenticationService.Authenticate(loginDto);

                return Ok(result);
            }
            catch (InvalidUserCredentialsException ex)
            {                
                return ReturnError(HttpStatusCode.Unauthorized, "Invalid Username or Password");
            }
            catch (Exception ex)
            {
                return ReturnError(HttpStatusCode.InternalServerError, _logger, ex, "Error while authenticating user.");
            }
        }
    }
}
