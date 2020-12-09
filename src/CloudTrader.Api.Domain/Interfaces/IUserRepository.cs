using CloudTrader.Api.Domain.Models;
using System;
using System.Threading.Tasks;

namespace CloudTrader.Api.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<Guid> SaveUser(User user);

        Task<User> GetUser(Guid id);

        Task<User> GetUserByName(string username);
    }
}