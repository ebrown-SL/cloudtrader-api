using CloudTrader.Api.Service.Interfaces;
using CloudTrader.Api.Service.Models;
using System.Threading.Tasks;

namespace CloudTrader.Api.Service.Services
{
    class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITraderApiService _traderApiService;

        public UserService(
            IUserRepository userRepository, 
            ITraderApiService traderApiService)
        {
            _userRepository = userRepository;
            _traderApiService = traderApiService;
        }

        public async Task<int> GetBalanceOfUser(int userId)
        {
            var currentUser = await GetUser(userId);
            var currentUserTraderId = currentUser.TraderId;
            var trader = await _traderApiService.GetTrader(currentUserTraderId);
            return trader.Balance;
        }

        public Task<User> GetUser(int userId)
        {
            return _userRepository.GetUser(userId);
        }
    }
}
