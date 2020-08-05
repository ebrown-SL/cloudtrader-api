using System.Threading.Tasks;

namespace CloudTrader.Api.Service.Services
{
    public interface IMineApiService
    {
        Task<int> GetMineStock(int mineId);
        Task UpdateMineStock(
            int mineId,
            int purchaseQuantity
        );

    }
}