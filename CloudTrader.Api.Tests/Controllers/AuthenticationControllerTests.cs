using System.Threading.Tasks;
using CloudTrader.Api.Controllers;
using CloudTrader.Api.Models;
using CloudTrader.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace CloudTrader.Api.Tests.Controllers
{
    public class AuthenticationControllerTests
    {
        [Test]
        public async Task Register_WithDuplicateUsername_ReturnsConflict()
        {
            var mockAuthenticationService = new Mock<IAuthenticationService>();
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(service => service.GetByUsername(It.IsAny<string>())).ReturnsAsync(new UserModel { Username = "username" });
            var authenticationController = new AuthenticationController(mockAuthenticationService.Object, mockUserService.Object);

            var result = await authenticationController.Register(new AuthenticationModel());

            Assert.IsInstanceOf<ConflictObjectResult>(result);
        }

        [Test]
        public async Task Register_WithValid_ReturnsOk()
        {
            var mockAuthenticationService = new Mock<IAuthenticationService>();
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(service => service.Create(It.IsAny<UserModel>())).ReturnsAsync(new UserModel { Id = 1, Username = "username" });
            var authenticationController = new AuthenticationController(mockAuthenticationService.Object, mockUserService.Object);

            var result = await authenticationController.Register(new AuthenticationModel());

            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public async Task Login_WithNotFoundUsername_ReturnsBadRequest()
        {
            var mockAuthenticationService = new Mock<IAuthenticationService>();
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(service => service.GetByUsername(It.IsAny<string>())).ReturnsAsync((UserModel)null);
            var authenticationController = new AuthenticationController(mockAuthenticationService.Object, mockUserService.Object);

            var result = await authenticationController.Login(new AuthenticationModel());

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task Login_WithIncorrectPassword_ReturnsBadRequest()
        {
            var mockAuthenticationService = new Mock<IAuthenticationService>();
            mockAuthenticationService.Setup(service => service.VerifyPassword(It.IsAny<string>(), It.IsAny<byte[]>(), It.IsAny<byte[]>())).Returns(false);
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(service => service.GetByUsername(It.IsAny<string>())).ReturnsAsync(new UserModel());
            var authenticationController = new AuthenticationController(mockAuthenticationService.Object, mockUserService.Object);

            var result = await authenticationController.Login(new AuthenticationModel());

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task Login_WithValid_ReturnsOk()
        {
            var mockAuthenticationService = new Mock<IAuthenticationService>();
            mockAuthenticationService.Setup(service => service.VerifyPassword(It.IsAny<string>(), It.IsAny<byte[]>(), It.IsAny<byte[]>())).Returns(true);
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(service => service.GetByUsername(It.IsAny<string>())).ReturnsAsync(new UserModel());
            var authenticationController = new AuthenticationController(mockAuthenticationService.Object, mockUserService.Object);

            var result = await authenticationController.Login(new AuthenticationModel());

            Assert.IsInstanceOf<OkObjectResult>(result);
        }
    }
}
