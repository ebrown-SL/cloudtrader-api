using System;

namespace CloudTrader.Users.Domain.Exceptions
{
    public class UsernameAlreadyExistsException : Exception
    {
        public readonly string Username;

        public UsernameAlreadyExistsException(string username)
            : base($"Username \"{username}\" is already taken")
        {
            Username = username;
        }
    }
}