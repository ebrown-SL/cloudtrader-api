using CloudTrader.Api.Domain.Models;
using System.Threading.Tasks;

namespace CloudTrader.Api.Domain.Interfaces
{
    public interface IRegisterService
    {
        Task<AuthDetails> Register(string username, string password);
    }
}