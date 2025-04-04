using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Explorers_Haven.Models;

namespace Explorers_Haven.Tests
{
    [TestFixture]
    public class StayModelTests
    {
        #region Property Assignments

        [Test]
        public void Stay_Properties_ShouldBeSetAndGetCorrectly()
        {
            // Arrange
            var stay = new Stay
            {
                Id = 1,
                Name = "Beach Resort",
                Disc = "A great place to relax",
                Image = "beach_resort.jpg",
                Price = 199.99m,
                Stars = 5,
                UserId = 1
            };

            // Act & Assert
            Assert.AreEqual(1, stay.Id);
            Assert.AreEqual("Beach Resort", stay.Name);
            Assert.AreEqual("A great place to relax", stay.Disc);
            Assert.AreEqual("beach_resort.jpg", stay.Image);
            Assert.AreEqual(199.99m, stay.Price);
            Assert.AreEqual(5, stay.Stars);
            Assert.AreEqual(1, stay.UserId);
        }

        #endregion

        #region Required Properties Validation

        [Test]
        public void Stay_Name_ShouldBeRequired()
        {
            // Arrange
            var stay = new Stay
            {
                Name = null
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(stay);
            var isValid = Validator.TryValidateObject(stay, validationContext, validationResults, true);

            // Assert
            Assert.IsFalse(isValid); // Name is required, so it should fail
            Assert.AreEqual(1, validationResults.Count);
            Assert.AreEqual("The Name field is required.", validationResults[0].ErrorMessage);
        }

        #endregion

        #region Nullable Properties Validation

        [Test]
        public void Stay_Disc_ShouldBeNullable()
        {
            // Arrange
            var stay = new Stay
            {
                Disc = null // Nullable property
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(stay);
            var isValid = Validator.TryValidateObject(stay, validationContext, validationResults, true);

            // Assert
            Assert.IsTrue(isValid); // Disc is nullable, so this should be valid
        }

        [Test]
        public void Stay_Image_ShouldBeNullable()
        {
            // Arrange
            var stay = new Stay
            {
                Image = null // Nullable property
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(stay);
            var isValid = Validator.TryValidateObject(stay, validationContext, validationResults, true);

            // Assert
            Assert.IsTrue(isValid); // Image is nullable, so this should be valid
        }

        #endregion

        #region Navigation Properties

        [Test]
        public void Stay_User_ShouldBeNullable()
        {
            // Arrange
            var stay = new Stay
            {
                User = null // Nullable navigation property
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(stay);
            var isValid = Validator.TryValidateObject(stay, validationContext, validationResults, true);

            // Assert
            Assert.IsTrue(isValid); // User can be null, so this should be valid
        }

        [Test]
        public void Stay_Offers_ShouldBeNullable()
        {
            // Arrange
            var stay = new Stay
            {
                Offers = null // Nullable navigation property
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(stay);
            var isValid = Validator.TryValidateObject(stay, validationContext, validationResults, true);

            // Assert
            Assert.IsTrue(isValid); // Offers can be null, so this should be valid
        }

        [Test]
        public void Stay_StayAmenities_ShouldBeNullable()
        {
            // Arrange
            var stay = new Stay
            {
                StayAmenities = null // Nullable navigation property
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(stay);
            var isValid = Validator.TryValidateObject(stay, validationContext, validationResults, true);

            // Assert
            Assert.IsTrue(isValid); // StayAmenities can be null, so this should be valid
        }

        #endregion

        #region Price Validation

        [Test]
        public void Stay_Price_ShouldBeNullable()
        {
            // Arrange
            var stay = new Stay
            {
                Price = null // Nullable property
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(stay);
            var isValid = Validator.TryValidateObject(stay, validationContext, validationResults, true);

            // Assert
            Assert.IsTrue(isValid); // Price is nullable, so this should be valid
        }

        [Test]
        public void Stay_Price_ShouldNotBeNegative()
        {
            // Arrange
            var stay = new Stay
            {
                Price = -10.00m // Negative price
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(stay);
            var isValid = Validator.TryValidateObject(stay, validationContext, validationResults, true);

            // Assert
            Assert.IsFalse(isValid); // Price cannot be negative
            Assert.AreEqual(1, validationResults.Count);
            Assert.AreEqual("The field Price must be a positive number.", validationResults[0].ErrorMessage);
        }

        #endregion

        #region Stars Validation

        [Test]
        public void Stay_Stars_ShouldBeBetween1And5()
        {
            // Arrange
            var stay = new Stay
            {
                Stars = 6 // Invalid number of stars (greater than 5)
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(stay);
            var isValid = Validator.TryValidateObject(stay, validationContext, validationResults, true);

            // Assert
            Assert.IsFalse(isValid); // Stars should be between 1 and 5
            Assert.AreEqual(1, validationResults.Count);
            Assert.AreEqual("The field Stars must be between 1 and 5.", validationResults[0].ErrorMessage);
        }

        [Test]
        public void Stay_Stars_ShouldBeNullable()
        {
            // Arrange
            var stay = new Stay
            {
                Stars = null // Nullable property
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(stay);
            var isValid = Validator.TryValidateObject(stay, validationContext, validationResults, true);

            // Assert
            Assert.IsTrue(isValid); // Stars is nullable, so this should be valid
        }

        #endregion
    }
}
