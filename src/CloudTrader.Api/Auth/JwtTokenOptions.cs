using System.ComponentModel.DataAnnotations;

namespace CloudTrader.Api.Auth
{
    public class JwtTokenOptions
    {
        [Required]
        [MinLength(16)]
        public string Key { get; set; }
    }
}