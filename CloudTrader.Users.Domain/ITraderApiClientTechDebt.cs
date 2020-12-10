using CloudTrader.Users.Domain.Models.TraderApiClientModels;
using System;
using System.Threading.Tasks;

namespace CloudTrader.Users.Domain
{
    /// <remark>
    /// TECH DEBT We intend to separate this api client from users domain
    /// </remark>
    public interface ITraderApiClientTechDebt
    {
        Task<Guid> CreateTrader();

        Task<TraderResponseModel> GetTrader(Guid traderId);

        Task<CloudStockDetail> GetTraderMineStock(Guid traderId, Guid mineId);

        Task UpdateTraderMineStockForPurchase(
            Guid traderId,
            SetTraderMineRequestModel newMineData
        );

        Task UpdateTraderBalanceForPurchase(
            Guid traderId,
            int newBalance
        );

        Task<GetTraderMinesResponseModel> GetAllTraderStock(Guid traderId);
    }
}