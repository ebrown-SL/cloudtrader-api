using System;

namespace CloudTrader.Api.Service.Services
{
    public class SetTraderMineRequestModel
    {
        public Guid MineId { get; set; }
        public int Stock { get; set; }
    }
}
