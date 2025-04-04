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
    public class StayAmenityServiceTests
    {
        private Mock<IRepository<StayAmenity>> _mockStayAmenityRepository;
        private StayAmenityService _stayAmenityService;

        [SetUp]
        public void SetUp()
        {
            // Mock the repository
            _mockStayAmenityRepository = new Mock<IRepository<StayAmenity>>();

            // Initialize the service with the mocked repository
            _stayAmenityService = new StayAmenityService(_mockStayAmenityRepository.Object);
        }

        #region AddStayAmenityAsync

        [Test]
        public async Task AddStayAmenityAsync_ShouldAddStayAmenity()
        {
            // Arrange
            var stayAmenity = new StayAmenity { Id = 1, AmenityId = 1, StayId = 1 };

            _mockStayAmenityRepository.Setup(r => r.AddAsync(stayAmenity)).Returns(Task.CompletedTask);

            // Act
            await _stayAmenityService.AddStayAmenityAsync(stayAmenity);

            // Assert
            _mockStayAmenityRepository.Verify(r => r.AddAsync(stayAmenity), Times.Once);
        }

        #endregion

        #region DeleteStayAmenityAsync

        [Test]
        public async Task DeleteStayAmenityAsync_ShouldDeleteStayAmenity()
        {
            // Arrange
            var stayAmenity = new StayAmenity { Id = 1, AmenityId = 1, StayId = 1 };

            _mockStayAmenityRepository.Setup(r => r.DeleteAsync(stayAmenity)).Returns(Task.CompletedTask);

            // Act
            await _stayAmenityService.DeleteStayAmenityAsync(stayAmenity);

            // Assert
            _mockStayAmenityRepository.Verify(r => r.DeleteAsync(stayAmenity), Times.Once);
        }

        #endregion

        #region DeleteStayAmenityByIdAsync

        [Test]
        public async Task DeleteStayAmenityByIdAsync_ShouldDeleteStayAmenityById()
        {
            // Arrange
            int stayAmenityId = 1;
            _mockStayAmenityRepository.Setup(r => r.DeleteByIdAsync(stayAmenityId)).Returns(Task.CompletedTask);

            // Act
            await _stayAmenityService.DeleteStayAmenityByIdAsync(stayAmenityId);

            // Assert
            _mockStayAmenityRepository.Verify(r => r.DeleteByIdAsync(stayAmenityId), Times.Once);
        }

        #endregion

        #region GetAll

        [Test]
        public void GetAll_ShouldReturnAllStayAmenities()
        {
            // Arrange
            var stayAmenities = new List<StayAmenity>
            {
                new StayAmenity { Id = 1, AmenityId = 1, StayId = 1 },
                new StayAmenity { Id = 2, AmenityId = 2, StayId = 1 }
            }.AsQueryable();

            _mockStayAmenityRepository.Setup(r => r.GetAll()).Returns(stayAmenities);

            // Act
            var result = _stayAmenityService.GetAll();

            // Assert
            Assert.AreEqual(2, result.Count());
        }

        #endregion

        #region GetStayAmenityByIdAsync

        [Test]
        public async Task GetStayAmenityByIdAsync_ShouldReturnStayAmenity()
        {
            // Arrange
            int stayAmenityId = 1;
            var stayAmenity = new StayAmenity { Id = stayAmenityId, AmenityId = 1, StayId = 1 };
            _mockStayAmenityRepository.Setup(r => r.GetByIdAsync(stayAmenityId)).ReturnsAsync(stayAmenity);

            // Act
            var result = await _stayAmenityService.GetStayAmenityByIdAsync(stayAmenityId);

            // Assert
            Assert.AreEqual(stayAmenity, result);
        }

        #endregion

        #region UpdateStayAmenityAsync

        [Test]
        public async Task UpdateStayAmenityAsync_ShouldUpdateStayAmenity()
        {
            // Arrange
            var stayAmenity = new StayAmenity { Id = 1, AmenityId = 1, StayId = 1 };
            _mockStayAmenityRepository.Setup(r => r.UpdateAsync(stayAmenity)).Returns(Task.CompletedTask);

            // Act
            await _stayAmenityService.UpdateStayAmenityAsync(stayAmenity);

            // Assert
            _mockStayAmenityRepository.Verify(r => r.UpdateAsync(stayAmenity), Times.Once);
        }

        #endregion

        #region GetAllStayAmenitysAsync with Filter

        [Test]
        public async Task GetAllStayAmenitysAsync_WithFilter_ShouldReturnFilteredStayAmenities()
        {
            // Arrange
            var stayAmenities = new List<StayAmenity>
            {
                new StayAmenity { Id = 1, AmenityId = 1, StayId = 1 },
                new StayAmenity { Id = 2, AmenityId = 2, StayId = 1 }
            };

            Expression<Func<StayAmenity, bool>> filter = sa => sa.StayId == 1;
            _mockStayAmenityRepository.Setup(r => r.GetAllAsync(filter)).ReturnsAsync(stayAmenities);

            // Act
            var result = await _stayAmenityService.GetAllStayAmenitysAsync(filter);

            // Assert
            Assert.AreEqual(2, result.Count());
        }

        #endregion

        #region GetAllStayAmenitysAsync without Filter

        [Test]
        public async Task GetAllStayAmenitysAsync_WithoutFilter_ShouldReturnAllStayAmenities()
        {
            // Arrange
            var stayAmenities = new List<StayAmenity>
            {
                new StayAmenity { Id = 1, AmenityId = 1, StayId = 1 },
                new StayAmenity { Id = 2, AmenityId = 2, StayId = 2 }
            };

            _mockStayAmenityRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(stayAmenities);

            // Act
            var result = await _stayAmenityService.GetAllStayAmenitysAsync();

            // Assert
            Assert.AreEqual(2, result.Count());
        }

        #endregion

    }
}

