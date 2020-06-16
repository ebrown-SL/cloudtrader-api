using System.Threading.Tasks;
using CloudTrader.Api.Exceptions;
using CloudTrader.Api.Helpers;
using CloudTrader.Api.Models;

namespace CloudTrader.Api.Services
{
    public interface IRegisterService
    {
        Task<AuthDetails> Register(string username, string password);
    }

    public class RegisterService : IRegisterService
    {
        private readonly IUserRepository _userRepository;

        private readonly ITokenGenerator _tokenGenerator;

        private readonly IPasswordUtils _passwordUtils;

        public RegisterService(IUserRepository userRepository, ITokenGenerator tokenGenerator, IPasswordUtils passwordUtils)
        {
            _userRepository = userRepository;
            _tokenGenerator = tokenGenerator;
            _passwordUtils = passwordUtils;
        }

        public async Task<AuthDetails> Register(string username, string password)
        {
            var existingUser = await _userRepository.GetUser(username);
            if (existingUser != null)
            {
                throw new UsernameAlreadyExistsException(username);
            }

            (byte[] passwordHash, byte[] passwordSalt) = _passwordUtils.CreatePasswordHash(password);

            var user = new User
            {
                Username = username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            //await _traderService.CreateTrader(user.Id);

            await _userRepository.SaveUser(user);

            var token = _tokenGenerator.GenerateToken(user.Id);

            return new AuthDetails
            {
                Id = user.Id,
                Username = user.Username,
                Token = token
            };
        }
    }
}
