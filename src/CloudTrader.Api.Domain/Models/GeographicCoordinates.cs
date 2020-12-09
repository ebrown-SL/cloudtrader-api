using System.ComponentModel.DataAnnotations;

namespace CloudTrader.Api.Domain.Services
{
    public class GeographicCoordinates
    {
        [Required]
        public double? Latitude { get; set; }

        [Required]
        public double? Longitude { get; set; }

        public GeographicCoordinates()
        {
        }

        public GeographicCoordinates(double? latitude, double? longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}