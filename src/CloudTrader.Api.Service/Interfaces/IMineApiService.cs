using System;
using System.Threading.Tasks;

namespace CloudTrader.Api.Service.Services
{
    public interface IMineApiService
    {
        Task<int> GetMineStock(Guid mineId);
        Task UpdateMineStock(
            Guid mineId,
            int purchaseQuantity
        );

    }
}