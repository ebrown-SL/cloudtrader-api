using CloudTrader.Api.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CloudTrader.Api.Domain.Tests.Models
{
    public class LoginSuccessResponseTests
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase("    ")]
        public void LoginSuccessResponse_WithNoUsername_IsInvalid(string username)
        {
            var authDetails = new LoginSuccessResponse
            {
                Id = new Guid(),
                Username = username,
                Token = "token"
            };

            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(authDetails, new ValidationContext(authDetails), validationResults, true);

            Assert.False(isValid);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("    ")]
        public void LoginSuccessResponse_WithNoToken_IsInvalid(string token)
        {
            var authDetails = new LoginSuccessResponse
            {
                Id = new Guid(),
                Username = "username",
                Token = token
            };

            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(authDetails, new ValidationContext(authDetails), validationResults, true);

            Assert.False(isValid);
        }

        public void LoginSuccessResponse_WithRequiredFields_IsValid()
        {
            var authDetails = new LoginSuccessResponse
            {
                Id = new Guid(),
                Username = "username",
                Token = "token"
            };

            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(authDetails, new ValidationContext(authDetails), validationResults, true);

            Assert.True(isValid);
        }
    }
}