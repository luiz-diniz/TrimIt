namespace ShortUrl.Core.Interfaces
{
    public interface IPasswordService
    {
        string HashInputPassword(string password);
        bool VerifyPassword(string password, string hashedPassword);
    }
}
