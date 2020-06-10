using CloudTrader.Api.Helpers;
using CloudTrader.Api.Services;
using CloudTrader.Api.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System;

namespace CloudTrader.Api.Tests
{
    public class UserServiceTests
    {
        private DataContext _dataContext;
        private UserService _userService;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase(databaseName: "testdb").Options;
            _dataContext = new DataContext(options);
            _userService = new UserService(_dataContext);
        }

        [TearDown]
        public void TearDown()
        {
            _dataContext.Database.EnsureDeleted();
            _dataContext.Dispose();
        }

        [Test]
        public async Task Create_WithValidUser_ReturnsUser()
        {
            var user = new UserModel
            {
                Id = 1,
                Username = "username",
                PasswordHash = new byte[] { },
                PasswordSalt = new byte[] { }
            };
            Validator.ValidateObject(user, new ValidationContext(user));

            var returnedUser = await _userService.Create(user);

            Assert.AreEqual(returnedUser, user);
        }

        [Test]
        public async Task Create_WithDuplicateKey_ThrowsArgumentException()
        {
            var user = new UserModel
            {
                Id = 1,
                Username = "username",
                PasswordHash = new byte[] { },
                PasswordSalt = new byte[] { }
            };
            Validator.ValidateObject(user, new ValidationContext(user));

            await _userService.Create(user);

            Assert.ThrowsAsync<ArgumentException>(async () => await _userService.Create(user));
        }

        [Test]
        public async Task GetById_WithExistingUser_ReturnsUser()
        {
            var user = new UserModel
            {
                Id = 1,
                Username = "username",
                PasswordHash = new byte[] { },
                PasswordSalt = new byte[] { }
            };
            Validator.ValidateObject(user, new ValidationContext(user));

            await _userService.Create(user);

            var returnedUser = await _userService.GetById(user.Id);

            Assert.AreEqual(returnedUser, user);
        }

        [Test]
        public async Task GetById_WithNonExistingUser_ReturnsNull()
        {
            var returnedUser = await _userService.GetById(1);

            Assert.IsNull(returnedUser);
        }

        [Test]
        public async Task GetByUsername_WithExistingUser_ReturnsUser()
        {
            var user = new UserModel
            {
                Id = 1,
                Username = "username",
                PasswordHash = new byte[] { },
                PasswordSalt = new byte[] { }
            };
            Validator.ValidateObject(user, new ValidationContext(user));

            await _userService.Create(user);

            var returnedUser = await _userService.GetByUsername(user.Username);

            Assert.AreEqual(returnedUser, user);
        }

        [Test]
        public async Task GetByUsername_WithNonExistingUser_ThrowsError()
        {
            var returnedUser = await _userService.GetByUsername("username");

            Assert.IsNull(returnedUser);
        }
    }
}
