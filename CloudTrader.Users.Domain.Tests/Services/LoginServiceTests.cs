using CloudTrader.Users.Domain;
using CloudTrader.Users.Domain.Exceptions;
using CloudTrader.Users.Domain.Helpers;
using CloudTrader.Users.Domain.Models;
using CloudTrader.Users.Domain.Services;
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
            var mockPasswordUtils = new Mock<IPasswordUtils>();
            var loginService = new LoginService(mockUserRepository.Object, mockPasswordUtils.Object);

            mockUserRepository.Setup(mock => mock.GetUser(It.IsAny<Guid>())).ReturnsAsync((User?)null);

            Assert.ThrowsAsync<UnauthorizedException>(async () => await loginService.Login("username", "password"));
        }

        [Test]
        public void Login_WithInvalidPassword_ThrowsUnauthorizedException()
        {
            var mockUserRepository = new Mock<IUserRepository>();
            var mockPasswordUtils = new Mock<IPasswordUtils>();
            var loginService = new LoginService(mockUserRepository.Object, mockPasswordUtils.Object);

            mockUserRepository.Setup(mock => mock.GetUser(It.IsAny<Guid>())).ReturnsAsync(new User());
            mockPasswordUtils.Setup(mock => mock.VerifyPassword(It.IsAny<string>(), It.IsAny<byte[]>(), It.IsAny<byte[]>())).Returns(false);

            Assert.ThrowsAsync<UnauthorizedException>(async () => await loginService.Login("username", "password"));
        }

        [Test]
        public async Task Login_WithValidUsernameAndPassword_ReturnsValidUserAsync()
        {
            var mockUserRepository = new Mock<IUserRepository>();
            var mockPasswordUtils = new Mock<IPasswordUtils>();
            var loginService = new LoginService(mockUserRepository.Object, mockPasswordUtils.Object);
            var mockUserId = new Guid();
            var mockUsername = "username";

            mockUserRepository.Setup(mock => mock.GetUserByUsername(mockUsername)).ReturnsAsync(new User { Id = mockUserId, Username = mockUsername });
            mockPasswordUtils.Setup(mock => mock.VerifyPassword(It.IsAny<string>(), It.IsAny<byte[]>(), It.IsAny<byte[]>())).Returns(true);

            var loginResult = await loginService.Login("username", "password");

            Assert.AreEqual(mockUserId, loginResult.Id);
        }
    }
}