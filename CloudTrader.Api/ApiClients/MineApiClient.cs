using CloudTrader.Api.Domain.Helpers;
using CloudTrader.Api.Helpers;
using CloudTrader.Api.Models;
using CloudTrader.Users.Domain;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CloudTrader.Api.ApiClients
{
    public class MineApiClient : IMineApiClient, IMineApiClientTechDebt
    {
        private readonly string mineServiceUrl = Environment.GetEnvironmentVariable("MINES_API_URL") + "/api/mine";

        public async Task<int> GetMineStock(Guid mineId)
        {
            using var client = new HttpClient();

            var response = await client.GetAsync($"{mineServiceUrl}/{mineId}");

            return (await response.ReadAsJson<Mine>()).Stock;
        }

        /// <remark>
        /// The logic in this method needs to be refactored to Mines Service
        /// </remark>
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

        public async Task<GetAllMinesResponseModel> GetAllMines()
        {
            using var client = new HttpClient();

            var response = await client.GetAsync(
                $"{mineServiceUrl}"
            );

            return await response.ReadAsJson<GetAllMinesResponseModel>();
        }

        public async Task<Mine> GetMine(Guid mineId)
        {
            using var client = new HttpClient();

            var response = await client.GetAsync($"{mineServiceUrl}/{mineId}");

            return await response.ReadAsJson<Mine>();
        }
    }
}