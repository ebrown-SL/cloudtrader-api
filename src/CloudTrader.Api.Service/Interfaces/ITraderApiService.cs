using CloudTrader.Api.Data;
using System.Threading.Tasks;

namespace CloudTrader.Api.Service.Interfaces
{
    public interface ITraderApiService
    {
        Task<int> CreateTrader();
        Task<Trader> GetTrader(int traderId);
    }
}
