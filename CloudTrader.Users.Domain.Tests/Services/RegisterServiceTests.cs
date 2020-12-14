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
    public class RegisterServiceTests
    {
        [Test]
        public void Register_WithUsernameAlreadyTaken_ThrowsUsernameAlreadyExistsException()
        {
            var mockUserRepository = new Mock<IUserRepository>();
            var mockPasswordUtils = new Mock<IPasswordUtils>();
            var registerService = new RegisterService(
                mockUserRepository.Object,
                mockPasswordUtils.Object,
                new Mock<ITraderApiClientTechDebt>().Object);

            mockUserRepository.Setup(mock => mock.GetUserByUsername(It.IsAny<string>())).ReturnsAsync(new User());

            Assert.ThrowsAsync<UsernameAlreadyExistsException>(async () => await registerService.Register("username", "password"));
        }

        [Test]
        public async Task Register_WithValidUsernameAndPassword_ReturnsValidAuthDetails()
        {
            var mockUserRepository = new Mock<IUserRepository>();
            var mockPasswordUtils = new Mock<IPasswordUtils>();
            var registerService = new RegisterService(
                mockUserRepository.Object,
                mockPasswordUtils.Object,
                new Mock<ITraderApiClientTechDebt>().Object);

            var authDetails = await registerService.Register("username", "password");

            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(authDetails, new ValidationContext(authDetails), validationResults, true);

            Assert.True(isValid);
        }

        public class Register
        {
#nullable disable
            private Mock<IUserRepository> mockUserRepository;
            private Mock<IPasswordUtils> mockPasswordUtils;
            private Mock<ITraderApiClientTechDebt> mockTraderApiService;

            private RegisterService objectUnderTest;
#nullable restore
            private const string dummyPassword = "password";
            private readonly byte[] dummyPasswordHash = new byte[] { 1, 2, 3 };
            private readonly byte[] dummyPasswordSalt = new byte[] { 4, 5, 6 };

            private readonly Guid dummyTraderId = new Guid();

            [SetUp]
            public void SetupEach()
            {
                mockUserRepository = new Mock<IUserRepository>();
                mockUserRepository
                    .Setup(mock => mock.SaveUser(It.IsAny<User>()))
                    .Returns(Task.FromResult(dummyTraderId));

                mockPasswordUtils = new Mock<IPasswordUtils>();
                mockPasswordUtils
                    .Setup(mock => mock.CreatePasswordHash(dummyPassword))
                    .Returns((dummyPasswordHash, dummyPasswordSalt));

                mockTraderApiService = new Mock<ITraderApiClientTechDebt>();
                mockTraderApiService
                    .Setup(mock => mock.CreateTrader())
                    .Returns(Task.FromResult(dummyTraderId));

                objectUnderTest = new RegisterService(
                    mockUserRepository.Object,
                    mockPasswordUtils.Object,
                    mockTraderApiService.Object);
            }

            // Test that user repository is given a user at the point where .Register is called
            [Test]
            public async Task CallsUserRepositoryWhenRegisteringUser()
            {
                var authDetails = await objectUnderTest.Register("username", dummyPassword);

                Assert.That(
                    authDetails.Id,
                    Is.EqualTo(dummyTraderId),
                    "ID in return value should be the one returned by the user repository");

                Assert.That(
                    authDetails.Username,
                    Is.EqualTo("username"));
            }

            [Test]
            [Ignore("Need to determine why we aren't saving user when registering")]
            public async Task GeneratesExpectedPasswordHashAndSalt()
            {
                var authDetails = await objectUnderTest.Register("username", dummyPassword);

                mockUserRepository.Verify(mock =>
                    mock.SaveUser(
                        It.Is<User>(user =>
                            user.PasswordHash == dummyPasswordHash &&
                            user.PasswordSalt == dummyPasswordSalt
                        )
                    )
                );
            }

            [Test]
            public async Task RegisterServiceUsesTraderApiService()
            {
                var authDetails = await objectUnderTest.Register("username", dummyPassword);

                mockTraderApiService.Verify(mock =>
                    mock.CreateTrader()
                );
            }
        }
    }
}