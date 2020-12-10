using System;
using System.ComponentModel.DataAnnotations;

namespace CloudTrader.Api.Models
{
    public class LoginSuccessResponse
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Token { get; set; }

        public LoginSuccessResponse()
        {
        }

        public LoginSuccessResponse(Guid id, string username, string token)
        {
            Id = id;
            Username = username;
            Token = token;
        }
    }
}