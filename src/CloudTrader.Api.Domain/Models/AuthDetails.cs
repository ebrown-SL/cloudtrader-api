using System;
using System.ComponentModel.DataAnnotations;

namespace CloudTrader.Api.Domain.Models
{
    //; // TODO get rid of this if not being used anywhere

    public class AuthDetails
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Token { get; set; }
    }
}