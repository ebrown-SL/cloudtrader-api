using System.ComponentModel.DataAnnotations;

namespace CloudTrader.Api.Service.Models
{
    public class JwtTokenOptions
    {
        [Required]
        [MinLength(16)]
        public string Key { get; set; }
    }
}
