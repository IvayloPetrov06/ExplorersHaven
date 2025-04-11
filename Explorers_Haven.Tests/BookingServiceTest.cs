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
    public class BookingServiceTests
    {
        private Mock<IRepository<Booking>> _mockRepo;
        private BookingService _bookingService;

        [SetUp]
        public void Setup()
        {
            _mockRepo = new Mock<IRepository<Booking>>();
            _bookingService = new BookingService(_mockRepo.Object);
        }

        #region Add Booking

        [Test]
        public async Task AddBookingAsync_ShouldCallRepoAddAsync()
        {
            // Arrange
            var booking = new Booking { Id = 1, OfferId = 1};

            // Act
            await _bookingService.AddBookingAsync(booking);

            // Assert
            _mockRepo.Verify(r => r.AddAsync(It.Is<Booking>(b => b == booking)), Times.Once);
        }

        #endregion

        #region Update Booking

        [Test]
        public async Task UpdateBookingAsync_ShouldCallRepoUpdateAsync()
        {
            // Arrange
            var booking = new Booking { Id = 1, OfferId = 1 };

            // Act
            await _bookingService.UpdateBookingAsync(booking);

            // Assert
            _mockRepo.Verify(r => r.UpdateAsync(It.Is<Booking>(b => b == booking)), Times.Once);
        }

        #endregion

        #region Delete Booking

        [Test]
        public async Task DeleteBookingAsync_ShouldCallRepoDeleteAsync()
        {
            // Arrange
            var booking = new Booking { Id = 1, OfferId = 1};

            // Act
            await _bookingService.DeleteBookingAsync(booking);

            // Assert
            _mockRepo.Verify(r => r.DeleteAsync(It.Is<Booking>(b => b == booking)), Times.Once);
        }

        #endregion

        #region Delete Booking by ID

        [Test]
        public async Task DeleteBookingByIdAsync_ShouldCallRepoDeleteByIdAsync()
        {
            // Arrange
            var bookingId = 1;

            // Act
            await _bookingService.DeleteBookingByIdAsync(bookingId);

            // Assert
            _mockRepo.Verify(r => r.DeleteByIdAsync(It.Is<int>(id => id == bookingId)), Times.Once);
        }

        #endregion

        #region Delete All Bookings by Offer ID

        [Test]
        public async Task DeleteAllBookingsByOffers_ShouldDeleteAllBookingsWithOfferId()
        {
            // Arrange
            var offerId = 1;
            var bookings = new List<Booking>
            {
                new Booking { Id = 1, OfferId = offerId},
                new Booking { Id = 2, OfferId = offerId }
            };

            _mockRepo.Setup(r => r.GetAllAsync(It.IsAny<Expression<Func<Booking, bool>>>()))
                .ReturnsAsync(bookings);

            // Act
            await _bookingService.DeleteAllBookingsByOffers(offerId);

            // Assert
            _mockRepo.Verify(r => r.DeleteAsync(It.IsAny<Booking>()), Times.Exactly(2));
        }

        #endregion

        #region Get All Bookings

        [Test]
        public void GetAll_ShouldReturnAllBookings()
        {
            // Arrange
            var bookings = new List<Booking>
            {
                new Booking { Id = 1 },
                new Booking { Id = 2 }
            }.AsQueryable();

            _mockRepo.Setup(r => r.GetAll()).Returns(bookings);

            // Act
            var result = _bookingService.GetAll();

            // Assert
            Assert.AreEqual(2, result.Count());
        }

        #endregion

        #region Get All Bookings Async

        [Test]
        public async Task GetAllBookingsAsync_ShouldReturnAllBookingsAsync()
        {
            // Arrange
            var bookings = new List<Booking>
            {
                new Booking { Id = 1 },
                new Booking { Id = 2}
            };

            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(bookings);

            // Act
            var result = await _bookingService.GetAllBookingsAsync();

            // Assert
            Assert.AreEqual(2, result.Count());
        }

        #endregion

        #region Get Booking by ID

        [Test]
        public async Task GetBookingByIdAsync_ShouldReturnBooking()
        {
            // Arrange
            var bookingId = 1;
            var booking = new Booking { Id = bookingId};
            _mockRepo.Setup(r => r.GetByIdAsync(bookingId)).ReturnsAsync(booking);

            // Act
            var result = await _bookingService.GetBookingByIdAsync(bookingId);

            // Assert
            Assert.AreEqual(booking, result);
            _mockRepo.Verify(r => r.GetByIdAsync(It.Is<int>(id => id == bookingId)), Times.Once);
        }

        #endregion

        #region Get Booking Async with Filter

        [Test]
        public async Task GetBookingAsync_ShouldReturnBookingWithFilter()
        {
            // Arrange
            var booking = new Booking { Id = 1 };
            _mockRepo.Setup(r => r.GetAsync(It.IsAny<Expression<Func<Booking, bool>>>()))
                .ReturnsAsync(booking);

            // Act
            var result = await _bookingService.GetBookingAsync(b => b.Id == 1);

            // Assert
            Assert.AreEqual(booking, result);
        }

        #endregion

        #region All With Include

        [Test]
        public void AllWithInclude_ShouldIncludeProperties()
        {
            // Arrange
            var bookings = new List<Booking>
            {
                new Booking { Id = 1 },
                new Booking { Id = 2 }
            }.AsQueryable();

            _mockRepo.Setup(r => r.GetAllQuery()).Returns(bookings);

            // Act
            var result = _bookingService.AllWithInclude(b => b.Id);

            // Assert
            Assert.AreEqual(2, result.Count());
        }

        #endregion
    }
}
