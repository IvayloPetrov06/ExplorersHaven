using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Explorers_Haven.Models;

namespace Explorers_Haven.Tests
{
    [TestFixture]
    public class TransportModelTests
    {
        #region Property Assignments

        [Test]
        public void Transport_Properties_ShouldBeSetAndGetCorrectly()
        {
            // Arrange
            var transport = new Transport
            {
                Id = 1,
                Name = "Bus"
            };

            // Act & Assert
            Assert.AreEqual(1, transport.Id);
            Assert.AreEqual("Bus", transport.Name);
        }

        #endregion

        #region Nullable Properties Validation

        [Test]
        public void Transport_Name_ShouldBeNullable()
        {
            // Arrange
            var transport = new Transport
            {
                Name = null
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(transport);
            var isValid = Validator.TryValidateObject(transport, validationContext, validationResults, true);

            // Assert
            Assert.IsTrue(isValid); // Should be valid even with 'Name' as null
        }

        [Test]
        public void Transport_Travels_ShouldBeNullable()
        {
            // Arrange
            var transport = new Transport
            {
                Travels = null
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(transport);
            var isValid = Validator.TryValidateObject(transport, validationContext, validationResults, true);

            // Assert
            Assert.IsTrue(isValid); // Should be valid even with 'Travels' as null
        }

        #endregion

        #region Collection Property Initialization

        [Test]
        public void Transport_Travels_ShouldBeInitialized()
        {
            // Arrange
            var transport = new Transport();

            // Act & Assert
            Assert.IsNotNull(transport.Travels); // Travels should be initialized to an empty collection
        }

        #endregion

        #region Required Validation (if any)

        // If the 'Name' property had a Required attribute or other constraints, you could add tests for them.
        // Since 'Name' is nullable here, no validation is needed in this case.

        #endregion
    }
}
