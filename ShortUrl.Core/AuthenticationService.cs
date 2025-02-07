using Microsoft.Extensions.Logging;
using ShortUrl.Core.DTO;
using ShortUrl.Core.Exceptions;
using ShortUrl.Core.Interfaces;
using ShortUrl.Repository.Interfaces;

namespace ShortUrl.Core
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ILogger<AuthenticationService> _logger;
        private readonly IUserService _userService;
        private readonly IPasswordService _passwordService;
        private readonly ITokenService _tokenService;

        public AuthenticationService(ILogger<AuthenticationService> logger, IUserService userService, IPasswordService passwordService, ITokenService tokenService)
        {
            _logger = logger;
            _userService = userService;
            _passwordService = passwordService;
            _tokenService = tokenService;
        }

        public AuthenticationResultDto Authenticate(LoginDto login)
        {
            try
            {
                var user = _userService.GetCredentialsByEmail(login.Email);

                var validPassword = _passwordService.VerifyPassword(login.Password, user.Password);

                if (validPassword)
                {
                    return new AuthenticationResultDto
                    {
                        Token = _tokenService.GenerateToken(user)
                    };                 
                }
                
                throw new InvalidUserCredentialsException("Invalid Username or Password");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}