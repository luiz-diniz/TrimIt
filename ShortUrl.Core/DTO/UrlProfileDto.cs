﻿namespace ShortUrl.Core.DTO
{
    public class UrlProfileDto
    {
        public int Id { get; set; }
        public string OriginalUrl { get; set; }
        public string ShortUrl { get; set; }
        public int Clicks { get; set; }
        public DateTime? LastClick { get; set; }
        public DateTime ExpirationDateTime { get; set; }
    }
}