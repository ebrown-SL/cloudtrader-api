using System;
using System.Threading.Tasks;
using CloudTrader.Api.Service.Models;

namespace CloudTrader.Api.Service.Interfaces
{
    public interface IUserRepository
    {
        Task<Guid> SaveUser(User user);

        Task<User> GetUser(Guid id);

        Task<User> GetUserByName(string username);
    }
}
