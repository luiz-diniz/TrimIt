using Microsoft.EntityFrameworkCore;
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

        public void Create(UserEntity user)
        {
            _context.Add(user);
            _context.SaveChanges();
        }
        public UserEntity? GetByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        public UserEntity? GetById(int id)
        {
            return _context.Users.Include(x => x.Urls).FirstOrDefault(u => u.Id == id);
        }

        public int GetIdByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email)?.Id ?? 0;
        }
    }
}
