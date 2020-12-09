using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CloudTrader.Api.Domain.Models
{
    public class User
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [JsonIgnore]
        public byte[] PasswordHash { get; set; }

        [Required]
        [JsonIgnore]
        public byte[] PasswordSalt { get; set; }

        [Required]
        public Guid TraderId { get; set; }
    }
}