using CloudTrader.Api.Service.Exceptions;
using CloudTrader.Api.Service.Interfaces;
using CloudTrader.Api.Service.Models;
using System.Threading.Tasks;

namespace CloudTrader.Api.Service.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly IUserRepository _userRepository;

        private readonly ITokenGenerator _tokenGenerator;

        private readonly IPasswordUtils _passwordUtils;

        private readonly ITraderApiClient _traderApiService;

        public RegisterService(
            IUserRepository userRepository,
            ITokenGenerator tokenGenerator,
            IPasswordUtils passwordUtils,
            ITraderApiClient traderRepository)
        {
            _userRepository = userRepository;
            _tokenGenerator = tokenGenerator;
            _passwordUtils = passwordUtils;
            _traderApiService = traderRepository;
        }

        public async Task<AuthDetails> Register(string username, string password)
        {
            var existingUser = await _userRepository.GetUserByName(username);
            if (existingUser != null)
            {
                throw new UsernameAlreadyExistsException(username);
            }

            (byte[] passwordHash, byte[] passwordSalt) = _passwordUtils.CreatePasswordHash(password);

            var traderId = await _traderApiService.CreateTrader();

            var user = new User
            {
                Username = username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                TraderId = traderId
            };

            var id = await _userRepository.SaveUser(user);

            var token = _tokenGenerator.GenerateToken(id);

            return new AuthDetails
            {
                Id = id,
                Username = user.Username,
                Token = token
            };
        }
    }
}