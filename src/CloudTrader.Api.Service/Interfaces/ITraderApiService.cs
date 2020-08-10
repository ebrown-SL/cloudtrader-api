using CloudTrader.Api.Data;
using System;
using System.Threading.Tasks;

namespace CloudTrader.Api.Service.Interfaces
{
    public interface ITraderApiService
    {
        Task<Guid> CreateTrader();
        Task<TraderResponseModel> GetTrader(Guid traderId);
    }
}
