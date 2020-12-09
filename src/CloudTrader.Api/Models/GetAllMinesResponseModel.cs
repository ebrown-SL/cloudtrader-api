using System.Collections.Generic;

namespace CloudTrader.Api.Models
{
    public class GetAllMinesResponseModel
    {
        public List<Mine> Mines { get; set; }

        public GetAllMinesResponseModel()
        {
        }

        public GetAllMinesResponseModel(List<Mine> mines)
        {
            Mines = mines;
        }
    }
}