using CloudTrader.Api.Service.Models;
using System.Threading.Tasks;

namespace CloudTrader.Api.Service.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUser(int userId);
        Task<int> GetBalanceOfUser(int userId);
        Task<int> GetUsersStockForMine(
            int userId, 
            int mineId
        );
        Task ProcessTransaction(
            int userId,
            int mineId,
            int quantity,
            int purchaseAmount
        );
    }
}
