using ShortUrl.Core.DTO;
using ShortUrl.Core.Models;
using ShortUrl.Entities;

namespace ShortUrl.Core.Interfaces
{
    public interface IUserService
    {
        void Create(UserRegisterDTO user);
        UserCredentialsModel GetCredentialsByEmail(string email);
    }
}
