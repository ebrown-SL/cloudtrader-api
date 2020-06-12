using System.Threading.Tasks;
using CloudTrader.Api.Helpers;
using CloudTrader.Api.Models;
using Microsoft.EntityFrameworkCore;
using CloudTrader.Api.Exceptions;

namespace CloudTrader.Api.Services
{
    public interface IUserService
    {
        Task<UserModel> CreateUser(string username, string password);

        Task<UserModel> GetUser(int id);

        Task<UserModel> GetUser(string username);

        Task<bool> UserExists(string username);
    }

    public class UserService : IUserService
    {
        private readonly DataContext _context;

        private readonly IAuthenticationService _authenticationService;

        public UserService(DataContext context, IAuthenticationService authenticationService)
        {
            _context = context;
            _authenticationService = authenticationService;
        }

        public async Task<UserModel> CreateUser(string username, string password)
        {
            if (await UserExists(username))
            {
                throw new UsernameAlreadyExistsException(username);
            }

            _authenticationService.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new UserModel
            {
                Username = username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<UserModel> GetUser(int id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                throw new UserNotFoundException(id);
            }

            return user;
        }

        public async Task<UserModel> GetUser(string username)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Username == username);
            if (user == null)
            {
                throw new UserNotFoundException(username);
            }

            return user;
        }

        public async Task<bool> UserExists(string username)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Username == username);
            if (user == null)
            {
                return false;
            }

            return true;
        }
    }
}
