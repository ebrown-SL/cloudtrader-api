using System.Threading.Tasks;
using CloudTrader.Api.Service.Models;

namespace CloudTrader.Api.Service.Interfaces
{
    public interface IRegisterService
    {
        Task<AuthDetails> Register(string username, string password);
    }
}
