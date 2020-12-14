using CloudTrader.Users.Domain.Models;
using CloudTrader.Users.Domain.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace CloudTrader.Users.Domain.Tests.Services
{
    public class UserServiceTests
    {
#nullable disable
        private UserService testUserService;
        private Mock<IUserRepository> mockUserRepository;

        private User mockUser;
#nullable restore

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

            testUserService = new UserService(mockUserRepository.Object, mockTraderApiClient.Object, mockMineApiClient.Object);
            mockUser = new User(mockUserId, mockUsername, mockPasswordHash, mockPasswordSalt, mockTraderId);
        }

        [Test]
        public async Task GetUser_WithValidUserId_ReturnsCorrectUser()
        {
            mockUserRepository
                .Setup(mock => mock.GetUser(mockUserId))
                .ReturnsAsync(mockUser);

            var result = await testUserService.GetUser(mockUserId);

            Assert.AreEqual(mockUser, result);
        }
    }
}