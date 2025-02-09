using ShortUrl.Entities;

namespace ShortUrl.Repository.Interfaces
{
    public interface IUserRepository
    {
        void Create(UserEntity user);
        UserEntity? GetByEmail(string email);
        UserEntity? GetById(int id);
    }
}
