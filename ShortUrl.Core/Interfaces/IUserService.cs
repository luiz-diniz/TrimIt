using ShortUrl.Core.DTO;
using ShortUrl.Core.Models;

namespace ShortUrl.Core.Interfaces
{
    public interface IUserService
    {
        void Create(UserRegisterDTO user);
        UserCredentialsModel GetCredentialsByEmail(string email);
        UserProfileDto GetProfileById(int id);
        int GetIdByEmail(string email);
    }
}
