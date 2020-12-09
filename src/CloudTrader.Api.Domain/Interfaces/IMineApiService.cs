using CloudTrader.Api.Domain.Models;
using System;
using System.Threading.Tasks;

namespace CloudTrader.Api.Domain.Services
{
    public interface IMineApiService
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