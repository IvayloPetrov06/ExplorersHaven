using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Explorers_Haven.Models;

namespace Explorers_Haven.Tests
{
    [TestFixture]
    public class UserModelTests
    {
        #region Property Assignments

        [Test]
        public void User_Properties_ShouldBeSetAndGetCorrectly()
        {
            // Arrange
            var user = new User
            {
                Id = 1,
                Username = "TestUser",
                Email = "testuser@example.com",
                Password = "password123",
                ProfilePicture = "profile.jpg",
                Bio = "This is a test user.",
                UserIdentityId = "identityId123"
            };

            // Act & Assert
            Assert.AreEqual(1, user.Id);
            Assert.AreEqual("TestUser", user.Username);
            Assert.AreEqual("testuser@example.com", user.Email);
            Assert.AreEqual("password123", user.Password);
            Assert.AreEqual("profile.jpg", user.ProfilePicture);
            Assert.AreEqual("This is a test user.", user.Bio);
            Assert.AreEqual("identityId123", user.UserIdentityId);
        }

        #endregion

        #region Required Properties Validation

        [Test]
        public void User_Username_ShouldBeRequired()
        {
            // Arrange
            var user = new User
            {
                Username = null
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(user);
            var isValid = Validator.TryValidateObject(user, validationContext, validationResults, true);

            // Assert
            Assert.IsFalse(isValid); // Username is required, so it should fail
            Assert.AreEqual(1, validationResults.Count);
            Assert.AreEqual("The Username field is required.", validationResults[0].ErrorMessage);
        }

        [Test]
        public void User_Email_ShouldBeRequired()
        {
            // Arrange
            var user = new User
            {
                Email = null
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(user);
            var isValid = Validator.TryValidateObject(user, validationContext, validationResults, true);

            // Assert
            Assert.IsFalse(isValid); // Email is required, so it should fail
            Assert.AreEqual(1, validationResults.Count);
            Assert.AreEqual("The Email field is required.", validationResults[0].ErrorMessage);
        }

        [Test]
        public void User_Password_ShouldBeRequired()
        {
            // Arrange
            var user = new User
            {
                Password = null
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(user);
            var isValid = Validator.TryValidateObject(user, validationContext, validationResults, true);

            // Assert
            Assert.IsFalse(isValid); // Password is required, so it should fail
            Assert.AreEqual(1, validationResults.Count);
            Assert.AreEqual("The Password field is required.", validationResults[0].ErrorMessage);
        }

        #endregion

        #region String Length Constraints

        [Test]
        public void User_Username_ShouldHaveMaxLength50()
        {
            // Arrange
            var user = new User
            {
                Username = new string('a', 51) // 51 characters, exceeds max length
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(user);
            var isValid = Validator.TryValidateObject(user, validationContext, validationResults, true);

            // Assert
            Assert.IsFalse(isValid); // Username exceeds max length, so it should fail
            Assert.AreEqual(1, validationResults.Count);
            Assert.AreEqual("The length of the Username field must be 50 characters or less.", validationResults[0].ErrorMessage);
        }

        [Test]
        public void User_Email_ShouldHaveValidFormat()
        {
            // Arrange
            var user = new User
            {
                Email = "invalidemail.com" // Invalid email format
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(user);
            var isValid = Validator.TryValidateObject(user, validationContext, validationResults, true);

            // Assert
            Assert.IsFalse(isValid); // Invalid email format, should fail validation
            Assert.AreEqual(1, validationResults.Count);
            Assert.AreEqual("The Email field is not a valid e-mail address.", validationResults[0].ErrorMessage);
        }

        [Test]
        public void User_Password_ShouldHaveMinLength8AndMaxLength50()
        {
            // Arrange
            var user = new User
            {
                Password = "short" // Password is less than 8 characters
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(user);
            var isValid = Validator.TryValidateObject(user, validationContext, validationResults, true);

            // Assert
            Assert.IsFalse(isValid); // Password should be at least 8 characters
            Assert.AreEqual(1, validationResults.Count);
            Assert.AreEqual("The field Password must be a string with a minimum length of 8 and a maximum length of 50.", validationResults[0].ErrorMessage);
        }

        #endregion

        #region Nullable Properties Validation

        [Test]
        public void User_ProfilePicture_ShouldBeNullable()
        {
            // Arrange
            var user = new User
            {
                ProfilePicture = null // Nullable property
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(user);
            var isValid = Validator.TryValidateObject(user, validationContext, validationResults, true);

            // Assert
            Assert.IsTrue(isValid); // ProfilePicture is nullable, so this should be valid
        }

        [Test]
        public void User_Bio_ShouldBeNullable()
        {
            // Arrange
            var user = new User
            {
                Bio = null // Nullable property
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(user);
            var isValid = Validator.TryValidateObject(user, validationContext, validationResults, true);

            // Assert
            Assert.IsTrue(isValid); // Bio is nullable, so this should be valid
        }

        #endregion

        #region Navigation Properties

        [Test]
        public void User_Offers_ShouldBeNullable()
        {
            // Arrange
            var user = new User
            {
                Offers = null // Nullable navigation property
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(user);
            var isValid = Validator.TryValidateObject(user, validationContext, validationResults, true);

            // Assert
            Assert.IsTrue(isValid); // Offers can be null, so this should be valid
        }

        [Test]
        public void User_Travel_ShouldBeNullable()
        {
            // Arrange
            var user = new User
            {
                Travels = null // Nullable navigation property
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(user);
            var isValid = Validator.TryValidateObject(user, validationContext, validationResults, true);

            // Assert
            Assert.IsTrue(isValid); // Travels can be null, so this should be valid
        }

        #endregion
    }
}
