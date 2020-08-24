using CloudTrader.Api.Service.Helpers;
using CloudTrader.Api.Service.Interfaces;
using CloudTrader.Api.Service.Services;
using System.Net.Http;
using System.Threading.Tasks;
using CloudTrader.Api.Service.Exceptions;
using System;
using CloudTrader.Api.Controllers;

namespace CloudTrader.Api.Data
{
    public class TraderApiClient : ITraderApiClient
    {
        private readonly string traderServiceUrl = Environment.GetEnvironmentVariable("TRADER_API_URL") + "/api/trader";
        private readonly int INITIAL_TRADER_BALANCE = 100;

        public async Task<int> CreateTrader()
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

        public async Task<TraderResponseModel> GetTrader(int traderId)
        {
            using var client = new HttpClient();

            var uri = $"{traderServiceUrl}/{traderId}";

            var response = await client.GetAsync(uri);

            return await response.ReadAsJson<TraderResponseModel>();
        }

        public async Task<CloudStockDetail> GetTraderMineStock(int traderId, int mineId)
        {
            using var client = new HttpClient();

            var uri = $"{traderServiceUrl}/{traderId}/mines/{mineId}";

            var response = await client.GetAsync(uri);

            return await response.ReadAsJson<CloudStockDetail>();
        }

        public async Task<GetTraderMinesResponseModel> GetAllTraderStock(int traderId)
        {
            using var client = new HttpClient();

            var response = await client.GetAsync(
                $"{traderServiceUrl}/{traderId}/mines");

            return await response.ReadAsJson<GetTraderMinesResponseModel>();
        }
        
        public async Task UpdateTraderMineStockForPurchase(
            int traderId, 
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
            int traderId,
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
