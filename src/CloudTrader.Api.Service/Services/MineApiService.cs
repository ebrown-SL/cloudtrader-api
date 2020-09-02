using CloudTrader.Api.Data;
using CloudTrader.Api.Service.Helpers;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CloudTrader.Api.Service.Services
{
    public class MineApiService : IMineApiService
    {
        private const string mineServiceUrl = "http://localhost:1189/api/mine";

        public async Task<int> GetMineStock(Guid mineId)
        {
            using var client = new HttpClient();

            var response = await client.GetAsync($"{mineServiceUrl}/{mineId}");

            return (await response.ReadAsJson<Mine>()).Stock;
        }

        public async Task UpdateMineStock(
            Guid mineId, 
            int purchaseQuantity)
        {
            var existingStock = await GetMineStock(mineId);

            var newStock = existingStock - purchaseQuantity;

            using var client = new HttpClient();

            await client.PatchAsync(
                $"{mineServiceUrl}/{mineId}",
                new { Stock = newStock }.ToJsonStringContent()
            );
        }
    }
}
