using System.Collections.Generic;
using CloudTrader.Api.Services;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace CloudTrader.Api.Tests.Services
{
    public class AuthenticationServiceTests
    {
        private AuthenticationService _authenticationService;

        [SetUp]
        public void Setup()
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "JWT_KEY", "abcdefghijklmnopqrstuvwxyz" }
                })
                .Build();
            _authenticationService = new AuthenticationService(configuration);
        }

        [Test]
        public void CreatePasswordHash_WithValidPassword_ReturnsHashAndSalt()
        {
            string password = "password";

            _authenticationService.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            Assert.IsNotEmpty(passwordHash);
            Assert.IsNotEmpty(passwordSalt);
        }

        [Test]
        public void VerifyPassword_WithCorrectPassword_ReturnsTrue()
        {
            string password = "password";

            _authenticationService.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            var verified = _authenticationService.VerifyPassword(password, passwordHash, passwordSalt);

            Assert.IsTrue(verified);
        }

        [Test]
        public void VerifyPassword_WithIncorrectPassword_ReturnsFalse()
        {
            string password = "password";

            _authenticationService.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            var verified = _authenticationService.VerifyPassword("incorrect", passwordHash, passwordSalt);

            Assert.IsFalse(verified);
        }

        [Test]
        public void GenerateToken_WithValidId_ReturnsToken()
        {
            var token = _authenticationService.GenerateToken(1);

            Assert.IsNotNull(token);
            Assert.IsNotEmpty(token);
        }
    }
}
