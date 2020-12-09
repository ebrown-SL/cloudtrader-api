using CloudTrader.Api.Data;
using System.Collections.Generic;

namespace CloudTrader.Api.Controllers
{
    public class GetTraderMinesResponseModel
    {
        public List<CloudStockDetail> CloudStock { get; set; }
    }
}