using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Explorers_Haven.Models;

namespace Explorers_Haven.Tests
{
    [TestFixture]
    public class CommentModelTests
    {
        #region Property Assignments

        [Test]
        public void Comment_Properties_ShouldBeSetAndGetCorrectly()
        {
            // Arrange
            var comment = new Comment
            {
                Id = 1,
                Content = "Great experience!",
                Stars = 5,
                UserId = 101,
                OfferId = 202
            };

            // Act & Assert
            Assert.AreEqual(1, comment.Id);
            Assert.AreEqual("Great experience!", comment.Content);
            Assert.AreEqual(5, comment.Stars);
            Assert.AreEqual(101, comment.UserId);
            Assert.AreEqual(202, comment.OfferId);
        }

        #endregion

        #region Content Property Validation

        [Test]
        public void Comment_Content_ShouldHaveMaxLengthOf500Characters()
        {
            // Arrange
            var comment = new Comment
            {
                Content = new string('a', 500) // 500 characters
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(comment);
            var isValid = Validator.TryValidateObject(comment, validationContext, validationResults, true);

            // Assert
            Assert.IsTrue(isValid); // Should be valid for 500 characters

            // Now test with 501 characters (should fail)
            comment.Content = new string('a', 501);
            validationResults.Clear();
            isValid = Validator.TryValidateObject(comment, validationContext, validationResults, true);

            Assert.IsFalse(isValid); // Should be invalid for 501 characters
            Assert.AreEqual(1, validationResults.Count);
            Assert.AreEqual("The field Content must be a string with a maximum length of 500.", validationResults[0].ErrorMessage);
        }

        #endregion

        #region Stars Property Validation

        [Test]
        public void Comment_Stars_ShouldBeBetween1And5()
        {
            // Arrange
            var comment = new Comment
            {
                Stars = 3 // Valid value
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(comment);
            var isValid = Validator.TryValidateObject(comment, validationContext, validationResults, true);

            // Assert
            Assert.IsTrue(isValid); // Should be valid for 3 stars

            // Now test with an invalid value (e.g., 6)
            comment.Stars = 6;
            validationResults.Clear();
            isValid = Validator.TryValidateObject(comment, validationContext, validationResults, true);

            Assert.IsFalse(isValid); // Should be invalid for stars > 5
            Assert.AreEqual(1, validationResults.Count);
            Assert.AreEqual("Value must be between 1 and 5.", validationResults[0].ErrorMessage);

            // Now test with an invalid value (e.g., 0)
            comment.Stars = 0;
            validationResults.Clear();
            isValid = Validator.TryValidateObject(comment, validationContext, validationResults, true);

            Assert.IsFalse(isValid); // Should be invalid for stars < 1
            Assert.AreEqual(1, validationResults.Count);
            Assert.AreEqual("Value must be between 1 and 5.", validationResults[0].ErrorMessage);
        }

        #endregion

        #region Nullable Properties Validation

        [Test]
        public void Comment_UserId_ShouldBeNullable()
        {
            // Arrange
            var comment = new Comment
            {
                Content = "Nice offer!",
                Stars = 5,
                UserId = null // Nullable property
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(comment);
            var isValid = Validator.TryValidateObject(comment, validationContext, validationResults, true);

            // Assert
            Assert.IsTrue(isValid); // Should be valid even if 'UserId' is null
        }

        [Test]
        public void Comment_OfferId_ShouldBeNullable()
        {
            // Arrange
            var comment = new Comment
            {
                Content = "Amazing service!",
                Stars = 5,
                OfferId = null // Nullable property
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(comment);
            var isValid = Validator.TryValidateObject(comment, validationContext, validationResults, true);

            // Assert
            Assert.IsTrue(isValid); // Should be valid even if 'OfferId' is null
        }

        #endregion

        #region Validation of Invalid Data

        [Test]
        public void Comment_WithInvalidStars_ShouldFailValidation()
        {
            // Arrange
            var comment = new Comment
            {
                Content = "Poor experience.",
                Stars = 0 // Invalid value, should fail validation
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(comment);
            var isValid = Validator.TryValidateObject(comment, validationContext, validationResults, true);

            // Assert
            Assert.IsFalse(isValid); // Should fail validation because 'Stars' is less than 1
            Assert.AreEqual(1, validationResults.Count);
            Assert.AreEqual("Value must be between 1 and 5.", validationResults[0].ErrorMessage); // Example error message
        }

        [Test]
        public void Comment_WithTooLongContent_ShouldFailValidation()
        {
            // Arrange
            var comment = new Comment
            {
                Content = new string('a', 501), // Invalid length (more than 500 characters)
                Stars = 3
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(comment);
            var isValid = Validator.TryValidateObject(comment, validationContext, validationResults, true);

            // Assert
            Assert.IsFalse(isValid); // Should fail validation because 'Content' is longer than 500 characters
            Assert.AreEqual(1, validationResults.Count);
            Assert.AreEqual("The field Content must be a string with a maximum length of 500.", validationResults[0].ErrorMessage);
        }

        #endregion
    }
}
