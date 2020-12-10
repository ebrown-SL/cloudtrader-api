using AutoMapper;
using CloudTrader.Users.Domain;
using CloudTrader.Users.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CloudTrader.Users.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly IMapper mapper;

        private readonly UserContext context;

        public UserRepository(IMapper mapper, UserContext context)
        {
            this.mapper = mapper;
            this.context = context;
            this.context.Database.EnsureCreated();
        }

        public async Task<Guid> SaveUser(User user)
        {
            var userDbModel = mapper.Map<UserDbModel>(user);
            userDbModel.Id = new Guid();
            context.Users.Add(userDbModel);
            await context.SaveChangesAsync();

            return userDbModel.Id;
        }

        public async Task<User> GetUser(Guid id)
        {
            var userDbModel = await context.Users.SingleOrDefaultAsync(x => x.Id == id);
            var user = mapper.Map<User>(userDbModel);
            return user;
        }

        public async Task<User> GetUserByUsername(string username)
        {
            var userDbModel = await context.Users.SingleOrDefaultAsync(x => x.Username == username);
            var user = mapper.Map<User>(userDbModel);
            return user;
        }
    }
}