using System.Threading.Tasks;

namespace CloudTrader.Api.Service.Interfaces
{
    public interface IUserRepository
    {
        Task SaveUser(User user);

        Task<User> GetUser(int id);

        Task<User> GetUser(string username);
    }
}
