using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CloudTrader.Api.Models;
using NUnit.Framework;

namespace CloudTrader.Api.Tests.Models
{
    public class AuthenticationModelTests
    {

        [TestCase(null)]
        [TestCase("")]
        [TestCase("   ")]
        public void AuthenticationModel_WithEmptyUsername_IsInvalid(string username)
        {
            var user = new AuthenticationModel
            {
                Username = username,
                Password = "password"
            };

            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(user, new ValidationContext(user), validationResults);

            Assert.False(isValid);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("   ")]
        public void AuthenticationModel_WithEmptyPassword_IsInvalid(string password)
        {
            var user = new AuthenticationModel
            {
                Username = "username",
                Password = password
            };

            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(user, new ValidationContext(user), validationResults);

            Assert.False(isValid);
        }

        [Test]
        public void AuthenticationModel_WithNonEmptyUsernameAndPassword_IsValid()
        {
            var user = new AuthenticationModel
            {
                Username = "username",
                Password = "password"
            };

            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(user, new ValidationContext(user), validationResults);

            Assert.True(isValid);
        }
    }
}
