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
    public class TravelServiceTests
    {
        private Mock<IRepository<Travel>> _mockTravelRepository;
        private TravelService _travelService;

        [SetUp]
        public void SetUp()
        {
            // Mock the repository
            _mockTravelRepository = new Mock<IRepository<Travel>>();

            // Initialize the service with the mocked repository
            _travelService = new TravelService(_mockTravelRepository.Object);
        }

        #region AddTravelAsync

        [Test]
        public async Task AddTravelAsync_ShouldAddTravel()
        {
            // Arrange
            var travel = new Travel { Id = 1 };
            _mockTravelRepository.Setup(r => r.AddAsync(travel)).Returns(Task.CompletedTask);

            // Act
            await _travelService.AddTravelAsync(travel);

            // Assert
            _mockTravelRepository.Verify(r => r.AddAsync(travel), Times.Once);
        }

        #endregion

        #region UpdateTravelAsync

        [Test]
        public async Task UpdateTravelAsync_ShouldUpdateTravel()
        {
            // Arrange
            var travel = new Travel { Id = 1 };
            _mockTravelRepository.Setup(r => r.UpdateAsync(travel)).Returns(Task.CompletedTask);

            // Act
            await _travelService.UpdateTravelAsync(travel);

            // Assert
            _mockTravelRepository.Verify(r => r.UpdateAsync(travel), Times.Once);
        }

        #endregion

        #region DeleteTravelAsync

        [Test]
        public async Task DeleteTravelAsync_ShouldDeleteTravel()
        {
            // Arrange
            var travel = new Travel { Id = 1 };
            _mockTravelRepository.Setup(r => r.DeleteAsync(travel)).Returns(Task.CompletedTask);

            // Act
            await _travelService.DeleteTravelAsync(travel);

            // Assert
            _mockTravelRepository.Verify(r => r.DeleteAsync(travel), Times.Once);
        }

        #endregion

        #region DeleteTravelByIdAsync

        [Test]
        public async Task DeleteTravelByIdAsync_ShouldDeleteTravelById()
        {
            // Arrange
            int travelId = 1;
            _mockTravelRepository.Setup(r => r.DeleteByIdAsync(travelId)).Returns(Task.CompletedTask);

            // Act
            await _travelService.DeleteTravelByIdAsync(travelId);

            // Assert
            _mockTravelRepository.Verify(r => r.DeleteByIdAsync(travelId), Times.Once);
        }

        #endregion

        #region GetAll

        [Test]
        public void GetAll_ShouldReturnAllTravels()
        {
            // Arrange
            var travels = new List<Travel>
            {
                new Travel { Id = 1 },
                new Travel { Id = 2 }
            }.AsQueryable();

            _mockTravelRepository.Setup(r => r.GetAll()).Returns(travels);

            // Act
            var result = _travelService.GetAll();

            // Assert
            Assert.AreEqual(2, result.Count());
        }

        #endregion

        #region CombinedInclude

        [Test]
        public void CombinedInclude_ShouldIncludeRelatedEntities()
        {
            // Arrange
            var travels = new List<Travel>
            {
                new Travel { Id = 1 },
                new Travel { Id = 2 }
            }.AsQueryable();

            _mockTravelRepository.Setup(r => r.GetAllQuery()).Returns(travels);

            // Act
            var result = _travelService.CombinedInclude(t => t.Id);

            // Assert
            Assert.AreEqual(2, result.Count());
        }

        #endregion

        #region GetTravelByIdAsync

        [Test]
        public async Task GetTravelByIdAsync_ShouldReturnTravel()
        {
            // Arrange
            int travelId = 1;
            var travel = new Travel { Id = travelId };
            _mockTravelRepository.Setup(r => r.GetByIdAsync(travelId)).ReturnsAsync(travel);

            // Act
            var result = await _travelService.GetTravelByIdAsync(travelId);

            // Assert
            Assert.AreEqual(travel, result);
        }

        #endregion

        #region GetTravelAsync with Filter

        [Test]
        public async Task GetTravelAsync_WithFilter_ShouldReturnFilteredTravel()
        {
            // Arrange
            var filter = Expression.Lambda<Func<Travel, bool>>(
                Expression.Call(
                    Expression.Property(Expression.Parameter(typeof(Travel), "t"), "Name"),
                    typeof(string).GetMethod("Contains", new[] { typeof(string) }),
                    Expression.Constant("Test")
                ),
                Expression.Parameter(typeof(Travel), "t")
            );

            var travels = new List<Travel>
            {
                new Travel { Id = 1 },
                new Travel { Id = 2 }
            };

            _mockTravelRepository.Setup(r => r.GetAsync(filter)).ReturnsAsync(travels.First());

            // Act
            var result = await _travelService.GetTravelAsync(filter);

            // Assert
            Assert.AreEqual(travels.First(), result);
        }

        #endregion

        #region GetAllTravelAsync with Filter

        [Test]
        public async Task GetAllTravelAsync_WithFilter_ShouldReturnFilteredTravels()
        {
            // Arrange
            var filter = Expression.Lambda<Func<Travel, bool>>(
                Expression.Call(
                    Expression.Property(Expression.Parameter(typeof(Travel), "t"), "Name"),
                    typeof(string).GetMethod("Contains", new[] { typeof(string) }),
                    Expression.Constant("Test")
                ),
                Expression.Parameter(typeof(Travel), "t")
            );

            var travels = new List<Travel>
            {
                new Travel { Id = 1 },
                new Travel { Id = 2 }
            };

            _mockTravelRepository.Setup(r => r.GetAllAsync(filter)).ReturnsAsync(travels);

            // Act
            var result = await _travelService.GetAllTravelAsync(filter);

            // Assert
            Assert.AreEqual(2, result.Count());
        }

        #endregion

        #region GetAllTravelAsync without Filter

        [Test]
        public async Task GetAllTravelAsync_WithoutFilter_ShouldReturnAllTravels()
        {
            // Arrange
            var travels = new List<Travel>
            {
                new Travel { Id = 1 },
                new Travel { Id = 2}
            };

            _mockTravelRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(travels);

            // Act
            var result = await _travelService.GetAllTravelAsync();

            // Assert
            Assert.AreEqual(2, result.Count());
        }

        #endregion
    }
}
