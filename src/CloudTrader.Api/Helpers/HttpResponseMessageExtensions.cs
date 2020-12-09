using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace CloudTrader.Api.Domain.Helpers
{
    internal static class HttpResponseMessageExtensions
    {
        public static async Task<T> ReadAsJson<T>(this HttpResponseMessage message)
        {
            return JsonConvert.DeserializeObject<T>(
                await message.Content.ReadAsStringAsync()
            );
        }
    }
}