using System.ComponentModel.DataAnnotations;

namespace CloudTrader.Api.Service.Models
{
    public class AuthDetails
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Token { get; set; }
    }
}
