using ShortUrl.Entities;

namespace ShortUrl.Repository.Interfaces
{
    public interface IPasswordResetGuidRepository
    {
        void Create(PasswordResetGuidEntity passwordResetGuid);
    }
}
