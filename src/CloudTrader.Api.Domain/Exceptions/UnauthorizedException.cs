using System;

namespace CloudTrader.Api.Domain.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException()
            : base($"Unauthorized")
        {
        }

        public UnauthorizedException(string message)
            : base(message)
        {
        }
    }
}