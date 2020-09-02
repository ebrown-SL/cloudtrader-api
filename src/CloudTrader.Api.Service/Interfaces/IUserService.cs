using System;
﻿using CloudTrader.Api.Controllers;
using CloudTrader.Api.Service.Models;
using System.Threading.Tasks;

namespace CloudTrader.Api.Service.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUser(Guid userId);
        Task<int> GetBalanceOfUser(Guid userId);
        Task<int> GetUsersStockForMine(
            Guid userId,
            Guid mineId
        );
        Task<GetTraderMinesResponseModel> GetAllUserStock(Guid userId);
        Task ProcessTransaction(
            Guid userId,
            Guid mineId,
            int quantity,
            int purchaseAmount
        );
    }
}
