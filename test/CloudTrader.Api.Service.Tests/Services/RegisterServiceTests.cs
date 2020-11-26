using CloudTrader.Api.Service.Exceptions;
using CloudTrader.Api.Service.Interfaces;
using CloudTrader.Api.Service.Models;
using CloudTrader.Api.Service.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace CloudTrader.Api.Service.Tests.Services
{
    public class RegisterServiceTests
    {
        [Test]
        public void Register_WithUsernameAlreadyTaken_ThrowsUsernameAlreadyExistsException()
        {
            var mockUserRepository = new Mock<IUserRepository>();
            var mockTokenGenerator = new Mock<ITokenGenerator>();
            var mockPasswordUtils = new Mock<IPasswordUtils>();
            var registerService = new RegisterService(
                mockUserRepository.Object,
                mockTokenGenerator.Object,
                mockPasswordUtils.Object,
                new Mock<ITraderApiClient>().Object);

            mockUserRepository.Setup(mock => mock.GetUserByName(It.IsAny<string>())).ReturnsAsync(new User());

            Assert.ThrowsAsync<UsernameAlreadyExistsException>(async () => await registerService.Register("username", "password"));
        }

        [Test]
        public async Task Register_WithValidUsernameAndPassword_ReturnsValidAuthDetails()
        {
            var mockUserRepository = new Mock<IUserRepository>();
            var mockTokenGenerator = new Mock<ITokenGenerator>();
            var mockPasswordUtils = new Mock<IPasswordUtils>();
            var registerService = new RegisterService(
                mockUserRepository.Object,
                mockTokenGenerator.Object,
                mockPasswordUtils.Object,
                new Mock<ITraderApiClient>().Object);

            mockTokenGenerator.Setup(mock => mock.GenerateToken(It.IsAny<Guid>())).Returns("token");

            var authDetails = await registerService.Register("username", "password");

            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(authDetails, new ValidationContext(authDetails), validationResults, true);

            Assert.True(isValid);
        }

        public class Register
        {
            private Mock<IUserRepository> _mockUserRepository;
            private Mock<ITokenGenerator> _mockTokenGenerator;
            private Mock<IPasswordUtils> _mockPasswordUtils;
            private Mock<ITraderApiClient> _mockTraderApiService;

            private RegisterService _objectUnderTest;

            private const string dummyPassword = "password";
            private readonly byte[] dummyPasswordHash = new byte[] { 1, 2, 3 };
            private readonly byte[] dummyPasswordSalt = new byte[] { 4, 5, 6 };

            private readonly Guid dummyTraderId = new Guid();

            [SetUp]
            public void SetupEach()
            {
                _mockUserRepository = new Mock<IUserRepository>();
                _mockUserRepository
                    .Setup(mock => mock.SaveUser(It.IsAny<User>()))
                    .Returns(Task.FromResult(dummyTraderId));

                _mockTokenGenerator = new Mock<ITokenGenerator>();
                _mockTokenGenerator
                    .Setup(mock => mock.GenerateToken(It.IsAny<Guid>()))
                    .Returns("token");

                _mockPasswordUtils = new Mock<IPasswordUtils>();
                _mockPasswordUtils
                    .Setup(mock => mock.CreatePasswordHash(dummyPassword))
                    .Returns((dummyPasswordHash, dummyPasswordSalt));

                _mockTraderApiService = new Mock<ITraderApiClient>();
                _mockTraderApiService
                    .Setup(mock => mock.CreateTrader())
                    .Returns(Task.FromResult(dummyTraderId));

                _objectUnderTest = new RegisterService(
                    _mockUserRepository.Object,
                    _mockTokenGenerator.Object,
                    _mockPasswordUtils.Object,
                    _mockTraderApiService.Object);
            }

            // Test that user repository is given a user at the point where .Register is called
            [Test]
            public async Task CallsUserRepositoryWhenRegisteringUser()
            {
                var authDetails = await _objectUnderTest.Register("username", dummyPassword);

                Assert.That(
                    authDetails.Id,
                    Is.EqualTo(dummyTraderId),
                    "ID in return value should be the one returned by the user repository");

                Assert.That(
                    authDetails.Username,
                    Is.EqualTo("username"));

                Assert.That(
                    authDetails.Token,
                    Is.EqualTo("token"),
                    "Auth token should be the one from the mock token generator");

                _mockUserRepository.Verify(mock =>
                    mock.SaveUser(
                        It.Is<User>(user =>
                            user.Username == "username"
                        )
                    )
                );
            }

            [Test]
            public async Task GeneratesExpectedPasswordHashAndSalt()
            {
                var authDetails = await _objectUnderTest.Register("username", dummyPassword);

                _mockUserRepository.Verify(mock =>
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
                var authDetails = await _objectUnderTest.Register("username", dummyPassword);

                _mockUserRepository.Verify(mock =>
                    mock.SaveUser(
                        It.Is<User>(user =>
                            user.TraderId == dummyTraderId
                        )
                    )
                );

                _mockTraderApiService.Verify(mock =>
                    mock.CreateTrader()
                );
            }
        }
    }
}