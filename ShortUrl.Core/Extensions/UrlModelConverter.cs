using ShortUrl.Core.DTO;
using ShortUrl.Entities;

namespace ShortUrl.Core.Extensions
{
    public static class UrlModelConverter
    {
        public static Url ToUrlEntity(this UrlDTO urlModel)
        {
            return new Url
            {
                OriginalUrl = urlModel.Url,
                ExpiryDate = urlModel.ExpiryDate ?? DateTime.UtcNow.AddDays(7)
            };
        }
    }
}
