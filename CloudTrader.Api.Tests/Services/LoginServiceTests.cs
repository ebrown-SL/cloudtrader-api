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
    public class LoginServiceTests
    {
        [Test]
        public void Login_WithUserNotFound_ThrowsUnauthorizedException()
        {
            var mockUserRepository = new Mock<IUserRepository>();
            var mockTokenGenerator = new Mock<ITokenGenerator>();
            var mockPasswordUtils = new Mock<IPasswordUtils>();
            var loginService = new LoginService(mockUserRepository.Object, mockTokenGenerator.Object, mockPasswordUtils.Object);

            mockUserRepository.Setup(mock => mock.GetUser(It.IsAny<string>())).ReturnsAsync((User)null);

            Assert.ThrowsAsync<UnauthorizedException>(async () => await loginService.Login("username", "password"));
        }

        [Test]
        public void Login_WithInvalidPassword_ThrowsUnauthorizedException()
        {
            var mockUserRepository = new Mock<IUserRepository>();
            var mockTokenGenerator = new Mock<ITokenGenerator>();
            var mockPasswordUtils = new Mock<IPasswordUtils>();
            var loginService = new LoginService(mockUserRepository.Object, mockTokenGenerator.Object, mockPasswordUtils.Object);

            mockUserRepository.Setup(mock => mock.GetUser(It.IsAny<string>())).ReturnsAsync(new User());
            mockPasswordUtils.Setup(mock => mock.VerifyPassword(It.IsAny<string>(), It.IsAny<byte[]>(), It.IsAny<byte[]>())).Returns(false);

            Assert.ThrowsAsync<UnauthorizedException>(async () => await loginService.Login("username", "password"));
        }

        [Test]
        public async Task Login_WithValidUsernameAndPassword_ReturnsValidAuthDetailsAsync()
        {
            var mockUserRepository = new Mock<IUserRepository>();
            var mockTokenGenerator = new Mock<ITokenGenerator>();
            var mockPasswordUtils = new Mock<IPasswordUtils>();
            var loginService = new LoginService(mockUserRepository.Object, mockTokenGenerator.Object, mockPasswordUtils.Object);

            mockUserRepository.Setup(mock => mock.GetUser("username")).ReturnsAsync(new User{ Id = 1, Username = "username" });
            mockPasswordUtils.Setup(mock => mock.VerifyPassword(It.IsAny<string>(), It.IsAny<byte[]>(), It.IsAny<byte[]>())).Returns(true);
            mockTokenGenerator.Setup(mock => mock.GenerateToken(It.IsAny<int>())).Returns("token");

            var authDetails = await loginService.Login("username", "password");

            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(authDetails, new ValidationContext(authDetails), validationResults, true);

            Assert.True(isValid);
        }
    }
}
