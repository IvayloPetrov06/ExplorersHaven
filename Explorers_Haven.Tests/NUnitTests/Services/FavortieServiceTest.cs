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
    public class FavoriteServiceTests
    {
        private Mock<IRepository<Favorite>> _mockFavoriteRepository;
        private FavoriteService _favoriteService;

        [SetUp]
        public void SetUp()
        {
            // Mock the repository
            _mockFavoriteRepository = new Mock<IRepository<Favorite>>();

            // Initialize the service with the mocked repository
            _favoriteService = new FavoriteService(_mockFavoriteRepository.Object);
        }

        #region AddFavoriteAsync

        [Test]
        public async Task AddFavoriteAsync_ShouldAddFavorite()
        {
            // Arrange
            var favorite = new Favorite { Id = 1, OfferId = 1 };

            _mockFavoriteRepository.Setup(r => r.AddAsync(favorite)).Returns(Task.CompletedTask);

            // Act
            await _favoriteService.AddFavoriteAsync(favorite);

            // Assert
            _mockFavoriteRepository.Verify(r => r.AddAsync(favorite), Times.Once);
        }

        #endregion

        #region DeleteFavoriteAsync

        [Test]
        public async Task DeleteFavoriteAsync_ShouldDeleteFavorite()
        {
            // Arrange
            var favorite = new Favorite { Id = 1, OfferId = 1 };

            _mockFavoriteRepository.Setup(r => r.DeleteAsync(favorite)).Returns(Task.CompletedTask);

            // Act
            await _favoriteService.DeleteFavoriteAsync(favorite);

            // Assert
            _mockFavoriteRepository.Verify(r => r.DeleteAsync(favorite), Times.Once);
        }

        #endregion

        #region DeleteFavoriteByIdAsync

        [Test]
        public async Task DeleteFavoriteByIdAsync_ShouldDeleteFavoriteById()
        {
            // Arrange
            int favoriteId = 1;
            _mockFavoriteRepository.Setup(r => r.DeleteByIdAsync(favoriteId)).Returns(Task.CompletedTask);

            // Act
            await _favoriteService.DeleteFavoriteByIdAsync(favoriteId);

            // Assert
            _mockFavoriteRepository.Verify(r => r.DeleteByIdAsync(favoriteId), Times.Once);
        }

        #endregion

        #region DeleteAllFavoritesByOffers

        [Test]
        public async Task DeleteAllFavoritesByOffers_ShouldDeleteAllFavoritesWithOfferId()
        {
            // Arrange
            int offerId = 1;
            var favorites = new List<Favorite>
            {
                new Favorite { Id = 1, OfferId = offerId },
                new Favorite { Id = 2, OfferId = offerId }
            };

            _mockFavoriteRepository.Setup(r => r.GetAllAsync(It.IsAny<Expression<Func<Favorite, bool>>>())).ReturnsAsync(favorites);
            _mockFavoriteRepository.Setup(r => r.DeleteAsync(It.IsAny<Favorite>())).Returns(Task.CompletedTask);

            // Act
            await _favoriteService.DeleteAllFavoritesByOffers(offerId);

            // Assert
            _mockFavoriteRepository.Verify(r => r.DeleteAsync(It.IsAny<Favorite>()), Times.Exactly(2));
        }

        #endregion

        #region GetAll

        [Test]
        public void GetAll_ShouldReturnAllFavorites()
        {
            // Arrange
            var favorites = new List<Favorite>
            {
                new Favorite { Id = 1, OfferId = 1 },
                new Favorite { Id = 2, OfferId = 1 }
            }.AsQueryable();

            _mockFavoriteRepository.Setup(r => r.GetAll()).Returns(favorites);

            // Act
            var result = _favoriteService.GetAll();

            // Assert
            Assert.AreEqual(2, result.Count());
        }

        #endregion

        #region GetFavoriteByIdAsync

        [Test]
        public async Task GetFavoriteByIdAsync_ShouldReturnFavorite()
        {
            // Arrange
            int favoriteId = 1;
            var favorite = new Favorite { Id = favoriteId, OfferId = 1 };
            _mockFavoriteRepository.Setup(r => r.GetByIdAsync(favoriteId)).ReturnsAsync(favorite);

            // Act
            var result = await _favoriteService.GetFavoriteByIdAsync(favoriteId);

            // Assert
            Assert.AreEqual(favorite, result);
        }

        #endregion

        #region UpdateFavoriteAsync

        [Test]
        public async Task UpdateFavoriteAsync_ShouldUpdateFavorite()
        {
            // Arrange
            var favorite = new Favorite { Id = 1, OfferId = 1 };
            _mockFavoriteRepository.Setup(r => r.UpdateAsync(favorite)).Returns(Task.CompletedTask);

            // Act
            await _favoriteService.UpdateFavoriteAsync(favorite);

            // Assert
            _mockFavoriteRepository.Verify(r => r.UpdateAsync(favorite), Times.Once);
        }

        #endregion

        #region GetAllFavoritesAsync

        [Test]
        public async Task GetAllFavoritesAsync_ShouldReturnFilteredFavorites()
        {
            // Arrange
            var favorites = new List<Favorite>
            {
                new Favorite { Id = 1, OfferId = 1 },
                new Favorite { Id = 2, OfferId = 1 }
            };

            Expression<Func<Favorite, bool>> filter = f => f.OfferId == 1;
            _mockFavoriteRepository.Setup(r => r.GetAllAsync(filter)).ReturnsAsync(favorites);

            // Act
            var result = await _favoriteService.GetAllFavoritesAsync(filter);

            // Assert
            Assert.AreEqual(2, result.Count());
        }

        #endregion

    }
}
