namespace CloudTrader.Users.Domain.Helpers
{
    public interface IPasswordUtils
    {
        (byte[] passwordHash, byte[] passwordSalt) CreatePasswordHash(string password);

        bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt);
    }
}