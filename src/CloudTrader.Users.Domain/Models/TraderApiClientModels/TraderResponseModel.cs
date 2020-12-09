using System;

namespace CloudTrader.Users.Domain.Models.TraderApiClientModels
{
    //Copied from the Traders repo as temporary solution until Traders repo can publish a client
    public class TraderResponseModel
    {
        public Guid Id { get; set; }
        public int Balance { get; set; }
    }
}