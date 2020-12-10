using System;

namespace CloudTrader.Users.Domain.Models.TraderApiClientModels
{
    public class SetTraderMineRequestModel
    {
        public Guid MineId { get; set; }
        public int Stock { get; set; }
    }
}