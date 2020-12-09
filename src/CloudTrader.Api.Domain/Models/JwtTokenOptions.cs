using System.ComponentModel.DataAnnotations;

namespace CloudTrader.Api.Domain.Models
{
    public class JwtTokenOptions
    {
        [Required]
        [MinLength(16)]
        public string Key { get; set; }
    }
}