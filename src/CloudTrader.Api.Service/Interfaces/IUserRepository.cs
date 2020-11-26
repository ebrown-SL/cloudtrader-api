using CloudTrader.Api.Service.Models;
using System;
using System.Threading.Tasks;

namespace CloudTrader.Api.Service.Interfaces
{
    public interface IUserRepository
    {
        Task<Guid> SaveUser(User user);

        Task<User> GetUser(Guid id);

        Task<User> GetUserByName(string username);
    }
}