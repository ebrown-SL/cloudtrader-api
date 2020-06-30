using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CloudTrader.Api.Service.Models;
using NUnit.Framework;

namespace CloudTrader.Api.Service.Tests.Models
{
    public class JwtTokenOptionsTests
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase("    ")]
        public void JwtTokenOptions_WithNoKey_IsInvalid(string key)
        {
            var options = new JwtTokenOptions
            {
                Key = key
            };

            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(options, new ValidationContext(options), validationResults, true);

            Assert.False(isValid);
        }

        [TestCase("abc")]
        [TestCase("abcdef")]
        [TestCase("abcdefghijklmno")]
        public void JwtTokenOptions_KeyLengthLessThan16_IsInvalid(string key)
        {
            var options = new JwtTokenOptions
            {
                Key = key
            };

            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(options, new ValidationContext(options), validationResults, true);

            Assert.False(isValid);
        }

        [TestCase("abcdefghijklmnop")]
        [TestCase("abcdefghijklmnopqrstuvwxyz")]
        public void JwtTokenOptions_KeyLengthAtLeast16_IsValid(string key)
        {
            var options = new JwtTokenOptions
            {
                Key = key
            };

            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(options, new ValidationContext(options), validationResults, true);

            Assert.True(isValid);
        }
    }
}
