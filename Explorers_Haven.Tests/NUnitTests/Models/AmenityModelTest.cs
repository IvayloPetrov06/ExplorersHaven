using NUnit.Framework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Explorers_Haven.Models;

namespace Explorers_Haven.Tests
{
    [TestFixture]
    public class AmenityModelTests
    {
        #region Required Property Validation

        [Test]
        public void Amenity_Name_ShouldBeRequired()
        {
            // Arrange
            var amenity = new Amenity();

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(amenity);
            var isValid = Validator.TryValidateObject(amenity, validationContext, validationResults, true);

            // Assert
            Assert.IsFalse(isValid); // Should fail because 'Name' is required but not set
            Assert.AreEqual(1, validationResults.Count);
            Assert.AreEqual("The Name field is required.", validationResults[0].ErrorMessage);
        }

        #endregion

        #region Property Assignments

        [Test]
        public void Amenity_Properties_ShouldBeSetAndGetCorrectly()
        {
            // Arrange
            var amenity = new Amenity
            {
                Id = 1,
                Name = "Wi-Fi",
                Icon = "wifi-icon.png"
            };

            // Act & Assert
            Assert.AreEqual(1, amenity.Id);
            Assert.AreEqual("Wi-Fi", amenity.Name);
            Assert.AreEqual("wifi-icon.png", amenity.Icon);
        }

        #endregion

        #region Nullable Property Validation

        [Test]
        public void Amenity_Icon_ShouldBeNullable()
        {
            // Arrange
            var amenity = new Amenity
            {
                Name = "Pool", // Required field
                Icon = null // Nullable field
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(amenity);
            var isValid = Validator.TryValidateObject(amenity, validationContext, validationResults, true);

            // Assert
            Assert.IsTrue(isValid); // Should be valid even if 'Icon' is null
        }

        #endregion

        #region Collection Property StayAmenities

        [Test]
        public void Amenity_StayAmenities_ShouldInitializeToEmptyList()
        {
            // Arrange
            var amenity = new Amenity
            {
                Name = "Gym"
            };

            // Act & Assert
            Assert.IsNotNull(amenity.StayAmenities); // Should not be null
            Assert.IsEmpty(amenity.StayAmenities); // Should be an empty list by default
        }

        [Test]
        public void Amenity_StayAmenities_ShouldAllowAddingItems()
        {
            // Arrange
            var amenity = new Amenity
            {
                Name = "Pool"
            };
            var stayAmenity = new StayAmenity { Id = 1, AmenityId = amenity.Id };

            // Act
            amenity.StayAmenities.Add(stayAmenity);

            // Assert
            Assert.AreEqual(1, amenity.StayAmenities.Count); // Should have one item in the collection
            Assert.AreEqual(stayAmenity, amenity.StayAmenities.First()); // The first item should be the stayAmenity we added
        }

        #endregion
    }
}
