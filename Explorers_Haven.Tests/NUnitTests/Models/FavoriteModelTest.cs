using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Explorers_Haven.Models;

namespace Explorers_Haven.Tests
{
    [TestFixture]
    public class FavoriteModelTests
    {
        #region Property Assignments

        [Test]
        public void Favorite_Properties_ShouldBeSetAndGetCorrectly()
        {
            // Arrange
            var favorite = new Favorite
            {
                Id = 1,
                UserId = 101,
                OfferName = "Amazing offer!",
                OfferId = 202
            };

            // Act & Assert
            Assert.AreEqual(1, favorite.Id);
            Assert.AreEqual(101, favorite.UserId);
            Assert.AreEqual("Amazing offer!", favorite.OfferName);
            Assert.AreEqual(202, favorite.OfferId);
        }

        #endregion

        #region Nullable Properties Validation

        [Test]
        public void Favorite_UserId_ShouldBeNullable()
        {
            // Arrange
            var favorite = new Favorite
            {
                Id = 1,
                OfferName = "Great Offer",
                OfferId = 202,
                UserId = null // Nullable property
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(favorite);
            var isValid = Validator.TryValidateObject(favorite, validationContext, validationResults, true);

            // Assert
            Assert.IsTrue(isValid); // Should be valid even if 'UserId' is null
        }

        [Test]
        public void Favorite_OfferId_ShouldBeNullable()
        {
            // Arrange
            var favorite = new Favorite
            {
                Id = 1,
                OfferName = "Great Offer",
                OfferId = null // Nullable property
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(favorite);
            var isValid = Validator.TryValidateObject(favorite, validationContext, validationResults, true);

            // Assert
            Assert.IsTrue(isValid); // Should be valid even if 'OfferId' is null
        }

        #endregion

        #region Validation of Invalid Data

        [Test]
        public void Favorite_WithNullUserId_ShouldPassValidation()
        {
            // Arrange
            var favorite = new Favorite
            {
                OfferName = "Amazing Deal",
                OfferId = 102,
                UserId = null // Nullable property
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(favorite);
            var isValid = Validator.TryValidateObject(favorite, validationContext, validationResults, true);

            // Assert
            Assert.IsTrue(isValid); // Should be valid even with UserId as null
        }

        [Test]
        public void Favorite_WithOfferName_ShouldPassValidation()
        {
            // Arrange
            var favorite = new Favorite
            {
                OfferName = "Great Adventure!",
                OfferId = 203
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(favorite);
            var isValid = Validator.TryValidateObject(favorite, validationContext, validationResults, true);

            // Assert
            Assert.IsTrue(isValid); // Should be valid if 'OfferName' is provided
        }

        [Test]
        public void Favorite_WithOfferNameLongerThanMaxLength_ShouldFailValidation()
        {
            // Arrange
            var favorite = new Favorite
            {
                OfferName = new string('a', 101), // Exceeds max length (assumed max length > 100)
                OfferId = 203
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(favorite);
            var isValid = Validator.TryValidateObject(favorite, validationContext, validationResults, true);

            // Assert
            Assert.IsFalse(isValid); // Should fail if OfferName exceeds max length
            Assert.AreEqual(1, validationResults.Count);
            Assert.AreEqual("The field OfferName must be a string with a maximum length of 100.", validationResults[0].ErrorMessage);
        }

        #endregion
    }
}
