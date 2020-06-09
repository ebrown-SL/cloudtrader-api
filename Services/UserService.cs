using System.Threading.Tasks;
using CloudTrader.Api.Helpers;
using CloudTrader.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CloudTrader.Api.Services
{
    public interface IUserService
    {
        Task<UserModel> Create(UserModel user);

        Task<UserModel> GetByUsername(string username);
    }

    public class UserService : IUserService
    {
        private readonly DataContext _context;

        public UserService(DataContext context)
        {
            _context = context;
        }

        public async Task<UserModel> Create(UserModel user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<UserModel> GetByUsername(string username)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Username == username);

            return user;
        }
    }
}
