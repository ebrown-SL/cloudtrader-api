using CloudTrader.Api.Data;
using CloudTrader.Api.Service.Helpers;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Threading.Tasks;

namespace CloudTrader.Api.Service.Services
{
    public class MineApiService : IMineApiService
    {
        private const string mineServiceUrl = "http://localhost:1189/api/mine";

        public async Task<int> GetMineStock(int mineId)
        {
            using var client = new HttpClient();

            var response = await client.GetAsync($"{mineServiceUrl}/{mineId}");

            return (await response.ReadAsJson<Mine>()).Stock;
        }

        public async Task UpdateMineStock(
            int mineId, 
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

    public class Mine
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public GeographicCoordinates Coordinates { get; set; }

        [Required]
        public double Temperature { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Stock { get; set; }

        [Required]
        public string Name { get; set; }
    }

    public class GeographicCoordinates
    {
        [Required]
        public double? Latitude { get; set; }
        [Required]
        public double? Longitude { get; set; }

        public GeographicCoordinates() { }

        public GeographicCoordinates(double? latitude, double? longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
