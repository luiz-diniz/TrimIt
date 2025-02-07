namespace ShortUrl.Core.Exceptions
{
    public class UserNotFound : Exception
    {
        public UserNotFound(string? message) : base(message)
        {
        }
    }
}
