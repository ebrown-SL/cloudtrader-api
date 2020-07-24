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
            var traderModel = JsonConvert.DeserializeObject<TraderResponseModel>(
                await response.Content.ReadAsStringAsync()
            );

            return traderModel.Id;
        }
    }

    //Copied from the Traders repo as temporary solution until Traders repo can publish a client 
    class TraderResponseModel
    {
        public int Id { get; set; }
        public int Balance { get; set; }
    }
}
