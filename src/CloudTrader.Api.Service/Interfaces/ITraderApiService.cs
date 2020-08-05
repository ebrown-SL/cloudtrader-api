using CloudTrader.Api.Data;
using System.Threading.Tasks;

namespace CloudTrader.Api.Service.Interfaces
{
    public interface ITraderApiService
    {
        Task<int> CreateTrader();
        Task<TraderResponseModel> GetTrader(int traderId);
        Task UpdateTraderMineStockForPurchase(
            int traderId,
            int mineId,
            int quantityPurchased
        );
        Task UpdateTraderBalanceForPurchase(
            int userId,
            int traderId,
            int purchaseAmount
        );
    }
}
