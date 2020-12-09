using CloudTrader.Api.Domain.Exceptions;
using CloudTrader.Api.Domain.Interfaces;
using CloudTrader.Api.Domain.Models;
using CloudTrader.Api.Domain.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace CloudTrader.Api.Domain.Tests.Services
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

            mockUserRepository.Setup(mock => mock.GetUser(It.IsAny<Guid>())).ReturnsAsync((User)null);

            Assert.ThrowsAsync<UnauthorizedException>(async () => await loginService.Login("username", "password"));
        }

        [Test]
        public void Login_WithInvalidPassword_ThrowsUnauthorizedException()
        {
            var mockUserRepository = new Mock<IUserRepository>();
            var mockTokenGenerator = new Mock<ITokenGenerator>();
            var mockPasswordUtils = new Mock<IPasswordUtils>();
            var loginService = new LoginService(mockUserRepository.Object, mockTokenGenerator.Object, mockPasswordUtils.Object);

            mockUserRepository.Setup(mock => mock.GetUser(It.IsAny<Guid>())).ReturnsAsync(new User());
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

            mockUserRepository.Setup(mock => mock.GetUserByName("username")).ReturnsAsync(new User { Id = new Guid(), Username = "username" });
            mockPasswordUtils.Setup(mock => mock.VerifyPassword(It.IsAny<string>(), It.IsAny<byte[]>(), It.IsAny<byte[]>())).Returns(true);
            mockTokenGenerator.Setup(mock => mock.GenerateToken(It.IsAny<Guid>())).Returns("token");

            var authDetails = await loginService.Login("username", "password");

            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(authDetails, new ValidationContext(authDetails), validationResults, true);

            Assert.True(isValid);
        }
    }
}