using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CloudTrader.Api.Service.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [JsonIgnore]
        public byte[] PasswordHash { get; set; }

        [Required]
        [JsonIgnore]
        public byte[] PasswordSalt { get; set; }
    }
}
