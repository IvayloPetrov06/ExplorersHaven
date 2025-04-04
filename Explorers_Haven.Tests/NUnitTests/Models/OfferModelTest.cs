using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Explorers_Haven.Models;

namespace Explorers_Haven.Tests
{
    [TestFixture]
    public class OfferModelTests
    {
        #region Property Assignments

        [Test]
        public void Offer_Properties_ShouldBeSetAndGetCorrectly()
        {
            // Arrange
            var offer = new Offer
            {
                Id = 1,
                Name = "Special Offer",
                MaxPeople = 10,
                Discount = 20.5m,
                DurationDays = 7,
                StartDate = new DateOnly(2023, 1, 1),
                LastDate = new DateOnly(2023, 1, 8),
                Disc = "Limited time discount",
                CoverImage = "cover.jpg",
                BackImage = "back.jpg",
                Price = 100.00m,
                Rating = 4.5m,
                RealRating = 4.7m,
                Clicks = 150,
                UserId = 101,
                StayId = 202
            };

            // Act & Assert
            Assert.AreEqual(1, offer.Id);
            Assert.AreEqual("Special Offer", offer.Name);
            Assert.AreEqual(10, offer.MaxPeople);
            Assert.AreEqual(20.5m, offer.Discount);
            Assert.AreEqual(7, offer.DurationDays);
            Assert.AreEqual(new DateOnly(2023, 1, 1), offer.StartDate);
            Assert.AreEqual(new DateOnly(2023, 1, 8), offer.LastDate);
            Assert.AreEqual("Limited time discount", offer.Disc);
            Assert.AreEqual("cover.jpg", offer.CoverImage);
            Assert.AreEqual("back.jpg", offer.BackImage);
            Assert.AreEqual(100.00m, offer.Price);
            Assert.AreEqual(4.5m, offer.Rating);
            Assert.AreEqual(4.7m, offer.RealRating);
            Assert.AreEqual(150, offer.Clicks);
            Assert.AreEqual(101, offer.UserId);
            Assert.AreEqual(202, offer.StayId);
        }

        #endregion

        #region Nullable Properties Validation

        [Test]
        public void Offer_MaxPeople_ShouldBeNullable()
        {
            // Arrange
            var offer = new Offer
            {
                Name = "Special Offer",
                MaxPeople = null
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(offer);
            var isValid = Validator.TryValidateObject(offer, validationContext, validationResults, true);

            // Assert
            Assert.IsTrue(isValid); // Should be valid even with 'MaxPeople' as null
        }

        [Test]
        public void Offer_Discount_ShouldBeNullable()
        {
            // Arrange
            var offer = new Offer
            {
                Name = "Special Offer",
                Discount = null
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(offer);
            var isValid = Validator.TryValidateObject(offer, validationContext, validationResults, true);

            // Assert
            Assert.IsTrue(isValid); // Should be valid even with 'Discount' as null
        }

        [Test]
        public void Offer_DurationDays_ShouldBeNullable()
        {
            // Arrange
            var offer = new Offer
            {
                Name = "Special Offer",
                DurationDays = null
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(offer);
            var isValid = Validator.TryValidateObject(offer, validationContext, validationResults, true);

            // Assert
            Assert.IsTrue(isValid); // Should be valid even with 'DurationDays' as null
        }

        [Test]
        public void Offer_StartDate_ShouldBeNullable()
        {
            // Arrange
            var offer = new Offer
            {
                Name = "Special Offer",
                StartDate = null
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(offer);
            var isValid = Validator.TryValidateObject(offer, validationContext, validationResults, true);

            // Assert
            Assert.IsTrue(isValid); // Should be valid even with 'StartDate' as null
        }

        [Test]
        public void Offer_LastDate_ShouldBeNullable()
        {
            // Arrange
            var offer = new Offer
            {
                Name = "Special Offer",
                LastDate = null
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(offer);
            var isValid = Validator.TryValidateObject(offer, validationContext, validationResults, true);

            // Assert
            Assert.IsTrue(isValid); // Should be valid even with 'LastDate' as null
        }

        #endregion

        #region Required Properties Validation

        [Test]
        public void Offer_Name_ShouldBeRequired()
        {
            // Arrange
            var offer = new Offer
            {
                Name = null
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(offer);
            var isValid = Validator.TryValidateObject(offer, validationContext, validationResults, true);

            // Assert
            Assert.IsFalse(isValid); // 'Name' is required, should fail validation
            Assert.AreEqual(1, validationResults.Count);
            Assert.AreEqual("The Name field is required.", validationResults[0].ErrorMessage);
        }

        [Test]
        public void Offer_Name_ShouldBeValidWhenSet()
        {
            // Arrange
            var offer = new Offer
            {
                Name = "Valid Offer"
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(offer);
            var isValid = Validator.TryValidateObject(offer, validationContext, validationResults, true);

            // Assert
            Assert.IsTrue(isValid); // Name is set, should pass validation
        }

        #endregion

        #region Collection Properties Validation

        [Test]
        public void Offer_Activities_ShouldBeInitialized()
        {
            // Arrange
            var offer = new Offer();

            // Act & Assert
            Assert.IsNotNull(offer.Activities); // Should be initialized as a new list
        }

        [Test]
        public void Offer_Bookings_ShouldBeInitialized()
        {
            // Arrange
            var offer = new Offer();

            // Act & Assert
            Assert.IsNotNull(offer.Bookings); // Should be initialized as a new list
        }

        [Test]
        public void Offer_Ratings_ShouldBeInitialized()
        {
            // Arrange
            var offer = new Offer();

            // Act & Assert
            Assert.IsNotNull(offer.Ratings); // Should be initialized as a new list
        }

        [Test]
        public void Offer_Comments_ShouldBeInitialized()
        {
            // Arrange
            var offer = new Offer();

            // Act & Assert
            Assert.IsNotNull(offer.Comments); // Should be initialized as a new list
        }

        [Test]
        public void Offer_Favorites_ShouldBeInitialized()
        {
            // Arrange
            var offer = new Offer();

            // Act & Assert
            Assert.IsNotNull(offer.Favorites); // Should be initialized as a new list
        }

        #endregion
    }
}
