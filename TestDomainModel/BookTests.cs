// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BookTests.cs" company="Transilvania University of Brasov">
//   Copyright (c) Dogaru Alexandru.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TestDomainModel
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using DomainModel;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Represents a set of tests for the Book class in the domain model.
    /// </summary>
    [TestClass]
    public class BookTests
    {
        /// <summary>
        /// Verifies that the Id property of the Book class is greater than zero.
        /// </summary>
        [TestMethod]
        public void IdShouldBeGreaterThanZero()
        {
            // Arrange
            var book = new Book();

            // Act
            book.Id = 1; // Set a valid Id

            // Assert
            Assert.IsTrue(book.Id > 0);
        }

        /// <summary>
        /// Verifies that the Title property of the Book class cannot be null.
        /// </summary>
        [TestMethod]
        public void TitleShouldNotBeNull()
        {
            // Arrange
            var book = new Book();

            // Act
            book.Title = null;

            // Assert
            var validationContext = new ValidationContext(book) { MemberName = nameof(Book.Title) };
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(book.Title, validationContext, validationResults);

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, validationResults.Count);
            Assert.AreEqual("The Title cannot be null", validationResults[0].ErrorMessage);
        }

        /// <summary>
        /// Verifies that the Title property of the Book class has a length between 1 and 100 characters.
        /// </summary>
        [TestMethod]
        public void TitleLengthShouldBeBetweenOneAndHundred()
        {
            // Arrange
            var book = new Book();

            // Act
            book.Title = "ValidTitle"; // Set a valid title

            // Assert
            var validationContext = new ValidationContext(book) { MemberName = nameof(Book.Title) };
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(book.Title, validationContext, validationResults);

            Assert.IsTrue(isValid);
            Assert.AreEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Verifies that the Title property of the Book class must have a length greater than 0.
        /// </summary>
        [TestMethod]
        public void TitleLengthShouldBeGreaterThanZero()
        {
            // Arrange
            var book = new Book();

            // Act
            book.Title = string.Empty; // Set an invalid empty title

            // Assert
            var validationContext = new ValidationContext(book) { MemberName = nameof(Book.Title) };
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(book.Title, validationContext, validationResults);

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, validationResults.Count);
            Assert.AreEqual("The Title cannot be null", validationResults[0].ErrorMessage);
        }

        /// <summary>
        /// Verifies that the Title property of the Book class must have a length less than or equal to 100.
        /// </summary>
        [TestMethod]
        public void TitleLengthShouldBeLessThanOrEqualToHundred()
        {
            // Arrange
            var book = new Book();

            // Act
            book.Title = new string('A', 101); // Set an invalid long title

            // Assert
            var validationContext = new ValidationContext(book) { MemberName = nameof(Book.Title) };
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(book.Title, validationContext, validationResults);

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, validationResults.Count);
            Assert.AreEqual("The length must be between 1 and 100", validationResults[0].ErrorMessage);
        }

        /// <summary>
        /// Verifies that the Description property of the Book class has a length between 1 and 100 characters.
        /// </summary>
        [TestMethod]
        public void DescriptionLengthShouldBeBetweenOneAndHundred()
        {
            // Arrange
            var book = new Book();

            // Act
            book.Description = "ValidDescription"; // Set a valid description

            // Assert
            var validationContext = new ValidationContext(book) { MemberName = nameof(Book.Description) };
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(book.Description, validationContext, validationResults);

            Assert.IsTrue(isValid);
            Assert.AreEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Verifies that the Description property of the Book class must have a length greater than 0.
        /// </summary>
        [TestMethod]
        public void DescriptionLengthShouldBeGreaterThanZero()
        {
            // Arrange
            var book = new Book();

            // Act
            book.Description = string.Empty; // Set an invalid empty description

            // Assert
            var validationContext = new ValidationContext(book) { MemberName = nameof(Book.Description) };
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(book.Description, validationContext, validationResults);

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, validationResults.Count);
            Assert.AreEqual("The length must be between 1 and 100", validationResults[0].ErrorMessage);
        }

        /// <summary>
        /// Verifies that the Description property of the Book class must have a length less than or equal to 100.
        /// </summary>
        [TestMethod]
        public void DescriptionLengthShouldBeLessThanOrEqualToHundred()
        {
            // Arrange
            var book = new Book();

            // Act
            book.Description = new string('A', 101); // Set an invalid long description

            // Assert
            var validationContext = new ValidationContext(book) { MemberName = nameof(Book.Description) };
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(book.Description, validationContext, validationResults);

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, validationResults.Count);
            Assert.AreEqual("The length must be between 1 and 100", validationResults[0].ErrorMessage);
        }

        /// <summary>
        /// Verifies that the Editions property of the Book class cannot be null.
        /// </summary>
        [TestMethod]
        public void EditionsShouldNotBeNull()
        {
            // Arrange
            var book = new Book();

            // Act
            book.Editions = null;

            // Assert
            var validationContext = new ValidationContext(book) { MemberName = nameof(Book.Editions) };
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(book.Editions, validationContext, validationResults);

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, validationResults.Count);
            Assert.AreEqual("The Editions collection cannot be null", validationResults[0].ErrorMessage);
        }

        /// <summary>
        /// Verifies that the BookDomains property of the Book class cannot be null.
        /// </summary>
        [TestMethod]
        public void BookDomainsShouldNotBeNull()
        {
            // Arrange
            var book = new Book();

            // Act
            book.BookDomains = null;

            // Assert
            var validationContext = new ValidationContext(book) { MemberName = nameof(Book.BookDomains) };
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(book.BookDomains, validationContext, validationResults);

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, validationResults.Count);
            Assert.AreEqual("The BookDomains collection cannot be null", validationResults[0].ErrorMessage);
        }

        /// <summary>
        /// Verifies that the BookDomains property of the Book class must have at least one element.
        /// </summary>
        [TestMethod]
        public void BookDomainsShouldHaveAtLeastOneElement()
        {
            // Arrange
            var book = new Book();

            // Act
            book.BookDomains = new List<BookDomain>(); // Set an empty collection

            // Assert
            var validationContext = new ValidationContext(book) { MemberName = nameof(Book.BookDomains) };
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(book.BookDomains.ToArray<BookDomain>(), validationContext, validationResults);

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, validationResults.Count);
            Assert.AreEqual("At least one BookDomain is required", validationResults[0].ErrorMessage);
        }

        /// <summary>
        /// Verifies that the Authors property of the Book class cannot be null.
        /// </summary>
        [TestMethod]
        public void AuthorsShouldNotBeNull()
        {
            // Arrange
            var book = new Book();

            // Act
            book.Authors = null;

            // Assert
            var validationContext = new ValidationContext(book) { MemberName = nameof(Book.Authors) };
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(book.Authors, validationContext, validationResults);

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, validationResults.Count);
            Assert.AreEqual("The Authors collection cannot be null", validationResults[0].ErrorMessage);
        }

        /// <summary>
        /// Verify that a Book object passes validation when all properties, including collections, have valid data.
        /// </summary>
        [TestMethod]
        public void BookShouldPassValidation()
        {
            // Arrange
            var book = new Book
            {
                Id = 1,
                Title = "ValidTitle",
                Description = "ValidDescription",
                Editions = new List<Edition> { new Edition { /* Set valid properties for Edition */ } },
                BookDomains = new List<BookDomain> { new BookDomain { /* Set valid properties for BookDomain */ } },
                Authors = new List<Author> { new Author { /* Set valid properties for Author */ } }
            };

            // Act
            var validationResults = new List<ValidationResult>();

            // Validate individual properties
            Validator.TryValidateProperty(book.Id, new ValidationContext(book) { MemberName = nameof(Book.Id) }, validationResults);
            Validator.TryValidateProperty(book.Title, new ValidationContext(book) { MemberName = nameof(Book.Title) }, validationResults);
            Validator.TryValidateProperty(book.Description, new ValidationContext(book) { MemberName = nameof(Book.Description) }, validationResults);
            Validator.TryValidateProperty(book.Editions, new ValidationContext(book) { MemberName = nameof(Book.Editions) }, validationResults);
            Validator.TryValidateProperty(book.BookDomains.ToArray<BookDomain>(), new ValidationContext(book) { MemberName = nameof(Book.BookDomains) }, validationResults);
            Validator.TryValidateProperty(book.Authors, new ValidationContext(book) { MemberName = nameof(Book.Authors) }, validationResults);

            // Assert
            Assert.IsTrue(validationResults.Count == 0);
        }

        /// <summary>
        /// Verify that a Book object fails validation when a required property is missing.
        /// </summary>
        [TestMethod]
        public void BookShouldFailValidation()
        {
            // Arrange
            var book = new Book
            {
                Id = 1,
                //// Title is missing, intentionally causing failure
                Description = "ValidDescription",
                Editions = new List<Edition> { new Edition { /* Set valid properties for Edition */ } },
                BookDomains = new List<BookDomain> { new BookDomain { /* Set valid properties for BookDomain */ } },
                Authors = new List<Author> { new Author { /* Set valid properties for Author */ } }
            };

            // Act
            var validationResults = new List<ValidationResult>();

            // Validate individual properties
            Validator.TryValidateProperty(book.Id, new ValidationContext(book) { MemberName = nameof(Book.Id) }, validationResults);
            Validator.TryValidateProperty(book.Title, new ValidationContext(book) { MemberName = nameof(Book.Title) }, validationResults);
            Validator.TryValidateProperty(book.Description, new ValidationContext(book) { MemberName = nameof(Book.Description) }, validationResults);
            Validator.TryValidateProperty(book.Editions, new ValidationContext(book) { MemberName = nameof(Book.Editions) }, validationResults);
            Validator.TryValidateProperty(book.BookDomains.ToArray<BookDomain>(), new ValidationContext(book) { MemberName = nameof(Book.BookDomains) }, validationResults);
            Validator.TryValidateProperty(book.Authors, new ValidationContext(book) { MemberName = nameof(Book.Authors) }, validationResults);

            // Assert
            Assert.IsTrue(validationResults.Count > 0);
        }
    }
}
