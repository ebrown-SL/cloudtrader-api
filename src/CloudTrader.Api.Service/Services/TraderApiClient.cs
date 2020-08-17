using CloudTrader.Api.Service.Helpers;
using CloudTrader.Api.Service.Interfaces;
using CloudTrader.Api.Service.Services;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CloudTrader.Api.Data
{
    public class TraderApiClient : ITraderApiClient
    {
        private const string traderServiceUrl = "http://localhost:5999/api/trader";

        public async Task<int> CreateTrader()
        {
            // Make POST request to the traders API to create new trader
            using var client = new HttpClient();

            var payload = new StringContent("", Encoding.UTF8, "application/json");

            var response = await client.PostAsync(traderServiceUrl, payload);

            // Deserialize fetched object into TraderResponseModel format
            var traderModel = await response.ReadAsJson<TraderResponseModel>();

            return traderModel.Id;
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
