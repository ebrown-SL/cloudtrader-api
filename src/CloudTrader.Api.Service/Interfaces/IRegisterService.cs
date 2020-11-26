using CloudTrader.Api.Service.Models;
using System.Threading.Tasks;

namespace CloudTrader.Api.Service.Interfaces
{
    public interface IRegisterService
    {
        Task<AuthDetails> Register(string username, string password);
    }
}