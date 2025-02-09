using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShortUrl.Core.DTO;
using ShortUrl.Core.Interfaces;
using System.Net;

namespace ShortUrl.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ApiControllerBase
{
    private readonly ILogger _logger;
    private readonly IUserService _userService;

    public UserController(ILogger<UserController> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    [HttpPost]
    public IActionResult Create([FromBody] UserRegisterDTO userRegisterDto)
    {
        try
        {
            _userService.Create(userRegisterDto);

            return Ok();
        }
        catch (Exception ex)
        {
            return ReturnError(HttpStatusCode.InternalServerError, _logger, ex, "Error creating user.");
        }
    }

    [HttpGet("{id}")]
    [Authorize]
    public IActionResult Get(int id)
    {
        try
        {
            var userProfile = _userService.GetProfileById(id);

            return Ok(userProfile);
        }
        catch (Exception ex)
        {
            return ReturnError(HttpStatusCode.InternalServerError, _logger, ex, "Error getting the user profile.");
        }
    }
}