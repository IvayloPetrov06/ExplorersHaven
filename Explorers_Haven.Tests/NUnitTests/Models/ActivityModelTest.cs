using Moq;
using NUnit.Framework;
using System.ComponentModel.DataAnnotations;
using Explorers_Haven.Models;

namespace Explorers_Haven.Tests
{
    [TestFixture]
    public class ActivityModelTests
    {
        #region Required Property Validation

        [Test]
        public void Activity_Name_ShouldBeRequired()
        {
            // Arrange
            var activity = new Activity();

            // Act
            var validationResults = new System.Collections.Generic.List<ValidationResult>();
            var validationContext = new ValidationContext(activity);
            var isValid = Validator.TryValidateObject(activity, validationContext, validationResults, true);

            // Assert
            Assert.IsFalse(isValid); // Should fail because 'Name' is required but not set
            Assert.AreEqual(1, validationResults.Count);
            Assert.AreEqual("The Name field is required.", validationResults[0].ErrorMessage);
        }

        #endregion

        #region Property Assignments

        [Test]
        public void Activity_Properties_ShouldBeSetAndGetCorrectly()
        {
            // Arrange
            var activity = new Activity
            {
                Id = 1,
                Name = "Test Activity",
                CoverImage = "test.jpg",
                UserId = 101,
                OfferId = 202
            };

            // Act & Assert
            Assert.AreEqual(1, activity.Id);
            Assert.AreEqual("Test Activity", activity.Name);
            Assert.AreEqual("test.jpg", activity.CoverImage);
            Assert.AreEqual(101, activity.UserId);
            Assert.AreEqual(202, activity.OfferId);
        }

        #endregion

        #region Nullable Property Validation

        [Test]
        public void Activity_CoverImage_ShouldBeNullable()
        {
            // Arrange
            var activity = new Activity
            {
                Name = "Test Activity", // Required field
                CoverImage = null // Nullable field
            };

            // Act
            var validationResults = new System.Collections.Generic.List<ValidationResult>();
            var validationContext = new ValidationContext(activity);
            var isValid = Validator.TryValidateObject(activity, validationContext, validationResults, true);

            // Assert
            Assert.IsTrue(isValid); // Should be valid even if 'CoverImage' is null
        }

        [Test]
        public void Activity_UserId_ShouldBeNullable()
        {
            // Arrange
            var activity = new Activity
            {
                Name = "Test Activity", // Required field
                UserId = null // Nullable field
            };

            // Act
            var validationResults = new System.Collections.Generic.List<ValidationResult>();
            var validationContext = new ValidationContext(activity);
            var isValid = Validator.TryValidateObject(activity, validationContext, validationResults, true);

            // Assert
            Assert.IsTrue(isValid); // Should be valid even if 'UserId' is null
        }

        #endregion

        #region Relationship with Offer

        [Test]
        public void Activity_ShouldBeAbleToAssignOffer()
        {
            // Arrange
            var mockOffer = new Mock<Offer>();
            mockOffer.Setup(o => o.Id).Returns(1001);
            mockOffer.Setup(o => o.Name).Returns("Adventure Offer");

            var activity = new Activity
            {
                Name = "Test Activity",
                Offer = mockOffer.Object,
                OfferId = mockOffer.Object.Id
            };

            // Act & Assert
            Assert.AreEqual(1001, activity.Offer.Id);
            Assert.AreEqual("Adventure Offer", activity.Offer.Name);
            Assert.AreEqual(1001, activity.OfferId);
        }

        #endregion

        #region Relationship with User

        [Test]
        public void Activity_ShouldBeAbleToAssignUser()
        {
            // Arrange
            var mockUser = new Mock<User>();
            mockUser.Setup(u => u.Id).Returns(101);
            mockUser.Setup(u => u.Username).Returns("John Doe");

            var activity = new Activity
            {
                Name = "Test Activity",
                User = mockUser.Object,
                UserId = mockUser.Object.Id
            };

            // Act & Assert
            Assert.AreEqual(101, activity.User.Id);
            Assert.AreEqual("John Doe", activity.User.Username);
            Assert.AreEqual(101, activity.UserId);
        }

        #endregion
    }
}
