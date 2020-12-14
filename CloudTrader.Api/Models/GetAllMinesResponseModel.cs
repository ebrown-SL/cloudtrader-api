using System.Collections.Generic;

namespace CloudTrader.Api.Models
{
    public class GetAllMinesResponseModel
    {
        public List<Mine> Mines { get; set; }

#nullable disable

        // serialisation constructor
        public GetAllMinesResponseModel()
        {
        }

#nullable restore

        public GetAllMinesResponseModel(List<Mine> mines)
        {
            Mines = mines;
        }
    }
}