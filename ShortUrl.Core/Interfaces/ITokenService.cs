using ShortUrl.Core.Models;

namespace ShortUrl.Core.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(UserCredentialsModel userCredentials);
    }
}
