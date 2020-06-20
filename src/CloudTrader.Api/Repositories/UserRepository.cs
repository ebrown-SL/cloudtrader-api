using System.Threading.Tasks;
using CloudTrader.Api.Helpers;
using CloudTrader.Api.Service;
using Microsoft.EntityFrameworkCore;

namespace CloudTrader.Api.Repositories
{
    public interface IUserRepository
    {
        Task SaveUser(User user);

        Task<User> GetUser(int id);

        Task<User> GetUser(string username);
    }

    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task SaveUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetUser(int id)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> GetUser(string username)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.Username == username);
        }
    }
}
