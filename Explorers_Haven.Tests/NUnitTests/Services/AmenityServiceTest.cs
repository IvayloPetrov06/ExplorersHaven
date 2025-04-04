using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Explorers_Haven.Core.Services;
using Explorers_Haven.DataAccess.Repository;
using Explorers_Haven.Models;

namespace Explorers_Haven.Tests
{
    [TestFixture]
    public class AmenityServiceTests
    {
        private Mock<IRepository<Amenity>> _mockRepo;
        private AmenityService _amenityService;

        [SetUp]
        public void Setup()
        {
            _mockRepo = new Mock<IRepository<Amenity>>();
            _amenityService = new AmenityService(_mockRepo.Object);
        }

        #region Add Amenity

        [Test]
        public async Task AddAmenityAsync_ShouldCallRepoAddAsync()
        {
            // Arrange
            var amenity = new Amenity { Id = 1, Name = "Free Wifi" };

            // Act
            await _amenityService.AddAmenityAsync(amenity);

            // Assert
            _mockRepo.Verify(r => r.AddAsync(It.Is<Amenity>(a => a == amenity)), Times.Once);
        }

        #endregion

        #region Update Amenity

        [Test]
        public async Task UpdateAmenityAsync_ShouldCallRepoUpdateAsync()
        {
            // Arrange
            var amenity = new Amenity { Id = 1, Name = "Updated Wifi" };

            // Act
            await _amenityService.UpdateAmenityAsync(amenity);

            // Assert
            _mockRepo.Verify(r => r.UpdateAsync(It.Is<Amenity>(a => a == amenity)), Times.Once);
        }

        #endregion

        #region Delete Amenity

        [Test]
        public async Task DeleteAmenityAsync_ShouldCallRepoDeleteAsync()
        {
            // Arrange
            var amenity = new Amenity { Id = 1, Name = "Free Wifi" };

            // Act
            await _amenityService.DeleteAmenityAsync(amenity);

            // Assert
            _mockRepo.Verify(r => r.DeleteAsync(It.Is<Amenity>(a => a == amenity)), Times.Once);
        }

        #endregion

        #region Delete Amenity by ID

        [Test]
        public async Task DeleteAmenityByIdAsync_ShouldCallRepoDeleteByIdAsync()
        {
            // Arrange
            var amenityId = 1;

            // Act
            await _amenityService.DeleteAmenityByIdAsync(amenityId);

            // Assert
            _mockRepo.Verify(r => r.DeleteByIdAsync(It.Is<int>(id => id == amenityId)), Times.Once);
        }

        #endregion

        #region Get All Amenities

        [Test]
        public void GetAll_ShouldReturnAllAmenities()
        {
            // Arrange
            var amenities = new List<Amenity>
            {
                new Amenity { Id = 1, Name = "Free Wifi" },
                new Amenity { Id = 2, Name = "Pool" }
            }.AsQueryable();

            _mockRepo.Setup(r => r.GetAll()).Returns(amenities);

            // Act
            var result = _amenityService.GetAll();

            // Assert
            Assert.AreEqual(2, result.Count());
        }

        #endregion

        #region Get All Amenities Async

        [Test]
        public async Task GetAllAmenitiesAsync_ShouldReturnAllAmenitiesAsync()
        {
            // Arrange
            var amenities = new List<Amenity>
            {
                new Amenity { Id = 1, Name = "Free Wifi" },
                new Amenity { Id = 2, Name = "Pool" }
            };

            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(amenities);

            // Act
            var result = await _amenityService.GetAllAmenitiesAsync();

            // Assert
            Assert.AreEqual(2, result.Count());
        }

        #endregion

        #region Get Amenity by ID

        [Test]
        public async Task GetAmenityByIdAsync_ShouldReturnAmenity()
        {
            // Arrange
            var amenityId = 1;
            var amenity = new Amenity { Id = amenityId, Name = "Free Wifi" };
            _mockRepo.Setup(r => r.GetByIdAsync(amenityId)).ReturnsAsync(amenity);

            // Act
            var result = await _amenityService.GetAmenityByIdAsync(amenityId);

            // Assert
            Assert.AreEqual(amenity, result);
            _mockRepo.Verify(r => r.GetByIdAsync(It.Is<int>(id => id == amenityId)), Times.Once);
        }

        #endregion

        #region Get Amenity Async with Filter

        [Test]
        public async Task GetAmenityAsync_ShouldReturnAmenityWithFilter()
        {
            // Arrange
            var amenity = new Amenity { Id = 1, Name = "Free Wifi" };
            _mockRepo.Setup(r => r.GetAsync(It.IsAny<Expression<Func<Amenity, bool>>>()))
                .ReturnsAsync(amenity);

            // Act
            var result = await _amenityService.GetAmenityAsync(a => a.Name == "Free Wifi");

            // Assert
            Assert.AreEqual(amenity, result);
        }

        #endregion

        #region All With Include

        [Test]
        public void AllWithInclude_ShouldIncludeProperties()
        {
            // Arrange
            var amenities = new List<Amenity>
            {
                new Amenity { Id = 1, Name = "Free Wifi" },
                new Amenity { Id = 2, Name = "Pool" }
            }.AsQueryable();

            _mockRepo.Setup(r => r.GetAllQuery()).Returns(amenities);

            // Act
            var result = _amenityService.AllWithInclude(a => a.Name);

            // Assert
            Assert.AreEqual(2, result.Count());
        }

        #endregion

        #region Get All Amenities with Filter

        [Test]
        public async Task GetAllAmenitiesAsync_WithFilter_ShouldReturnFilteredResults()
        {
            // Arrange
            var amenities = new List<Amenity>
            {
                new Amenity { Id = 1, Name = "Free Wifi" },
                new Amenity { Id = 2, Name = "Pool" }
            };

            _mockRepo.Setup(r => r.GetAllAsync(It.IsAny<Expression<Func<Amenity, bool>>>()))
                .ReturnsAsync((Expression<Func<Amenity, bool>> filter) => amenities.Where(filter.Compile()).ToList());

            // Act
            var result = await _amenityService.GetAllAmenitiesAsync(a => a.Name == "Free Wifi");

            // Assert
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("Free Wifi", result.First().Name);
        }

        #endregion
    }
}
