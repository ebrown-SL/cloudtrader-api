using System.Collections.Generic;

namespace CloudTrader.Users.Domain.Models.TraderApiClientModels
{
    public class GetTraderMinesResponseModel
    {
        public List<CloudStockDetail> CloudStock { get; set; }
    }
}