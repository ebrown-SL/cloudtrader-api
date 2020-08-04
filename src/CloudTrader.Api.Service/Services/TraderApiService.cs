using CloudTrader.Api.Service.Helpers;
using CloudTrader.Api.Service.Interfaces;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CloudTrader.Api.Data
{
    public class TraderApiService : ITraderApiService
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
    }
}
