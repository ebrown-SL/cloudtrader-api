using System;
namespace CloudTrader.Api.Exceptions
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
