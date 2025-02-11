using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ShortUrl.Core.DTO;
using ShortUrl.Core.Exceptions;
using ShortUrl.Core.Interfaces;
using ShortUrl.Entities;
using ShortUrl.Repository.Interfaces;

namespace ShortUrl.Core
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ILogger<AuthenticationService> _logger;
        private readonly IUserService _userService;
        private readonly IPasswordHashService _passwordHashService;
        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService;
        private readonly IPasswordResetGuidRepository _passwordResetGuidRepository;
        private readonly IConfiguration _configuration;

        public AuthenticationService(ILogger<AuthenticationService> logger, IUserService userService, IPasswordHashService passwordHashService, 
            ITokenService tokenService, IEmailService emailService, IPasswordResetGuidRepository passwordResetGuidRepository, IConfiguration configuration)
        {
            _logger = logger;
            _userService = userService;
            _passwordHashService = passwordHashService;
            _tokenService = tokenService;
            _emailService = emailService;
            _passwordResetGuidRepository = passwordResetGuidRepository;
            _configuration = configuration;
        }

        public AuthenticationResultDto Authenticate(LoginDto login)
        {
            try
            {
                var user = _userService.GetCredentialsByEmail(login.Email);

                var validPassword = _passwordHashService.VerifyPassword(login.Password, user.Password);

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

        public void RequestPasswordReset(ForgotPasswordDto forgotPassword)
        {
            try
            {
                var idUser = _userService.GetIdByEmail(forgotPassword.Email);

                var guid = Guid.NewGuid().ToString();

                var passwordResetGuidEntity = new PasswordResetGuidEntity
                {
                    IdUser = idUser,
                    Guid = guid,
                    ExpirationDateTime = DateTime.UtcNow.AddMinutes(_configuration.GetValue<int>("Authentication:PasswordResetGuidExpirationTimeMinutes"))
                };

                _passwordResetGuidRepository.Create(passwordResetGuidEntity);

                _emailService.SendResetPasswordEmail(forgotPassword.Email, guid);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public void ResetPassword(ResetPasswordDto forgotPassword)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}