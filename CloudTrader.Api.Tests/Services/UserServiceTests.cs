using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using CloudTrader.Api.Exceptions;
using CloudTrader.Api.Helpers;
using CloudTrader.Api.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace CloudTrader.Api.Tests.Services
{
    public class UserServiceTests
    {
        private DataContext _dataContext;

        private IUserService _userService;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase(databaseName: "testdb").Options;
            _dataContext = new DataContext(options);

            var mockAuthenticationService = new Mock<IAuthenticationService>();
            var mockHash = new byte[] { };
            var mockSalt = new byte[] { };
            mockAuthenticationService.Setup(s => s.CreatePasswordHash(It.IsAny<string>(), out mockHash, out mockSalt));

            _userService = new UserService(_dataContext, mockAuthenticationService.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _dataContext.Database.EnsureDeleted();
            _dataContext.Dispose();
        }

        [Test]
        public async Task CreateUser_WithValidUser_ReturnsValidUser()
        {
            var user = await _userService.CreateUser("username", "password");

            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(user, new ValidationContext(user), validationResults);

            Assert.True(isValid);
        }

        [Test]
        public async Task CreateUser_WithDuplicateUsername_ThrowsUsernameAlreadyExistsException()
        {
            await _userService.CreateUser("username", "password");

            Assert.ThrowsAsync<UsernameAlreadyExistsException>(async () => await _userService.CreateUser("username", "password"));
        }

        [Test]
        public async Task GetUser_WithExistingId_ReturnsUser()
        {
            var user = await _userService.CreateUser("username", "password");

            var returnedUser = await _userService.GetUser(user.Id);

            Assert.AreEqual(returnedUser, user);
        }

        [Test]
        public void GetUser_WithNonExistingId_ThrowsUserNotFoundException()
        {
            Assert.ThrowsAsync<UserNotFoundException>(async () => await _userService.GetUser(1));
        }

        [Test]
        public async Task GetUser_WithExistingUsername_ReturnsUser()
        {
            var user = await _userService.CreateUser("username", "password");

            var returnedUser = await _userService.GetUser(user.Username);

            Assert.AreEqual(returnedUser, user);
        }

        [Test]
        public void GetUser_WithExistingUsername_ThrowsUserNotFoundException()
        {
            Assert.ThrowsAsync<UserNotFoundException>(async () => await _userService.GetUser("username"));
        }

        [Test]
        public async Task UserExists_WithExistingUsername_ReturnsTrue()
        {
            var user = await _userService.CreateUser("username", "password");

            var userExists = await _userService.UserExists(user.Username);

            Assert.True(userExists);
        }

        [Test]
        public async Task UserExists_WithNonExistingUsername_ReturnsFalse()
        {
            var userExists = await _userService.UserExists("username");

            Assert.False(userExists);
        }
    }
}
