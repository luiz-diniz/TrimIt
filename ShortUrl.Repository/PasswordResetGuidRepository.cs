using ShortUrl.Entities;
using ShortUrl.Repository.Interfaces;

namespace ShortUrl.Repository
{
    public class PasswordResetGuidRepository : IPasswordResetGuidRepository
    {
        private readonly AppDbContext _context;

        public PasswordResetGuidRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Create(PasswordResetGuidEntity passwordResetGuid)
        {
            _context.Add(passwordResetGuid);
            _context.SaveChanges();
        }
    }
}
