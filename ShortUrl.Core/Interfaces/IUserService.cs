using ShortUrl.Core.DTO;

namespace ShortUrl.Core.Interfaces
{
    public interface IUserService
    {
        void Create(UserRegisterDTO user);
    }
}
