using System;
using System.ComponentModel.DataAnnotations;

namespace CloudTrader.Api.Data
{
    public class UserDbModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public byte[] PasswordHash { get; set; }

        [Required]
        public byte[] PasswordSalt { get; set; }

        [Required]
        public Guid TraderId { get; set; }
    }
}