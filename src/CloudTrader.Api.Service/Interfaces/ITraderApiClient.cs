using CloudTrader.Api.Data;
using CloudTrader.Api.Service.Services;
using System.Threading.Tasks;

namespace CloudTrader.Api.Service.Interfaces
{
    public interface ITraderApiClient
    {
        Task<int> CreateTrader();
        Task<TraderResponseModel> GetTrader(int traderId);
        Task<CloudStockDetail> GetTraderMineStock(int traderId, int mineId);
        Task UpdateTraderMineStockForPurchase(
            int traderId,
            SetTraderMineRequestModel newMineData
        );
        Task UpdateTraderBalanceForPurchase(
            int traderId,
            int newBalance
        );
    }
}
