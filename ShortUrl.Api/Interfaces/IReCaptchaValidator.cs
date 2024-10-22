namespace ShortUrl.Api.Interfaces
{
    public interface IReCaptchaValidator
    {
        Task<bool> ValidateReCaptcha(string response);
    }
}