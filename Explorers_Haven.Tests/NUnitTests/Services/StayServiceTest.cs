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
    public class StayServiceTests
    {
        private Mock<IRepository<Stay>> _mockStayRepository;
        private StayService _stayService;

        [SetUp]
        public void SetUp()
        {
            // Mock the repository
            _mockStayRepository = new Mock<IRepository<Stay>>();

            // Initialize the service with the mocked repository
            _stayService = new StayService(_mockStayRepository.Object);
        }

        #region AddStayAsync

        [Test]
        public async Task AddStayAsync_ShouldAddStay()
        {
            // Arrange
            var stay = new Stay { Id = 1, Name = "Test Stay" };
            _mockStayRepository.Setup(r => r.AddAsync(stay)).Returns(Task.CompletedTask);

            // Act
            await _stayService.AddStayAsync(stay);

            // Assert
            _mockStayRepository.Verify(r => r.AddAsync(stay), Times.Once);
        }

        #endregion

        #region UpdateStayAsync

        [Test]
        public async Task UpdateStayAsync_ShouldUpdateStay()
        {
            // Arrange
            var stay = new Stay { Id = 1, Name = "Updated Stay" };
            _mockStayRepository.Setup(r => r.UpdateAsync(stay)).Returns(Task.CompletedTask);

            // Act
            await _stayService.UpdateStayAsync(stay);

            // Assert
            _mockStayRepository.Verify(r => r.UpdateAsync(stay), Times.Once);
        }

        #endregion

        #region DeleteStayAsync

        [Test]
        public async Task DeleteStayAsync_ShouldDeleteStay()
        {
            // Arrange
            var stay = new Stay { Id = 1, Name = "Test Stay" };
            _mockStayRepository.Setup(r => r.DeleteAsync(stay)).Returns(Task.CompletedTask);

            // Act
            await _stayService.DeleteStayAsync(stay);

            // Assert
            _mockStayRepository.Verify(r => r.DeleteAsync(stay), Times.Once);
        }

        #endregion

        #region DeleteStayByIdAsync

        [Test]
        public async Task DeleteStayByIdAsync_ShouldDeleteStayById()
        {
            // Arrange
            int stayId = 1;
            _mockStayRepository.Setup(r => r.DeleteByIdAsync(stayId)).Returns(Task.CompletedTask);

            // Act
            await _stayService.DeleteStayByIdAsync(stayId);

            // Assert
            _mockStayRepository.Verify(r => r.DeleteByIdAsync(stayId), Times.Once);
        }

        #endregion

        #region GetAll

        [Test]
        public void GetAll_ShouldReturnAllStays()
        {
            // Arrange
            var stays = new List<Stay>
            {
                new Stay { Id = 1, Name = "Stay 1" },
                new Stay { Id = 2, Name = "Stay 2" }
            }.AsQueryable();

            _mockStayRepository.Setup(r => r.GetAll()).Returns(stays);

            // Act
            var result = _stayService.GetAll();

            // Assert
            Assert.AreEqual(2, result.Count());
        }

        #endregion

        #region CombinedInclude

        [Test]
        public void CombinedInclude_ShouldIncludeRelatedEntities()
        {
            // Arrange
            var stays = new List<Stay>
            {
                new Stay { Id = 1, Name = "Stay 1" },
                new Stay { Id = 2, Name = "Stay 2" }
            }.AsQueryable();

            _mockStayRepository.Setup(r => r.GetAllQuery()).Returns(stays);

            // Act
            var result = _stayService.CombinedInclude(s => s.Name);

            // Assert
            Assert.AreEqual(2, result.Count());
        }

        #endregion

        #region GetStayByIdAsync

        [Test]
        public async Task GetStayByIdAsync_ShouldReturnStay()
        {
            // Arrange
            int stayId = 1;
            var stay = new Stay { Id = stayId, Name = "Test Stay" };
            _mockStayRepository.Setup(r => r.GetByIdAsync(stayId)).ReturnsAsync(stay);

            // Act
            var result = await _stayService.GetStayByIdAsync(stayId);

            // Assert
            Assert.AreEqual(stay, result);
        }

        #endregion

        #region GetStayAsync with Filter

        [Test]
        public async Task GetStayAsync_WithFilter_ShouldReturnFilteredStay()
        {
            // Arrange
            var filter = Expression.Lambda<Func<Stay, bool>>(
                Expression.Call(
                    Expression.Property(Expression.Parameter(typeof(Stay), "s"), "Name"),
                    typeof(string).GetMethod("Contains", new[] { typeof(string) }),
                    Expression.Constant("Test")
                ),
                Expression.Parameter(typeof(Stay), "s")
            );

            var stays = new List<Stay>
    {
        new Stay { Id = 1, Name = "Test Stay 1" },
        new Stay { Id = 2, Name = "Test Stay 2" }
    };

            _mockStayRepository.Setup(r => r.GetAsync(filter)).ReturnsAsync(stays.First());

            // Act
            var result = await _stayService.GetStayAsync(filter);

            // Assert
            Assert.AreEqual(stays.First(), result);
        }

        #endregion

        #region GetAllStayAsync with Filter

        [Test]
        public async Task GetAllStayAsync_WithFilter_ShouldReturnFilteredStays()
        {
            // Arrange
            var filter = Expression.Lambda<Func<Stay, bool>>(
                Expression.Call(
                    Expression.Property(Expression.Parameter(typeof(Stay), "s"), "Name"),
                    typeof(string).GetMethod("Contains", new[] { typeof(string) }),
                    Expression.Constant("Test")
                ),
                Expression.Parameter(typeof(Stay), "s")
            );
            var stays = new List<Stay>
            {
                new Stay { Id = 1, Name = "Stay 1" },
                new Stay { Id = 2, Name = "Stay 2" }
            };

            _mockStayRepository.Setup(r => r.GetAllAsync(filter)).ReturnsAsync(stays);

            // Act
            var result = await _stayService.GetAllStayAsync(filter);

            // Assert
            Assert.AreEqual(2, result.Count());
        }
        //
        #endregion

        #region GetAllStayAsync without Filter

        [Test]
        public async Task GetAllStayAsync_WithoutFilter_ShouldReturnAllStays()
        {
            // Arrange
            var stays = new List<Stay>
            {
                new Stay { Id = 1, Name = "Stay 1" },
                new Stay { Id = 2, Name = "Stay 2" }
            };

            _mockStayRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(stays);

            // Act
            var result = await _stayService.GetAllStayAsync();

            // Assert
            Assert.AreEqual(2, result.Count());
        }

        #endregion
    }
}
