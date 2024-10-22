using ShortUrl.Api.Interfaces;

namespace ShortUrl.Api
{
    public class ReCaptchaValidator : IReCaptchaValidator
    {
        private readonly ILogger<ReCaptchaValidator> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _api;
        private readonly string _secret;

        private record ReCaptchaResult(bool success);

        public ReCaptchaValidator(ILogger<ReCaptchaValidator> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;

            _api = configuration.GetValue("ReCaptcha:Api", string.Empty);

            if (_api is null)
                throw new ArgumentNullException("Invalid API URL on the AppSettings.json");

            _secret = configuration.GetValue("ReCaptcha:Secret", string.Empty);

            if (_secret is null)
                throw new ArgumentNullException("Invalid API Secret on the AppSettings.json");
        }

        public async Task<bool> ValidateReCaptcha(string captchaResponse)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();

                var urlWithParameters = $"{_api}?secret={_secret}&response={captchaResponse}";

                var response = await client.PostAsync(urlWithParameters, null);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    _logger.LogDebug("CAPTCHA JSON Reponse: {Json}", json);

                    var result = await response.Content.ReadFromJsonAsync<ReCaptchaResult>();

                    if (result is not null)
                    {
                        return result.success;
                    }

                    _logger.LogWarning("Invalid JSON returned from the CAPTCHA API.");
                    return false;
                }

                _logger.LogWarning("CAPTCHA API request failed: {StatusCode}", response.StatusCode);
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
