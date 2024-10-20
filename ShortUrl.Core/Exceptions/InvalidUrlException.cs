﻿namespace ShortUrl.Core.Exceptions
{
    public class InvalidUrlException : Exception
    {
        public InvalidUrlException(string? message) : base(message)
        {
        }
    }
}