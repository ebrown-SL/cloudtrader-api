using System;

namespace CloudTrader.Api.Service.Exceptions
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
