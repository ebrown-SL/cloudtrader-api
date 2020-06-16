using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CloudTrader.Api.Models;
using NUnit.Framework;

namespace CloudTrader.Api.Tests.Models
{
    public class CredentialsTests
    {

        [TestCase(null)]
        [TestCase("")]
        [TestCase("   ")]
        public void Credentials_WithEmptyUsername_IsInvalid(string username)
        {
            var user = new Credentials

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
        public void Credentials_WithEmptyPassword_IsInvalid(string password)
        {
            var user = new Credentials

            {
                Username = "username",
                Password = password
            };

            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(user, new ValidationContext(user), validationResults);

            Assert.False(isValid);
        }

        [Test]
        public void Credentials_WithNonEmptyUsernameAndPassword_IsValid()
        {
            var user = new Credentials

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
