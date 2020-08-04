using CloudTrader.Api.Service.Models;
using System.Threading.Tasks;

namespace CloudTrader.Api.Service.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUser(int userId);
        Task<int> GetBalanceOfUser(int userId);
    }
}
