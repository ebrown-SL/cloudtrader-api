using System.Net.Http;
using System.Text;

namespace CloudTrader.Api.Helpers
{
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
}