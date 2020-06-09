using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CloudTrader.Api.Models
{
    public class UserModel
    {
        [Key]
        public int ID { get; set; }

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
