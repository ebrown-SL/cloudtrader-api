using CloudTrader.Api.Service.Interfaces;
using CloudTrader.Api.Service.Models;
using System.Threading.Tasks;

namespace CloudTrader.Api.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITraderApiService _traderApiService;
        private readonly IMineApiService _mineApiService;

        public UserService(
            IUserRepository userRepository, 
            ITraderApiService traderApiService,
            IMineApiService mineApiService)
        {
            _userRepository = userRepository;
            _traderApiService = traderApiService;
            _mineApiService = mineApiService;
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

        public async Task ProcessTransaction(
            int userId, 
            int mineId, 
            int quantity, 
            int purchaseAmount)
        {
            var currentUser = await GetUser(userId);
            var currentUserTraderId = currentUser.TraderId;

            // Update user/trader's balance
            await _traderApiService.UpdateTraderBalanceForPurchase(
                userId, currentUserTraderId, purchaseAmount
            );

            // Update user/trader's stock
           await _traderApiService.UpdateTraderMineStockForPurchase(
                currentUserTraderId, mineId, quantity
            );

            // Update mine's stock
            await _mineApiService.UpdateMineStock(mineId, quantity);

        }
    }
}
