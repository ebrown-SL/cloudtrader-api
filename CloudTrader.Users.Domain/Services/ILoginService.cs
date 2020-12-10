using CloudTrader.Users.Domain.Models;
using System.Threading.Tasks;

namespace CloudTrader.Users.Domain.Services

{
    public interface ILoginService
    {
        Task<User> Login(string username, string password);
    }
}