using ShortUrl.Core.DTO;

namespace ShortUrl.Core.Interfaces
{
    public interface IAuthenticationService
    {
        AuthenticationResultDto Authenticate(LoginDto login);
        void RequestPasswordReset(ForgotPasswordDto forgotPassword);
        void ResetPassword(ResetPasswordDto forgotPassword);
    }
}
