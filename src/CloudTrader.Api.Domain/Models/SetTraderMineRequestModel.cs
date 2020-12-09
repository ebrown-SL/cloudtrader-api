using System;

namespace CloudTrader.Api.Domain.Services
{
    public class SetTraderMineRequestModel
    {
        public Guid MineId { get; set; }
        public int Stock { get; set; }
    }
}