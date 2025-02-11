namespace ShortUrl.Core.Interfaces
{
    public interface IEmailService
    {
        void SendResetPasswordEmail(string email, string guid);
    }
}