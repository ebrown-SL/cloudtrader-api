using CloudTrader.Users.Domain.Models;
using CloudTrader.Users.Domain.Models.TraderApiClientModels;
using System;
using System.Threading.Tasks;

namespace CloudTrader.Users.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly ITraderApiClientTechDebt traderApiClient;
        private readonly IMineApiClientTechDebt mineApiClient;

        public UserService(
            IUserRepository userRepository,
            ITraderApiClientTechDebt traderApiClient,
            IMineApiClientTechDebt mineApiClient)
        {
            this.userRepository = userRepository;
            this.traderApiClient = traderApiClient;
            this.mineApiClient = mineApiClient;
        }

        public async Task<int> GetBalanceOfUser(Guid userId)
        {
            var currentUser = await GetUser(userId);
            var currentUserTraderId = currentUser.TraderId;
            var trader = await traderApiClient.GetTrader(currentUserTraderId);
            return trader.Balance;
        }

        public Task<User> GetUser(Guid userId)
        {
            return userRepository.GetUser(userId);
        }

        public async Task<int> GetUsersStockForMine(Guid userId, Guid mineId)
        {
            var user = await GetUser(userId);
            var userTraderId = user.TraderId;

            return (await traderApiClient.GetTraderMineStock(
                userTraderId,
                mineId)
            ).Stock;
        }

        public async Task<GetTraderMinesResponseModel> GetAllUserStock(Guid userId)
        {
            var user = await GetUser(userId);
            var userTraderId = user.TraderId;

            return await traderApiClient.GetAllTraderStock(userTraderId);
        }

        public async Task ProcessTransaction(
            Guid userId,
            Guid mineId,
            int quantity,
            int purchaseAmount)
        {
            var user = await GetUser(userId);
            var userTraderId = user.TraderId;

            var userBalance = await GetBalanceOfUser(userId);
            var newUserBalance = userBalance - purchaseAmount;

            var traderMineStock = (await traderApiClient.GetTraderMineStock(
                userTraderId,
                mineId)
            ).Stock;

            var newTraderMineData = new SetTraderMineRequestModel
            {
                MineId = mineId,
                Stock = traderMineStock + quantity
            };

            // Update user/trader's balance
            await traderApiClient.UpdateTraderBalanceForPurchase(
                userTraderId, newUserBalance
            );

            // Update user/trader's stock
            await traderApiClient.UpdateTraderMineStockForPurchase(
                userTraderId, newTraderMineData
            );

            // Update mine's stock
            await mineApiClient.UpdateMineStock(mineId, quantity);
        }
    }
}