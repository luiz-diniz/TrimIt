using ShortUrl.Entities;

namespace ShortUrl.Repository.Interfaces
{
    public interface IUserRepository
    {
        void Create(User user);
    }
}
