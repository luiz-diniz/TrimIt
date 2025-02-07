using ShortUrl.Core.Enums;

namespace ShortUrl.Core.Models
{
    public class UserCredentialsModel
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public UserRoleEnum Role { get; set; }
    }
}