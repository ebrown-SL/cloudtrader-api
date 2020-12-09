using System;
using System.ComponentModel.DataAnnotations;

namespace CloudTrader.Api.Models
{
    public class PurchaseObject
    {
        [Required]
        public Guid mineId { get; set; }

        [Required]
        public int quantity { get; set; }

        [Required]
        public int purchaseAmount { get; set; }
    }
}