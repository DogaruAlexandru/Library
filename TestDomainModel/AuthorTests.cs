// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuthorTests.cs" company="Transilvania University of Brasov">
//   Copyright (c) Dogaru Alexandru.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TestDomainModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using DomainModel;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Unit tests for the <see cref="Author"/> class.
    /// </summary>
    [TestClass]
    public class AuthorTests
    {
        /// <summary>
        /// Validates that the <see cref="Author.Id"/> property is set correctly.
        /// </summary>
        [TestMethod]
        public void AuthorValidateIdSuccess()
        {
            // Arrange
            var author = new Author
            {
                Id = 1, // Set a valid Id
                Name = "John Doe",
                Books = new List<Book>()
            };

            // Act
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(author, null, null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(author, validationContext, results, true);

            // Assert
            Assert.IsTrue(isValid, "Author Id validation failed.");
        }

        /// <summary>
        /// Validates that the <see cref="Author.Name"/> property within the valid length range passes validation.
        /// </summary>
        [TestMethod]
        public void AuthorValidateNameLengthSuccess()
        {
            // Arrange
            var author = new Author
            {
                Name = "John Doe",
                Books = new List<Book>()
            };

            // Act
            var validationContext = new ValidationContext(author, null, null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(author, validationContext, results, true);

            // Assert
            Assert.IsTrue(isValid, "Author name validation failed.");
        }

        /// <summary>
        /// Validates that the <see cref="Author.Name"/> property fails validation for a too short name.
        /// </summary>
        [TestMethod]
        public void AuthorValidateNameLengthTooShortFailure()
        {
            // Arrange
            var author = new Author
            {
                Name = string.Empty,
                Books = new List<Book>()
            };

            // Act
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(author, null, null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(author, validationContext, results, true);

            // Assert
            Assert.IsFalse(isValid, "Author name validation passed for a too short name.");
            Assert.IsTrue(
                results.Any(vr => vr.MemberNames.Contains(nameof(Author.Name)) && vr.ErrorMessage.Contains("The Name cannot be null")),
                "Expected validation error message not found.");
        }

        /// <summary>
        /// Validates that the <see cref="Author.Name"/> property fails validation for a too long name.
        /// </summary>
        [TestMethod]
        public void AuthorValidateNameLengthTooLongFailure()
        {
            // Arrange
            var author = new Author
            {
                Name = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s",
                Books = new List<Book>()
            };

            // Act
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(author, null, null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(author, validationContext, results, true);

            // Assert
            Assert.IsFalse(isValid, "Author name validation passed for a too long name.");
            Assert.IsTrue(
                results.Any(vr => vr.MemberNames.Contains(nameof(Author.Name)) && vr.ErrorMessage.Contains("between 1 and 100")),
                "Expected validation error message not found.");
        }

        /// <summary>
        /// Validates that the <see cref="Author.Books"/> property within the valid length range passes validation.
        /// </summary>
        [TestMethod]
        public void AuthorValidateBooksNotNullSuccess()
        {
            // Arrange
            var author = new Author
            {
                Name = "Jane Doe",
                Books = new List<Book> { new Book { Title = "Sample Book" } }
            };

            // Act
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(author, null, null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(author, validationContext, results, true);

            // Assert
            Assert.IsTrue(isValid, "Author Books validation failed.");
        }

        /// <summary>
        /// Validates that the <see cref="Author.Books"/> property fails validation for a null Books collection.
        /// </summary>
        [TestMethod]
        public void AuthorValidateBooksNotNullFailure()
        {
            // Arrange
            var author = new Author
            {
                Name = "Jane Doe",
                Books = null
            };

            // Act
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(author, null, null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(author, validationContext, results, true);

            // Assert
            Assert.IsFalse(isValid, "Author Books validation passed for null Books collection.");
            Assert.IsTrue(
                results.Any(vr => vr.MemberNames.Contains(nameof(Author.Books)) && vr.ErrorMessage.Contains("cannot be null")),
                "Expected validation error message not found.");
        }
    }
}
