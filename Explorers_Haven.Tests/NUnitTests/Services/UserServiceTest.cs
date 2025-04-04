using Moq;
using NUnit.Framework;
using Explorers_Haven.Core.Services;
using Explorers_Haven.Models;
using Explorers_Haven.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Explorers_Haven.Tests
{
    [TestFixture]
    public class UserServiceTests
    {
        private Mock<IRepository<User>> _mockUserRepository;
        private UserService _userService;

        [SetUp]
        public void SetUp()
        {
            // Mock the repository
            _mockUserRepository = new Mock<IRepository<User>>();

            // Initialize the service with the mocked repository
            _userService = new UserService(_mockUserRepository.Object);
        }

        #region AddUserAsync

        [Test]
        public async Task AddUserAsync_ShouldAddUser()
        {
            // Arrange
            var user = new User { Id = 1, Username = "testuser" };
            _mockUserRepository.Setup(r => r.AddAsync(user)).Returns(Task.CompletedTask);

            // Act
            await _userService.AddUserAsync(user);

            // Assert
            _mockUserRepository.Verify(r => r.AddAsync(user), Times.Once);
        }

        #endregion

        #region UpdateUserAsync

        [Test]
        public async Task UpdateUserAsync_ShouldUpdateUser()
        {
            // Arrange
            var user = new User { Id = 1, Username = "updateduser" };
            _mockUserRepository.Setup(r => r.UpdateAsync(user)).Returns(Task.CompletedTask);

            // Act
            await _userService.UpdateUserAsync(user);

            // Assert
            _mockUserRepository.Verify(r => r.UpdateAsync(user), Times.Once);
        }

        #endregion

        #region DeleteUserAsync

        [Test]
        public async Task DeleteUserAsync_ShouldDeleteUser()
        {
            // Arrange
            var user = new User { Id = 1, Username = "testuser" };
            _mockUserRepository.Setup(r => r.DeleteAsync(user)).Returns(Task.CompletedTask);

            // Act
            await _userService.DeleteUserAsync(user);

            // Assert
            _mockUserRepository.Verify(r => r.DeleteAsync(user), Times.Once);
        }

        #endregion

        #region DeleteUserByIdAsync

        [Test]
        public async Task DeleteUserByIdAsync_ShouldDeleteUserById()
        {
            // Arrange
            int userId = 1;
            _mockUserRepository.Setup(r => r.DeleteByIdAsync(userId)).Returns(Task.CompletedTask);

            // Act
            await _userService.DeleteUserByIdAsync(userId);

            // Assert
            _mockUserRepository.Verify(r => r.DeleteByIdAsync(userId), Times.Once);
        }

        #endregion

        #region GetAll

        [Test]
        public void GetAll_ShouldReturnAllUsers()
        {
            // Arrange
            var users = new List<User>
            {
                new User { Id = 1, Username = "User 1" },
                new User { Id = 2, Username = "User 2" }
            }.AsQueryable();

            _mockUserRepository.Setup(r => r.GetAll()).Returns(users);

            // Act
            var result = _userService.GetAll();

            // Assert
            Assert.AreEqual(2, result.Count());
        }

        #endregion

        #region GetUserByIdAsync

        [Test]
        public async Task GetUserByIdAsync_ShouldReturnUser()
        {
            // Arrange
            int userId = 1;
            var user = new User { Id = userId, Username = "testuser" };
            _mockUserRepository.Setup(r => r.GetByIdAsync(userId)).ReturnsAsync(user);

            // Act
            var result = await _userService.GetUserByIdAsync(userId);

            // Assert
            Assert.AreEqual(user, result);
        }

        #endregion

        #region GetUserAsync with Filter

        [Test]
        public async Task GetUserAsync_WithFilter_ShouldReturnFilteredUser()
        {
            // Arrange
            var filter = Expression.Lambda<Func<User, bool>>(
                Expression.Call(
                    Expression.Property(Expression.Parameter(typeof(User), "u"), "Username"),
                    typeof(string).GetMethod("Contains", new[] { typeof(string) }),
                    Expression.Constant("user")
                ),
                Expression.Parameter(typeof(User), "u")
            );

            var users = new List<User>
            {
                new User { Id = 1, Username = "testuser" },
                new User { Id = 2, Username = "anotheruser" }
            };

            _mockUserRepository.Setup(r => r.GetAsync(filter)).ReturnsAsync(users.First());

            // Act
            var result = await _userService.GetUserAsync(filter);

            // Assert
            Assert.AreEqual(users.First(), result);
        }

        #endregion

        #region GetAllUsersAsync with Filter

        [Test]
        public async Task GetAllUsersAsync_WithFilter_ShouldReturnFilteredUsers()
        {
            // Arrange
            var filter = Expression.Lambda<Func<User, bool>>(
                Expression.Call(
                    Expression.Property(Expression.Parameter(typeof(User), "u"), "Username"),
                    typeof(string).GetMethod("Contains", new[] { typeof(string) }),
                    Expression.Constant("user")
                ),
                Expression.Parameter(typeof(User), "u")
            );
            var users = new List<User>
            {
                new User { Id = 1, Username = "testuser" },
                new User { Id = 2, Username = "anotheruser" }
            };

            _mockUserRepository.Setup(r => r.GetAllAsync(filter)).ReturnsAsync(users);

            // Act
            var result = await _userService.GetAllUsersAsync(filter);

            // Assert
            Assert.AreEqual(2, result.Count());
        }

        #endregion

        #region GetAllUsersAsync without Filter

        [Test]
        public async Task GetAllUsersAsync_WithoutFilter_ShouldReturnAllUsers()
        {
            // Arrange
            var users = new List<User>
            {
                new User { Id = 1, Username = "User 1" },
                new User { Id = 2, Username = "User 2" }
            };

            _mockUserRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(users);

            // Act
            var result = await _userService.GetAllUsersAsync();

            // Assert
            Assert.AreEqual(2, result.Count());
        }

        #endregion

        #region GetAllUserNamesAsync

        [Test]
        public async Task GetAllUserNamesAsync_ShouldReturnAllUserNames()
        {
            // Arrange
            var users = new List<User>
            {
                new User { Id = 1, Username = "User 1" },
                new User { Id = 2, Username = "User 2" }
            };

            _mockUserRepository.Setup(r => r.GetAll()).Returns(users.AsQueryable());

            // Act
            var result = await _userService.GetAllUserNamesAsync();

            // Assert
            Assert.AreEqual(2, result.Count());
            Assert.Contains("User 1", result.ToList());
            Assert.Contains("User 2", result.ToList());
        }

        #endregion
    }
}
