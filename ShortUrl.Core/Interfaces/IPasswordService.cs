namespace ShortUrl.Core.Interfaces
{
    public interface IPasswordHashService
    {
        string HashInputPassword(string password);
        bool VerifyPassword(string password, string hashedPassword);
    }
}
