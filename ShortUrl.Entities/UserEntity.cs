namespace ShortUrl.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }

        public IEnumerable<UrlEntity> Urls { get; set; }
        public IEnumerable<PasswordResetGuidEntity> Guids { get; set; }
    }
}