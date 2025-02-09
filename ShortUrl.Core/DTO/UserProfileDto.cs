namespace ShortUrl.Core.DTO
{
    public class UserProfileDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<UrlProfileDto> Urls { get; set; }
    }
}
