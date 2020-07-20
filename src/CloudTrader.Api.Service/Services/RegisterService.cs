using System.Net.Http;
using System.Threading.Tasks;
using CloudTrader.Api.Service.Exceptions;
using CloudTrader.Api.Service.Interfaces;
using CloudTrader.Api.Service.Models;

namespace CloudTrader.Api.Service.Services
{
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

            // Make POST request to the traders API to create new trader
            using var client = new HttpClient();
            var url = "https://localhost44399/api/trader";

            var response = await client.PostAsync(url, null);

            string result = response.Content.ReadAsStringAsync().Result;

            var user = new User
            {
                Username = username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
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
