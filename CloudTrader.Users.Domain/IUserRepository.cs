using CloudTrader.Users.Domain.Models;
using System;
using System.Threading.Tasks;

namespace CloudTrader.Users.Domain
{
    public interface IUserRepository
    {
        Task<Guid> SaveUser(User user);

        Task<User?> GetUser(Guid id);

        Task<User?> GetUserByUsername(string username);
    }
}