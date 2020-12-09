using CloudTrader.Api.Controllers;
using CloudTrader.Api.Domain.Exceptions;
using CloudTrader.Api.Domain.Helpers;
using CloudTrader.Api.Domain.Interfaces;
using CloudTrader.Api.Domain.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CloudTrader.Api.Data
{
    public class TraderApiClient : ITraderApiClient
    {
        private readonly string traderServiceUrl = Environment.GetEnvironmentVariable("TRADERS_API_URL") + "/api/trader";
        private readonly int INITIAL_TRADER_BALANCE = 100;

        public async Task<Guid> CreateTrader()
        {
            // Make POST request to the traders API to create new trader
            using var client = new HttpClient();

            var payload = new { balance = INITIAL_TRADER_BALANCE }.ToJsonStringContent();

            try
            {
                var response = await client.PostAsync(traderServiceUrl, payload);
                response.EnsureSuccessStatusCode();
                // Deserialize fetched object into TraderResponseModel format
                var traderModel = await response.ReadAsJson<TraderResponseModel>();

                return traderModel.Id;
            }
            catch
            {
                throw new ApiConnectionError("trader");
            }
        }

        public async Task<TraderResponseModel> GetTrader(Guid traderId)
        {
            using var client = new HttpClient();

            var uri = $"{traderServiceUrl}/{traderId}";

            var response = await client.GetAsync(uri);

            return await response.ReadAsJson<TraderResponseModel>();
        }

        public async Task<CloudStockDetail> GetTraderMineStock(Guid traderId, Guid mineId)
        {
            using var client = new HttpClient();

            var uri = $"{traderServiceUrl}/{traderId}/mines/{mineId}";

            var response = await client.GetAsync(uri);

            return await response.ReadAsJson<CloudStockDetail>();
        }

        public async Task<GetTraderMinesResponseModel> GetAllTraderStock(Guid traderId)
        {
            using var client = new HttpClient();

            var response = await client.GetAsync(
                $"{traderServiceUrl}/{traderId}/mines");

            return await response.ReadAsJson<GetTraderMinesResponseModel>();
        }

        public async Task UpdateTraderMineStockForPurchase(
            Guid traderId,
            SetTraderMineRequestModel newMineData
        )
        {
            using var client = new HttpClient();

            await client.PostAsync(
                $"{traderServiceUrl}/{traderId}/mines",
                newMineData.ToJsonStringContent()
            );
        }

        public async Task UpdateTraderBalanceForPurchase(
            Guid traderId,
            int newBalance
        )
        {
            using var client = new HttpClient();

            await client.PutAsync(
                $"{traderServiceUrl}/{traderId}/balance",
                new { Balance = newBalance }
                    .ToJsonStringContent()
            );
        }
    }
}