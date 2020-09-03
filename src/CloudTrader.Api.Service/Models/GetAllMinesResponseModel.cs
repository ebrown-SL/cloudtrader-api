using CloudTrader.Api.Service.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudTrader.Api.Service.Models
{
    public class GetAllMinesResponseModel
    {
        public List<Mine> Mines { get; set; }

        public GetAllMinesResponseModel() { }

        public GetAllMinesResponseModel(List<Mine> mines)
        {
            Mines = mines;
        }
    }
}
