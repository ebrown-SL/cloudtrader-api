using System;

namespace CloudTrader.Api.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string username)
            : base($"User with username \"{username}\" not found")
        {
        }

        public UserNotFoundException(int id)
            : base($"User with id \"{id}\" not found")
        {
        }
    }
}
