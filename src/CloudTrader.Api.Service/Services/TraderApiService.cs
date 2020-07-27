using CloudTrader.Api.Service.Helpers;
using CloudTrader.Api.Service.Interfaces;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CloudTrader.Api.Data
{
    public class TraderApiService : ITraderApiService
    {
        private const string traderServiceUrl = "https://localhost:44399/api/trader";

        public async Task<int> CreateTrader()
        {
            // Make POST request to the traders API to create new trader
            using var client = new HttpClient();

            var payload = new StringContent("", Encoding.UTF8, "application/json");

            var response = await client.PostAsync(traderServiceUrl, payload);

            // Deserialize fetched object into TraderResponseModel format
            var traderModel = await response.ReadAsJson<Trader>();
            
            return traderModel.Id;
        }

        public async Task<Trader> GetTrader(int traderId)
        {
            using var client = new HttpClient();

            var uri = $"{traderServiceUrl}/{traderId}";

            var response = await client.GetAsync(uri);

            return await response.ReadAsJson<Trader>();
        }
    }

    //Copied from the Traders repo as temporary solution until Traders repo can publish a client 
    public class Trader
    {
        public int Id { get; set; }
        public int Balance { get; set; }
    }
}
