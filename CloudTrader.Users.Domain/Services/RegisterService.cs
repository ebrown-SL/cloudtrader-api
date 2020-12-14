using CloudTrader.Users.Domain.Exceptions;
using CloudTrader.Users.Domain.Helpers;
using CloudTrader.Users.Domain.Models;
using System.Threading.Tasks;

namespace CloudTrader.Users.Domain.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly IUserRepository userRepository;

        private readonly IPasswordUtils passwordUtils;

        private readonly ITraderApiClientTechDebt traderApiService;

        public RegisterService(
            IUserRepository userRepository,
            IPasswordUtils passwordUtils,
            ITraderApiClientTechDebt traderRepository)
        {
            this.userRepository = userRepository;
            this.passwordUtils = passwordUtils;
            this.traderApiService = traderRepository;
        }

        public async Task<User> Register(string username, string password)
        {
            var existingUser = await userRepository.GetUserByUsername(username);
            if (existingUser != null)
            {
                throw new UsernameAlreadyExistsException(username);
            }

            (byte[] passwordHash, byte[] passwordSalt) = passwordUtils.CreatePasswordHash(password);

            var traderId = await traderApiService.CreateTrader();

            var user = new User(username, passwordHash, passwordSalt, traderId);
            ; // TODO - are we not saving this user anywhere?

            return user;
        }
    }
}