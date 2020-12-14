using CloudTrader.Users.Domain.Models;
using CloudTrader.Users.Domain.Models.TraderApiClientModels;
using System;
using System.Threading.Tasks;

namespace CloudTrader.Users.Domain.Services
{
    /// <remark>
    /// TECH DEBT We intend to separate this api client from users domain
    /// </remark>
    public interface IUserService
    {
        Task<User> GetUser(Guid userId);

        Task<int> GetBalanceOfUser(Guid userId);

        Task<int> GetUsersStockForMine(
            Guid userId,
            Guid mineId
        );

        Task<GetTraderMinesResponseModel> GetAllUserStock(Guid userId);

        Task ProcessTransaction(
            Guid userId,
            Guid mineId,
            int quantity,
            int purchaseAmount
        );
    }
}