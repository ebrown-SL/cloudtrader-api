using System.Threading.Tasks;
using CloudTrader.Api.Helpers;
using CloudTrader.Api.Models;

namespace CloudTrader.Api.Services
{
    public interface IUserService
    {
        Task<UserModel> Create(UserModel user);
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
    }
}
