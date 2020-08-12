using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CloudTrader.Api.Service.Models
{
    public class PurchaseObject
    {
        [Required]
        public int mineId { get; set; }

        [Required]
        public int quantity { get; set; }

        [Required]
        public int purchaseAmount { get; set; }
    }
}
