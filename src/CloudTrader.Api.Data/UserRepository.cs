using System.Threading.Tasks;
using AutoMapper;
using CloudTrader.Api.Service.Interfaces;
using CloudTrader.Api.Service.Models;
using Microsoft.EntityFrameworkCore;

namespace CloudTrader.Api.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly IMapper _mapper;

        private readonly UserContext _context;

        public UserRepository(IMapper mapper)
        {
            _mapper = mapper;

            var contextOptions = new DbContextOptionsBuilder<UserContext>()
                .UseInMemoryDatabase(databaseName: "Traders")
                .Options;
            _context = new UserContext(contextOptions);
        }

        public async Task<int> SaveUser(User user)
        {
            var userDbModel = _mapper.Map<UserDbModel>(user);
            _context.Users.Add(userDbModel);
            await _context.SaveChangesAsync();

            return userDbModel.Id;
        }

        public async Task<User> GetUser(int id)
        {
            var userDbModel = await _context.Users.SingleOrDefaultAsync(x => x.Id == id);
            var user = _mapper.Map<User>(userDbModel);
            return user;
        }

        public async Task<User> GetUser(string username)
        {
            var userDbModel = await _context.Users.SingleOrDefaultAsync(x => x.Username == username);
            var user = _mapper.Map<User>(userDbModel);
            return user;
        }
    }
}
