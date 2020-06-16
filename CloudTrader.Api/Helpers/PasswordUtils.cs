using System.Text;

namespace CloudTrader.Api.Helpers
{
    public interface IPasswordUtils
    {
        (byte[] passwordHash, byte[] passwordSalt) CreatePasswordHash(string password);

        bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt);
    }

    public class PasswordUtils : IPasswordUtils
    {
        public (byte[] passwordHash, byte[] passwordSalt) CreatePasswordHash(string password)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                var passwordSalt = hmac.Key;
                var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                return (passwordHash, passwordSalt);
            }
        }

        public bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
            }

            return true;
        }
    }
}
