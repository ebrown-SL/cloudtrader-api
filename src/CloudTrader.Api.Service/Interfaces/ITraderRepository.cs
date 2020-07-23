using System.Threading.Tasks;

namespace CloudTrader.Api.Service.Interfaces
{
    public interface ITraderRepository
    {
        Task<int> CreateTrader();
    }
}
