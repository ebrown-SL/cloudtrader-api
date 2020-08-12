using CloudTrader.Api.Service.Interfaces;
using CloudTrader.Api.Service.Models;
using System.Threading.Tasks;

namespace CloudTrader.Api.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITraderApiClient _traderApiClient;
        private readonly IMineApiService _mineApiService;

        public UserService(
            IUserRepository userRepository, 
            ITraderApiClient traderApiClient,
            IMineApiService mineApiService)
        {
            _userRepository = userRepository;
            _traderApiClient = traderApiClient;
            _mineApiService = mineApiService;
        }

        public async Task<int> GetBalanceOfUser(int userId)
        {
            var currentUser = await GetUser(userId);
            var currentUserTraderId = currentUser.TraderId;
            var trader = await _traderApiClient.GetTrader(currentUserTraderId);
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
            var user = await GetUser(userId);
            var userTraderId = user.TraderId;

            var userBalance = await GetBalanceOfUser(userId);
            var newUserBalance = userBalance - purchaseAmount;

            var traderMineStock = (await _traderApiClient.GetTraderMineStock(
                userTraderId, 
                mineId)
            ).Stock;

            var newTraderMineData = new SetTraderMineRequestModel
            {
                MineId = mineId,
                Stock = traderMineStock + quantity
            };


            // Update user/trader's balance
            await _traderApiClient.UpdateTraderBalanceForPurchase(
                userTraderId, newUserBalance
            );

            // Update user/trader's stock
            await _traderApiClient.UpdateTraderMineStockForPurchase(
                userTraderId, newTraderMineData
            );

            // Update mine's stock
            await _mineApiService.UpdateMineStock(mineId, quantity);

        }
    }
    public class SetTraderMineRequestModel
    {
        public int MineId { get; set; }
        public int Stock { get; set; }
    }
}
