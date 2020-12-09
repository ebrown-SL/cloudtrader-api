using CloudTrader.Users.Domain.Exceptions;
using CloudTrader.Users.Domain.Helpers;
using CloudTrader.Users.Domain.Models;
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
            var user = await userRepository.GetUserByUsername(username);
            if (user == null)
            {
                throw new UnauthorizedException("Username or password is incorrect");
            }

            var success = passwordUtils.VerifyPassword(password, user.PasswordHash, user.PasswordSalt);
            if (!success)
            {
                throw new UnauthorizedException("Username or password is incorrect");
            }

            return user;
        }
    }
}