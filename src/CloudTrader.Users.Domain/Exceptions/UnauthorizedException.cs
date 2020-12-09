using System;

namespace CloudTrader.Users.Domain.Exceptions
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