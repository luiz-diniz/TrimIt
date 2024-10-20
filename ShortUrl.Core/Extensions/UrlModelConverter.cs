using ShortUrl.Core.Models;
using ShortUrl.Entities;

namespace ShortUrl.Core.Extensions
{
    public static class UrlModelConverter
    {
        public static Url ToUrlEntity(this UrlModel urlModel)
        {
            return new Url
            {
                OriginalUrl = urlModel.OriginalUrl,
                ExpiryDate = urlModel.ExpiryDate
            };
        }
    }
}
