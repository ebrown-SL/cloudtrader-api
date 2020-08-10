using System;
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

        public UserRepository(IMapper mapper, UserContext context)
        {
            _mapper = mapper;
            _context = context;
            _context.Database.EnsureCreated();
        }

        public async Task<Guid> SaveUser(User user)
        {
            var userDbModel = _mapper.Map<UserDbModel>(user);
            userDbModel.Id = new Guid();
            _context.Users.Add(userDbModel);
            await _context.SaveChangesAsync();

            return userDbModel.Id;
        }

        public async Task<User> GetUser(Guid id)
        {
            var userDbModel = await _context.Users.SingleOrDefaultAsync(x => x.Id == id);
            var user = _mapper.Map<User>(userDbModel);
            return user;
        }

        public async Task<User> GetUserByName(string username)
        {
            var userDbModel = await _context.Users.SingleOrDefaultAsync(x => x.Username == username);
            var user = _mapper.Map<User>(userDbModel);
            return user;
        }
    }
}
