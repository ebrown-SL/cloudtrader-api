using CloudTrader.Users.Domain.Exceptions;
using CloudTrader.Users.Domain.Helpers;
using CloudTrader.Users.Domain.Models;
using System.Text;
using System.Threading.Tasks;

namespace CloudTrader.Users.Domain.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository userRepository;

        private readonly IPasswordUtils passwordUtils;

        public LoginService(IUserRepository userRepository, IPasswordUtils passwordUtils)
        {
            this.userRepository = userRepository;
            this.passwordUtils = passwordUtils;
        }

        public async Task<User> Login(string username, string password)
        {
            var user = await userRepository.GetUserByUsername(username).ConfigureAwait(false);
            var safePasswordHash = user?.PasswordHash ?? Encoding.UTF8.GetBytes("not a real password");
            var safePasswordSalt = user?.PasswordSalt ?? Encoding.UTF8.GetBytes("not a real salt");
            bool validPassword = passwordUtils.VerifyPassword(password, safePasswordHash, safePasswordSalt);
            if (user is null || !validPassword)
            {
                throw new UnauthorizedException("User could not be authenticated");
            }

            return user;
        }
    }
}