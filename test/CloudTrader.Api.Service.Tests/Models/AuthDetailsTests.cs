using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CloudTrader.Api.Service.Models;
using NUnit.Framework;

namespace CloudTrader.Api.Service.Tests.Models
{
    public class AuthDetailsTests
    {
        [TestCase(null)]
        public void AuthDetails_WithNoId_DefaultsTo0(int id)
        {
            var authDetails = new AuthDetails
            {
                Id = id,
                Username = "username",
                Token = "token"
            };

            Assert.AreEqual(0, authDetails.Id);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("    ")]
        public void AuthDetails_WithNoUsername_IsInvalid(string username)
        {
            var authDetails = new AuthDetails
            {
                Id = 1,
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
                Id = 1,
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
                Id = 1,
                Username = "username",
                Token = "token"
            };

            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(authDetails, new ValidationContext(authDetails), validationResults, true);

            Assert.True(isValid);
        }
    }
}
