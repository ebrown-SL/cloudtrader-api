using CloudTrader.Api.Service.Models;
using System;
using System.Threading.Tasks;

namespace CloudTrader.Api.Service.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUser(Guid userId);
        Task<int> GetBalanceOfUser(Guid userId);
    }
}
