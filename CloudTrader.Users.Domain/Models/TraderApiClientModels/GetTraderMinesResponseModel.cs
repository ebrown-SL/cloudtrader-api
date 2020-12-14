using System.Collections.Generic;

#nullable disable // serialisation pass through from mines

namespace CloudTrader.Users.Domain.Models.TraderApiClientModels
{
    public class GetTraderMinesResponseModel
    {
        public List<CloudStockDetail> CloudStock { get; set; }
    }
}