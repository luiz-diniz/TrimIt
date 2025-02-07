namespace ShortUrl.Core.Exceptions
{
    public class InvalidUserCredentialsException : Exception
    {
        public InvalidUserCredentialsException(string? message) : base(message)
        {
        }
    }
}
