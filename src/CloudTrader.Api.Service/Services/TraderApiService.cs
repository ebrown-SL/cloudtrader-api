using CloudTrader.Api.Service.Interfaces;
﻿using CloudTrader.Api.Service.Helpers;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CloudTrader.Api.Service.Exceptions;

namespace CloudTrader.Api.Data
{
    public class TraderApiService : ITraderApiService
    {
        private readonly string traderServiceUrl = Environment.GetEnvironmentVariable("TRADER_API_URL") + "/api/trader";
        private readonly int INITIAL_TRADER_BALANCE = 100;

        public async Task<int> CreateTrader()
        {
            // Make POST request to the traders API to create new trader
            using var client = new HttpClient();

            var payload = new StringContent($" {{ \"balance\": {INITIAL_TRADER_BALANCE} }} ", Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync(traderServiceUrl, payload);
                response.EnsureSuccessStatusCode();
                // Deserialize fetched object into TraderResponseModel format
                var traderModel = await response.ReadAsJson<TraderResponseModel>();

                return traderModel.Id;
            } catch
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
    }
}
