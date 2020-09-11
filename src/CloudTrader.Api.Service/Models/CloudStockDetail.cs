using System;
using System.ComponentModel.DataAnnotations;

namespace CloudTrader.Api.Data
{
    public class CloudStockDetail
    {
        [Key]
        [Required]
        public Guid MineId { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Stock { get; set; }
    }
}
