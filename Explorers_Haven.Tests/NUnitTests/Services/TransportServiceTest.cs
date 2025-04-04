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
    public class TransportServiceTests
    {
        private Mock<IRepository<Transport>> _mockTransportRepository;
        private TransportService _transportService;

        [SetUp]
        public void SetUp()
        {
            // Mock the repository
            _mockTransportRepository = new Mock<IRepository<Transport>>();

            // Initialize the service with the mocked repository
            _transportService = new TransportService(_mockTransportRepository.Object);
        }

        #region AddTransportAsync

        [Test]
        public async Task AddTransportAsync_ShouldAddTransport()
        {
            // Arrange
            var transport = new Transport { Id = 1, Name = "Test Transport" };
            _mockTransportRepository.Setup(r => r.AddAsync(transport)).Returns(Task.CompletedTask);

            // Act
            await _transportService.AddTransportAsync(transport);

            // Assert
            _mockTransportRepository.Verify(r => r.AddAsync(transport), Times.Once);
        }

        #endregion

        #region UpdateTransportAsync

        [Test]
        public async Task UpdateTransportAsync_ShouldUpdateTransport()
        {
            // Arrange
            var transport = new Transport { Id = 1, Name = "Updated Transport" };
            _mockTransportRepository.Setup(r => r.UpdateAsync(transport)).Returns(Task.CompletedTask);

            // Act
            await _transportService.UpdateTransportAsync(transport);

            // Assert
            _mockTransportRepository.Verify(r => r.UpdateAsync(transport), Times.Once);
        }

        #endregion

        #region DeleteTransportAsync

        [Test]
        public async Task DeleteTransportAsync_ShouldDeleteTransport()
        {
            // Arrange
            var transport = new Transport { Id = 1, Name = "Test Transport" };
            _mockTransportRepository.Setup(r => r.DeleteAsync(transport)).Returns(Task.CompletedTask);

            // Act
            await _transportService.DeleteTransportAsync(transport);

            // Assert
            _mockTransportRepository.Verify(r => r.DeleteAsync(transport), Times.Once);
        }

        #endregion

        #region DeleteTransportByIdAsync

        [Test]
        public async Task DeleteTransportByIdAsync_ShouldDeleteTransportById()
        {
            // Arrange
            int transportId = 1;
            _mockTransportRepository.Setup(r => r.DeleteByIdAsync(transportId)).Returns(Task.CompletedTask);

            // Act
            await _transportService.DeleteTransportByIdAsync(transportId);

            // Assert
            _mockTransportRepository.Verify(r => r.DeleteByIdAsync(transportId), Times.Once);
        }

        #endregion

        #region GetAll

        [Test]
        public void GetAll_ShouldReturnAllTransports()
        {
            // Arrange
            var transports = new List<Transport>
            {
                new Transport { Id = 1, Name = "Transport 1" },
                new Transport { Id = 2, Name = "Transport 2" }
            }.AsQueryable();

            _mockTransportRepository.Setup(r => r.GetAll()).Returns(transports);

            // Act
            var result = _transportService.GetAll();

            // Assert
            Assert.AreEqual(2, result.Count());
        }

        #endregion

        #region CombinedInclude

        [Test]
        public void CombinedInclude_ShouldIncludeRelatedEntities()
        {
            // Arrange
            var transports = new List<Transport>
            {
                new Transport { Id = 1, Name = "Transport 1" },
                new Transport { Id = 2, Name = "Transport 2" }
            }.AsQueryable();

            _mockTransportRepository.Setup(r => r.GetAllQuery()).Returns(transports);

            // Act
            var result = _transportService.AllWithInclude(t => t.Name);

            // Assert
            Assert.AreEqual(2, result.Count());
        }

        #endregion

        #region GetTransportByIdAsync

        [Test]
        public async Task GetTransportByIdAsync_ShouldReturnTransport()
        {
            // Arrange
            int transportId = 1;
            var transport = new Transport { Id = transportId, Name = "Test Transport" };
            _mockTransportRepository.Setup(r => r.GetByIdAsync(transportId)).ReturnsAsync(transport);

            // Act
            var result = await _transportService.GetTransportByIdAsync(transportId);

            // Assert
            Assert.AreEqual(transport, result);
        }

        #endregion

        #region GetTransportAsync with Filter

        [Test]
        public async Task GetTransportAsync_WithFilter_ShouldReturnFilteredTransport()
        {
            // Arrange
            var filter = Expression.Lambda<Func<Transport, bool>>(
                Expression.Call(
                    Expression.Property(Expression.Parameter(typeof(Transport), "t"), "Name"),
                    typeof(string).GetMethod("Contains", new[] { typeof(string) }),
                    Expression.Constant("Test")
                ),
                Expression.Parameter(typeof(Transport), "t")
            );

            var transports = new List<Transport>
            {
                new Transport { Id = 1, Name = "Test Transport 1" },
                new Transport { Id = 2, Name = "Test Transport 2" }
            };

            _mockTransportRepository.Setup(r => r.GetAsync(filter)).ReturnsAsync(transports.First());

            // Act
            var result = await _transportService.GetTransportAsync(filter);

            // Assert
            Assert.AreEqual(transports.First(), result);
        }

        #endregion

        #region GetAllTransportAsync with Filter

        [Test]
        public async Task GetAllTransportAsync_WithFilter_ShouldReturnFilteredTransports()
        {
            // Arrange
            var filter = Expression.Lambda<Func<Transport, bool>>(
                Expression.Call(
                    Expression.Property(Expression.Parameter(typeof(Transport), "t"), "Name"),
                    typeof(string).GetMethod("Contains", new[] { typeof(string) }),
                    Expression.Constant("Test")
                ),
                Expression.Parameter(typeof(Transport), "t")
            );
            var transports = new List<Transport>
            {
                new Transport { Id = 1, Name = "Transport 1" },
                new Transport { Id = 2, Name = "Transport 2" }
            };

            _mockTransportRepository.Setup(r => r.GetAllAsync(filter)).ReturnsAsync(transports);

            // Act
            var result = await _transportService.GetAllTransportsAsync(filter);

            // Assert
            Assert.AreEqual(2, result.Count());
        }

        #endregion

        #region GetAllTransportAsync without Filter

        [Test]
        public async Task GetAllTransportAsync_WithoutFilter_ShouldReturnAllTransports()
        {
            // Arrange
            var transports = new List<Transport>
            {
                new Transport { Id = 1, Name = "Transport 1" },
                new Transport { Id = 2, Name = "Transport 2" }
            };

            _mockTransportRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(transports);

            // Act
            var result = await _transportService.GetAllTransportsAsync();

            // Assert
            Assert.AreEqual(2, result.Count());
        }

        #endregion
    }
}
