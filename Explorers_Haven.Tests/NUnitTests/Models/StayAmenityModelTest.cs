using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Explorers_Haven.Models;

namespace Explorers_Haven.Tests
{
    [TestFixture]
    public class StayAmenityModelTests
    {
        #region Property Assignments

        [Test]
        public void StayAmenity_Properties_ShouldBeSetAndGetCorrectly()
        {
            // Arrange
            var stayAmenity = new StayAmenity
            {
                Id = 1,
                AmenityId = 101,
                StayId = 202
            };

            // Act & Assert
            Assert.AreEqual(1, stayAmenity.Id);
            Assert.AreEqual(101, stayAmenity.AmenityId);
            Assert.AreEqual(202, stayAmenity.StayId);
        }

        #endregion

        #region Nullable Properties Validation

        [Test]
        public void StayAmenity_AmenityId_ShouldBeNullable()
        {
            // Arrange
            var stayAmenity = new StayAmenity
            {
                AmenityId = null
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(stayAmenity);
            var isValid = Validator.TryValidateObject(stayAmenity, validationContext, validationResults, true);

            // Assert
            Assert.IsTrue(isValid); // Should be valid even with 'AmenityId' as null
        }

        [Test]
        public void StayAmenity_StayId_ShouldBeNullable()
        {
            // Arrange
            var stayAmenity = new StayAmenity
            {
                StayId = null
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(stayAmenity);
            var isValid = Validator.TryValidateObject(stayAmenity, validationContext, validationResults, true);

            // Assert
            Assert.IsTrue(isValid); // Should be valid even with 'StayId' as null
        }

        #endregion

        #region Navigation Properties Validation

        [Test]
        public void StayAmenity_Amenity_ShouldBeNullable()
        {
            // Arrange
            var stayAmenity = new StayAmenity
            {
                Amenity = null
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(stayAmenity);
            var isValid = Validator.TryValidateObject(stayAmenity, validationContext, validationResults, true);

            // Assert
            Assert.IsTrue(isValid); // Should be valid even with 'Amenity' as null
        }

        [Test]
        public void StayAmenity_Stay_ShouldBeNullable()
        {
            // Arrange
            var stayAmenity = new StayAmenity
            {
                Stay = null
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(stayAmenity);
            var isValid = Validator.TryValidateObject(stayAmenity, validationContext, validationResults, true);

            // Assert
            Assert.IsTrue(isValid); // Should be valid even with 'Stay' as null
        }

        #endregion

        #region Collection Properties Initialization

        [Test]
        public void StayAmenity_NavigationProperties_ShouldBeNullable()
        {
            // Arrange
            var stayAmenity = new StayAmenity
            {
                Amenity = null,
                Stay = null
            };

            // Act & Assert
            Assert.IsNull(stayAmenity.Amenity); // Should allow 'Amenity' to be null
            Assert.IsNull(stayAmenity.Stay); // Should allow 'Stay' to be null
        }

        #endregion

        #region Required Validation (if any)

        // If you had any required properties in StayAmenity, we could add tests for them.
        // Since StayId, AmenityId, Amenity, and Stay are nullable, no required property is being validated here.

        #endregion
    }
}
