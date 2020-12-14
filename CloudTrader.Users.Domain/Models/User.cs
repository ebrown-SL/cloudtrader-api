using System;
using System.ComponentModel.DataAnnotations;

namespace CloudTrader.Users.Domain.Models
{
    public class User
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public byte[] PasswordHash { internal get; set; }

        [Required]
        public byte[] PasswordSalt { internal get; set; }

        [Required]
        public Guid TraderId { get; set; }

#nullable disable
        // serialisation constructor

        public User()
        {
        }

#nullable restore

        public User(string username, byte[] passwordHash, byte[] passwordSalt, Guid traderId)
        {
            Username = username;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            TraderId = traderId;
        }

        public User(Guid id, string username, byte[] passwordHash, byte[] passwordSalt, Guid traderId)
        {
            Id = id;
            Username = username;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            TraderId = traderId;
        }
    }
}