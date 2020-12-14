using CloudTrader.Api.Models;
using System;
using System.Threading.Tasks;

namespace CloudTrader.Api.ApiClients
{
    public interface IMineApiClient
    {
        Task<int> GetMineStock(Guid mineId);

        Task UpdateMineStock(
            Guid mineId,
            int purchaseQuantity
        );

        Task<GetAllMinesResponseModel> GetAllMines();

        Task<Mine> GetMine(Guid mineId);
    }
}