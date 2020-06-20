using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using CloudTrader.Api.Exceptions;
using CloudTrader.Api.Helpers;
using CloudTrader.Api.Models;
using CloudTrader.Api.Repositories;
using CloudTrader.Api.Services;
using Moq;
using NUnit.Framework;

namespace CloudTrader.Api.Tests.Services
{
    public class RegisterServiceTests
    {
        [Test]
        public void Register_WithUsernameAlreadyTaken_ThrowsUsernameAlreadyExistsException()
        {
            var mockUserRepository = new Mock<IUserRepository>();
            var mockTokenGenerator = new Mock<ITokenGenerator>();
            var mockPasswordUtils = new Mock<IPasswordUtils>();
            var registerService = new RegisterService(mockUserRepository.Object, mockTokenGenerator.Object, mockPasswordUtils.Object);

            mockUserRepository.Setup(mock => mock.GetUser(It.IsAny<string>())).ReturnsAsync(new User());

            Assert.ThrowsAsync<UsernameAlreadyExistsException>(async () => await registerService.Register("username", "password"));
        }

        [Test]
        public async Task Register_WithValidUsernameAndPassword_ReturnsValidAuthDetails()
        {
            var mockUserRepository = new Mock<IUserRepository>();
            var mockTokenGenerator = new Mock<ITokenGenerator>();
            var mockPasswordUtils = new Mock<IPasswordUtils>();
            var registerService = new RegisterService(mockUserRepository.Object, mockTokenGenerator.Object, mockPasswordUtils.Object);

            mockTokenGenerator.Setup(mock => mock.GenerateToken(It.IsAny<int>())).Returns("token");

            var authDetails = await registerService.Register("username", "password");

            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(authDetails, new ValidationContext(authDetails), validationResults, true);

            Assert.True(isValid);
        }
    }
}
