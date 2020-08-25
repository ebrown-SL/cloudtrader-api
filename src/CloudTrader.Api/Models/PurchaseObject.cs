using System.ComponentModel.DataAnnotations;

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
