using CloudTrader.Users.Domain.Models;
using System.Threading.Tasks;

namespace CloudTrader.Users.Domain.Services
{
    public interface IRegisterService
    {
        Task<User> Register(string username, string password);
    }
}