using CloudTrader.Api.Controllers;
using CloudTrader.Api.Data;
using CloudTrader.Api.Domain.Services;
using System;
using System.Threading.Tasks;

namespace CloudTrader.Api.Domain.Interfaces
{
    public interface ITraderApiClient
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