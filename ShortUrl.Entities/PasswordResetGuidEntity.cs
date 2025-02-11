namespace ShortUrl.Entities
{
    public class PasswordResetGuidEntity
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public string Guid { get; set; }
        public DateTime ExpirationDateTime { get; set; }

        public UserEntity User { get; set; }
    }
}
