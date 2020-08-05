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
    public class TraderApiService : ITraderApiService
    {
        // CloudTrader.Trader running options
        /*private const string traderServiceUrl = "http://localhost:5999/api/trader";*/
        // IIS Express
        private const string traderServiceUrl = "http://localhost:14663/api/trader";

        private readonly IUserService _userService;

        public TraderApiService(IUserService userService)
        {
            _userService = userService;
        }

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
            int mineId, 
            int quantityPurchased 
        )
        {
            var existingStock = (await GetTraderMineStock(traderId, mineId)).Stock;

            using var client = new HttpClient();

            var newMineData = new SetTraderMineRequestModel();
            newMineData.MineId = mineId;
            newMineData.Stock = existingStock + quantityPurchased;

            await client.PostAsync(
                $"{traderServiceUrl}/{traderId}/mines",
                ObjectExtensions.ToJsonStringContent(
                    new { id = traderId, mine = newMineData })
            );
        }

        public async Task UpdateTraderBalanceForPurchase(
            int userId,
            int traderId,
            int purchaseAmount)
        {
            var existingBalance = await _userService.GetBalanceOfUser(userId);

            var newBalance = existingBalance - purchaseAmount;

            using var client = new HttpClient();

            await client.PutAsync(
                $"{traderServiceUrl}/{traderId}/balance",
                ObjectExtensions.ToJsonStringContent(new { Balance = newBalance })
            );
        }
    }

    public static class ObjectExtensions
    {
        public static string ToJson(this object foo)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(foo);
        }

        public static HttpContent ToJsonStringContent(this object foo)
        {
            return new StringContent(ToJson(foo), Encoding.UTF8, "application/json");
        }
    }

    public class SetTraderMineRequestModel
    {
        public int MineId { get; set; }
        public int Stock { get; set; }
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
