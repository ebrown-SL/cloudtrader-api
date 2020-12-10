using CloudTrader.Api.Controllers;
using CloudTrader.Users.Domain;
using CloudTrader.Users.Domain.Models;
using CloudTrader.Users.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace CloudTrader.Api.Tests.Controllers
{
    internal class UserControllerTests
    {
        private UserController mockUserController;
        private UserService mockUserService;

        private Mock<IUserRepository> mockUserRepository;

        private User mockUser;

        private readonly string mockUsername = "test@test.com";
        private readonly Guid mockUserId = new Guid();
        private readonly byte[] mockPasswordHash = new byte[] { 1, 2, 3 };
        private readonly byte[] mockPasswordSalt = new byte[] { 4, 5, 6 };
        private readonly Guid mockTraderId = new Guid();

        [SetUp]
        public void SetupMockConfig()
        {
            mockUserRepository = new Mock<IUserRepository>();
            var mockTraderApiClient = new Mock<ITraderApiClientTechDebt>();
            var mockMineApiClient = new Mock<IMineApiClientTechDebt>();

            mockUser = new User(mockUserId, mockUsername, mockPasswordHash, mockPasswordSalt, mockTraderId);
            mockUserService = new UserService(mockUserRepository.Object, mockTraderApiClient.Object, mockMineApiClient.Object);
            mockUserController = new UserController(mockUserService);
        }

        [Test]
        [Ignore("Need to mock context")]
        public async Task GetUser_WithValidUserId_Returns200OkWithUser()
        {
            mockUserRepository
                .Setup(mock => mock.GetUser(mockUserId))
                .ReturnsAsync(mockUser);
            var result = await mockUserController.GetUser();

            Assert.IsInstanceOf<OkObjectResult>(result);

            var okResult = (OkObjectResult)result;

            Assert.IsAssignableFrom<User>(okResult);
        }
    }
}