using CloudTrader.Api.Service.Models;
using System.Threading.Tasks;

namespace CloudTrader.Api.Service.Interfaces
{
    public interface ILoginService
    {
        Task<AuthDetails> Login(string username, string password);
    }
}