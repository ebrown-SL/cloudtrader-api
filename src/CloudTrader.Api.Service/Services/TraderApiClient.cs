using CloudTrader.Api.Service.Helpers;
using CloudTrader.Api.Service.Interfaces;
using CloudTrader.Api.Service.Services;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CloudTrader.Api.Data
{
    public class TraderApiClient : ITraderApiClient
    {
        // CloudTrader.Trader running options
        private const string traderServiceUrl = "http://localhost:5999/api/trader";
        // IIS Express
        //private const string traderServiceUrl = "http://localhost:14663/api/trader";

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

    public static class ObjectExtensions
    {
        public static string ToJson(this object obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }

        public static HttpContent ToJsonStringContent(this object obj)
        {
            return new StringContent(
                obj.ToJson(),
                Encoding.UTF8,
                "application/json"
            );
        }
    }

    public class GetTraderMinesResponseModel
    {
        public List<CloudStockDetail> CloudStock { get; set; }
    }

    public class CloudStockDetail
    {
        [Key]
        [Required]
        public int MineId { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Stock { get; set; }
    }

    public class SetTraderBalanceRequestModel
    {
        public int Balance { get; set; }
    }
}
