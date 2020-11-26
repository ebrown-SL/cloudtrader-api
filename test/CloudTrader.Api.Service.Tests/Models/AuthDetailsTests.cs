using CloudTrader.Api.Service.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CloudTrader.Api.Service.Tests.Models
{
    public class AuthDetailsTests
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase("    ")]
        public void AuthDetails_WithNoUsername_IsInvalid(string username)
        {
            var authDetails = new AuthDetails
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
        public void AuthDetails_WithNoToken_IsInvalid(string token)
        {
            var authDetails = new AuthDetails
            {
                Id = new Guid(),
                Username = "username",
                Token = token
            };

            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(authDetails, new ValidationContext(authDetails), validationResults, true);

            Assert.False(isValid);
        }

        public void AuthDetails_WithRequiredFields_IsValid()
        {
            var authDetails = new AuthDetails
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