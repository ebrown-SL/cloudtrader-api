using CloudTrader.Api.Domain.Models;
using System.Threading.Tasks;

namespace CloudTrader.Api.Domain.Interfaces
{
    public interface ILoginService
    {
        Task<AuthDetails> Login(string username, string password);
    }
}