using System;
using System.ComponentModel.DataAnnotations;

namespace CloudTrader.Api.Service.Services
{
    public class Mine
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public GeographicCoordinates Coordinates { get; set; }

        [Required]
        public double Temperature { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Stock { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
