using Microsoft.Extensions.Configuration;
using ShortUrl.Api.Interfaces;

namespace ShortUrl.Api
{
    public class ReCaptchaValidator : IReCaptchaValidator
    {
        private readonly ILogger<ReCaptchaValidator> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly string _api;
        private readonly string _secret;
        private readonly bool _validateCaptcha;

        private record ReCaptchaResult(bool success);

        public ReCaptchaValidator(ILogger<ReCaptchaValidator> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;

            _api = GetAppSettingsValue("ReCaptcha:Api", string.Empty);
            _secret = GetAppSettingsValue("ReCaptcha:Secret", string.Empty);
            _validateCaptcha = GetAppSettingsValue("ReCaptcha:Validate", true);
        }

        public async Task<bool> ValidateReCaptcha(string captchaResponse)
        {
            try
            {
                if (!_validateCaptcha)
                    return true;

                var client = _httpClientFactory.CreateClient();

                var urlWithParameters = $"{_api}?secret={_secret}&response={captchaResponse}";

                var response = await client.PostAsync(urlWithParameters, null);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    _logger.LogInformation("CAPTCHA JSON Reponse: {Json}", json);

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

        private T GetAppSettingsValue<T>(string key, T defaultValue)
        {
            var value = _configuration.GetValue(key, defaultValue);

            if (value is null)
                throw new ArgumentNullException($"Invalid Key {key} on the AppSettings.json");

            return value;
        }
    }
}
