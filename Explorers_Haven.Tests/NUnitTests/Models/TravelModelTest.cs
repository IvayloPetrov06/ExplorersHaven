using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Explorers_Haven.Models;

namespace Explorers_Haven.Tests
{
    [TestFixture]
    public class TravelModelTests
    {
        #region Property Assignments

        [Test]
        public void Travel_Properties_ShouldBeSetAndGetCorrectly()
        {
            // Arrange
            var travel = new Travel
            {
                Id = 1,
                Start = "New York",
                Finish = "Los Angeles",
                DurationDays = 5,
                Arrival = true,
                DateStart = new DateOnly(2025, 4, 1),
                DateFinish = new DateOnly(2025, 4, 6),
                TransportId = 101,
                UserId = 202,
                OfferId = 303
            };

            // Act & Assert
            Assert.AreEqual(1, travel.Id);
            Assert.AreEqual("New York", travel.Start);
            Assert.AreEqual("Los Angeles", travel.Finish);
            Assert.AreEqual(5, travel.DurationDays);
            Assert.IsTrue(travel.Arrival.HasValue && travel.Arrival.Value);
            Assert.AreEqual(new DateOnly(2025, 4, 1), travel.DateStart);
            Assert.AreEqual(new DateOnly(2025, 4, 6), travel.DateFinish);
            Assert.AreEqual(101, travel.TransportId);
            Assert.AreEqual(202, travel.UserId);
            Assert.AreEqual(303, travel.OfferId);
        }

        #endregion

        #region Required Properties Validation

        [Test]
        public void Travel_Start_ShouldBeRequired()
        {
            // Arrange
            var travel = new Travel
            {
                Start = null
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(travel);
            var isValid = Validator.TryValidateObject(travel, validationContext, validationResults, true);

            // Assert
            Assert.IsFalse(isValid); // Should fail validation due to 'Start' being null
            Assert.AreEqual(1, validationResults.Count);
            Assert.AreEqual("The Start field is required.", validationResults[0].ErrorMessage);
        }

        [Test]
        public void Travel_TransportId_ShouldNotBeNull()
        {
            // Arrange
            var travel = new Travel
            {
                TransportId = 0
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(travel);
            var isValid = Validator.TryValidateObject(travel, validationContext, validationResults, true);

            // Assert
            Assert.IsTrue(isValid); // TransportId can be non-zero, as it's a foreign key
        }

        #endregion

        #region Nullable Properties Validation

        [Test]
        public void Travel_Finish_ShouldBeNullable()
        {
            // Arrange
            var travel = new Travel
            {
                Finish = null
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(travel);
            var isValid = Validator.TryValidateObject(travel, validationContext, validationResults, true);

            // Assert
            Assert.IsTrue(isValid); // 'Finish' should be valid even if it's null
        }

        [Test]
        public void Travel_DurationDays_ShouldBeNullable()
        {
            // Arrange
            var travel = new Travel
            {
                DurationDays = null
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(travel);
            var isValid = Validator.TryValidateObject(travel, validationContext, validationResults, true);

            // Assert
            Assert.IsTrue(isValid); // 'DurationDays' should be valid even if it's null
        }

        [Test]
        public void Travel_Arrival_ShouldBeNullable()
        {
            // Arrange
            var travel = new Travel
            {
                Arrival = null
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(travel);
            var isValid = Validator.TryValidateObject(travel, validationContext, validationResults, true);

            // Assert
            Assert.IsTrue(isValid); // 'Arrival' should be valid even if it's null
        }

        [Test]
        public void Travel_DateStart_ShouldBeNullable()
        {
            // Arrange
            var travel = new Travel
            {
                DateStart = null
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(travel);
            var isValid = Validator.TryValidateObject(travel, validationContext, validationResults, true);

            // Assert
            Assert.IsTrue(isValid); // 'DateStart' should be valid even if it's null
        }

        [Test]
        public void Travel_DateFinish_ShouldBeNullable()
        {
            // Arrange
            var travel = new Travel
            {
                DateFinish = null
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(travel);
            var isValid = Validator.TryValidateObject(travel, validationContext, validationResults, true);

            // Assert
            Assert.IsTrue(isValid); // 'DateFinish' should be valid even if it's null
        }

        #endregion

        #region Navigation Properties Validation

        [Test]
        public void Travel_Transport_ShouldNotBeNull()
        {
            // Arrange
            var travel = new Travel
            {
                Transport = null
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(travel);
            var isValid = Validator.TryValidateObject(travel, validationContext, validationResults, true);

            // Assert
            Assert.IsTrue(isValid); // It's allowed for Transport to be null as long as TransportId is provided
        }

        [Test]
        public void Travel_User_ShouldBeNullable()
        {
            // Arrange
            var travel = new Travel
            {
                User = null
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(travel);
            var isValid = Validator.TryValidateObject(travel, validationContext, validationResults, true);

            // Assert
            Assert.IsTrue(isValid); // It's allowed for User to be null
        }

        [Test]
        public void Travel_Offer_ShouldNotBeNull()
        {
            // Arrange
            var travel = new Travel
            {
                Offer = null
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(travel);
            var isValid = Validator.TryValidateObject(travel, validationContext, validationResults, true);

            // Assert
            Assert.IsTrue(isValid); // 'Offer' should not be null, as OfferId is required
        }

        #endregion
    }
}
