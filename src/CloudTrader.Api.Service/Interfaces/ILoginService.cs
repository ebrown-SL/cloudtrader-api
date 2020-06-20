using System.Threading.Tasks;
using CloudTrader.Api.Service.Models;

namespace CloudTrader.Api.Service.Interfaces
{
    public interface ILoginService
    {
        Task<AuthDetails> Login(string username, string password);
    }
}
