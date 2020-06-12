using System;

namespace CloudTrader.Api.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string username)
            : base($"User {username} not found")
        {
        }

        public UserNotFoundException(int id)
            : base($"User {id} not found")
        {
        }
    }
}
