using ShortUrl.Core.Interfaces;
using static BCrypt.Net.BCrypt;

namespace ShortUrl.Core
{
    public class PasswordService : IPasswordService
    {
        public string HashInputPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException(nameof(password), "Password null or empty.");

            return HashPassword(password);
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException(nameof(password), "Password is null or empty.");

            if (string.IsNullOrWhiteSpace(hashedPassword))
                throw new ArgumentNullException(nameof(hashedPassword), "Hashed Password is null or empty.");

            return Verify(password, hashedPassword);
        }
    }
}
