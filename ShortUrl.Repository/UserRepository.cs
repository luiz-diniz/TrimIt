using ShortUrl.Entities;
using ShortUrl.Repository.Interfaces;

namespace ShortUrl.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Create(User user)
        {
            _context.Add(user);
            _context.SaveChanges();
        }
    }
}
