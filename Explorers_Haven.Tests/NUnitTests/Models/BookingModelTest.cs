using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Explorers_Haven.Models;

namespace Explorers_Haven.Tests
{
    [TestFixture]
    public class BookingModelTests
    {
        #region Property Assignments

        [Test]
        public void Booking_Properties_ShouldBeSetAndGetCorrectly()
        {
            // Arrange
            var booking = new Booking
            {
                Id = 1,
                PeopleCount = 3,
                YoungOldPeopleCount = 1,
                StartDate = new DateOnly(2025, 4, 10),
                Price = 150.5m,
                OfferName = "Adventure Package",
                DurationDays = 7,
                UserId = 101,
                OfferId = 202
            };

            // Act & Assert
            Assert.AreEqual(1, booking.Id);
            Assert.AreEqual(3, booking.PeopleCount);
            Assert.AreEqual(1, booking.YoungOldPeopleCount);
            Assert.AreEqual(new DateOnly(2025, 4, 10), booking.StartDate);
            Assert.AreEqual(150.5m, booking.Price);
            Assert.AreEqual("Adventure Package", booking.OfferName);
            Assert.AreEqual(7, booking.DurationDays);
            Assert.AreEqual(101, booking.UserId);
            Assert.AreEqual(202, booking.OfferId);
        }

        #endregion

        #region Nullable Property Validation

        [Test]
        public void Booking_PeopleCount_ShouldBeNullable()
        {
            // Arrange
            var booking = new Booking
            {
                OfferName = "Vacation Package",
                PeopleCount = null // Nullable property
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(booking);
            var isValid = Validator.TryValidateObject(booking, validationContext, validationResults, true);

            // Assert
            Assert.IsTrue(isValid); // Should be valid even if 'PeopleCount' is null
        }

        [Test]
        public void Booking_YoungOldPeopleCount_ShouldBeNullable()
        {
            // Arrange
            var booking = new Booking
            {
                OfferName = "Family Trip",
                YoungOldPeopleCount = null // Nullable property
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(booking);
            var isValid = Validator.TryValidateObject(booking, validationContext, validationResults, true);

            // Assert
            Assert.IsTrue(isValid); // Should be valid even if 'YoungOldPeopleCount' is null
        }

        [Test]
        public void Booking_StartDate_ShouldBeNullable()
        {
            // Arrange
            var booking = new Booking
            {
                OfferName = "Holiday Trip",
                StartDate = null // Nullable property
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(booking);
            var isValid = Validator.TryValidateObject(booking, validationContext, validationResults, true);

            // Assert
            Assert.IsTrue(isValid); // Should be valid even if 'StartDate' is null
        }

        [Test]
        public void Booking_Price_ShouldBeNullable()
        {
            // Arrange
            var booking = new Booking
            {
                OfferName = "Luxury Stay",
                Price = null // Nullable property
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(booking);
            var isValid = Validator.TryValidateObject(booking, validationContext, validationResults, true);

            // Assert
            Assert.IsTrue(isValid); // Should be valid even if 'Price' is null
        }

        [Test]
        public void Booking_OfferName_ShouldBeNullable()
        {
            // Arrange
            var booking = new Booking
            {
                OfferName = null // Nullable property
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(booking);
            var isValid = Validator.TryValidateObject(booking, validationContext, validationResults, true);

            // Assert
            Assert.IsTrue(isValid); // Should be valid even if 'OfferName' is null
        }

        [Test]
        public void Booking_DurationDays_ShouldBeNullable()
        {
            // Arrange
            var booking = new Booking
            {
                OfferName = "Weekend Getaway",
                DurationDays = null // Nullable property
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(booking);
            var isValid = Validator.TryValidateObject(booking, validationContext, validationResults, true);

            // Assert
            Assert.IsTrue(isValid); // Should be valid even if 'DurationDays' is null
        }

        [Test]
        public void Booking_UserId_ShouldBeNullable()
        {
            // Arrange
            var booking = new Booking
            {
                OfferName = "Adventure Trip",
                UserId = null // Nullable property
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(booking);
            var isValid = Validator.TryValidateObject(booking, validationContext, validationResults, true);

            // Assert
            Assert.IsTrue(isValid); // Should be valid even if 'UserId' is null
        }

        [Test]
        public void Booking_OfferId_ShouldBeNullable()
        {
            // Arrange
            var booking = new Booking
            {
                OfferName = "Beach Holiday",
                OfferId = null // Nullable property
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(booking);
            var isValid = Validator.TryValidateObject(booking, validationContext, validationResults, true);

            // Assert
            Assert.IsTrue(isValid); // Should be valid even if 'OfferId' is null
        }

        #endregion

        #region Validation of Invalid Data

        [Test]
        public void Booking_WithInvalidPeopleCount_ShouldFailValidation()
        {
            // Arrange
            var booking = new Booking
            {
                OfferName = "Invalid Trip",
                PeopleCount = -1 // Invalid value, should fail validation
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(booking);
            var isValid = Validator.TryValidateObject(booking, validationContext, validationResults, true);

            // Assert
            Assert.IsFalse(isValid); // Should fail validation because 'PeopleCount' is negative
            Assert.AreEqual(1, validationResults.Count);
            Assert.AreEqual("The field PeopleCount must be between 0 and 100.", validationResults[0].ErrorMessage); // Example error message
        }

        #endregion
    }
}
