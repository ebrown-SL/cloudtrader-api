using System;

namespace CloudTrader.Users.Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message = "The requested item could not be found") : base(message)
        {
        }
    }
}