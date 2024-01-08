// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BookDomainTests.cs" company="Transilvania University of Brasov">
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
    /// Represents a set of tests for the BookDomain class in the domain model.
    /// </summary>
    [TestClass]
    public class BookDomainTests
    {
        /// <summary>
        /// Verifies that the Id property of the BookDomain class is greater than zero.
        /// </summary>
        [TestMethod]
        public void IdShouldBeGreaterThanZero()
        {
            // Arrange
            var bookDomain = new BookDomain();

            // Act
            bookDomain.Id = 1; // Set a valid Id

            // Assert
            Assert.IsTrue(bookDomain.Id > 0);
        }

        /// <summary>
        /// Verifies that the Name property of the BookDomain class cannot be null.
        /// </summary>
        [TestMethod]
        public void NameShouldNotBeNull()
        {
            // Arrange
            var bookDomain = new BookDomain();

            // Act
            bookDomain.Name = null;

            // Assert
            var validationContext = new ValidationContext(bookDomain) { MemberName = nameof(BookDomain.Name) };
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(bookDomain.Name, validationContext, validationResults);

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, validationResults.Count);
            Assert.AreEqual("The Name cannot be null", validationResults[0].ErrorMessage);
        }

        /// <summary>
        /// Verifies that the Name property of the BookDomain class has a length between 1 and 100 characters.
        /// </summary>
        [TestMethod]
        public void NameLengthShouldBeBetweenOneAndHundred()
        {
            // Arrange
            var bookDomain = new BookDomain();

            // Act
            bookDomain.Name = "ValidName"; // Set a valid name

            // Assert
            var validationContext = new ValidationContext(bookDomain) { MemberName = nameof(BookDomain.Name) };
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(bookDomain.Name, validationContext, validationResults);

            Assert.IsTrue(isValid);
            Assert.AreEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Verifies that the Name property of the BookDomain class has a length between 1 and 100 characters.
        /// </summary>
        [TestMethod]
        public void NameLengthShouldBeGreaterThanZero()
        {
            // Arrange
            var bookDomain = new BookDomain();

            // Act
            bookDomain.Name = string.Empty; // Set an invalid empty name

            // Assert
            var validationContext = new ValidationContext(bookDomain) { MemberName = nameof(BookDomain.Name) };
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(bookDomain.Name, validationContext, validationResults);

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, validationResults.Count);
            Assert.AreEqual("The Name cannot be null", validationResults[0].ErrorMessage);
        }

        /// <summary>
        /// Verifies that the Name property of the BookDomain class must have a length less than or equal to 100.
        /// </summary>
        [TestMethod]
        public void NameLengthShouldBeLessThanOrEqualToHundred()
        {
            // Arrange
            var bookDomain = new BookDomain();

            // Act
            bookDomain.Name = new string('A', 101); // Set an invalid long name

            // Assert
            var validationContext = new ValidationContext(bookDomain) { MemberName = nameof(BookDomain.Name) };
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(bookDomain.Name, validationContext, validationResults);

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, validationResults.Count);
            Assert.AreEqual("The length must be between 1 and 100", validationResults[0].ErrorMessage);
        }

        /// <summary>
        /// Verifies that the ParentDomain property of the BookDomain class can be set to null.
        /// </summary>
        [TestMethod]
        public void ParentDomainShouldBeNull()
        {
            // Arrange
            var bookDomain = new BookDomain();

            // Act
            bookDomain.ParentDomain = null;

            // Assert
            Assert.IsNull(bookDomain.ParentDomain);
        }

        /// <summary>
        /// Verifies that the ParentDomain property of the BookDomain class can be set to a valid BookDomain instance.
        /// </summary>
        [TestMethod]
        public void ParentDomainShouldBeSet()
        {
            // Arrange
            var bookDomain = new BookDomain();
            var parentDomain = new BookDomain();

            // Act
            bookDomain.ParentDomain = parentDomain;

            // Assert
            Assert.AreEqual(parentDomain, bookDomain.ParentDomain);
        }

        /// <summary>
        /// Verifies that the Books collection of the BookDomain class can be null.
        /// </summary>
        [TestMethod]
        public void BooksCollectionCanBeNull()
        {
            // Arrange
            var bookDomain = new BookDomain();

            // Act
            bookDomain.Books = null;

            // Assert
            var validationContext = new ValidationContext(bookDomain) { MemberName = nameof(BookDomain.Books) };
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(bookDomain.Books, validationContext, validationResults);

            Assert.IsTrue(isValid);
            Assert.AreEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Verifies that the Books collection of the BookDomain class cannot be empty.
        /// </summary>
        [TestMethod]
        public void BooksCollectionCanNotBeEmpty()
        {
            // Arrange
            var bookDomain = new BookDomain();

            // Act
            bookDomain.Books = new List<Book>();

            // Assert
            var validationContext = new ValidationContext(bookDomain) { MemberName = nameof(BookDomain.Books) };
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(bookDomain.Books.ToArray<Book>(), validationContext, validationResults);

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, validationResults.Count);
            Assert.AreEqual("At least one Book is required", validationResults[0].ErrorMessage);
        }

        /// <summary>
        /// Verifies that the Books collection of the BookDomain class can be populated with valid Book instances.
        /// </summary>
        [TestMethod]
        public void BooksCollectionCanBePopulated()
        {
            // Arrange
            var bookDomain = new BookDomain();
            var book1 = new Book();
            var book2 = new Book();

            // Act
            bookDomain.Books = new List<Book> { book1, book2 };

            // Assert
            var validationContext = new ValidationContext(bookDomain) { MemberName = nameof(BookDomain.Books) };
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(bookDomain.Books.ToArray<Book>(), validationContext, validationResults);

            Assert.IsTrue(isValid);
            Assert.AreEqual(0, validationResults.Count);
            Assert.IsNotNull(bookDomain.Books);
            Assert.AreEqual(2, bookDomain.Books.Count);
            Assert.IsTrue(bookDomain.Books.Contains(book1));
            Assert.IsTrue(bookDomain.Books.Contains(book2));
        }

        /// <summary>
        /// Verifies that a valid BookDomain object passes validation.
        /// </summary>
        [TestMethod]
        public void ValidBookDomainShouldPassValidation()
        {
            // Arrange
            var validBookDomain = new BookDomain
            {
                Id = 1,
                Name = "ValidBookDomain",
                ParentDomain = null, // Set a valid null parent domain
                Books = new List<Book> { new Book(), new Book { Title = "Title" }, new Book() }
            };

            // Act
            var validationResults = new List<ValidationResult>();

            // Validate individual properties
            Validator.TryValidateProperty(validBookDomain.Id, new ValidationContext(validBookDomain) { MemberName = nameof(BookDomain.Id) }, validationResults);
            Validator.TryValidateProperty(validBookDomain.Name, new ValidationContext(validBookDomain) { MemberName = nameof(BookDomain.Name) }, validationResults);
            Validator.TryValidateProperty(validBookDomain.ParentDomain, new ValidationContext(validBookDomain) { MemberName = nameof(BookDomain.ParentDomain) }, validationResults);
            Validator.TryValidateProperty(validBookDomain.Books.ToArray<Book>(), new ValidationContext(validBookDomain) { MemberName = nameof(BookDomain.Books) }, validationResults);

            // Assert
            Assert.IsTrue(validationResults.Count == 0);
        }

        /// <summary>
        /// Verifies that an invalid BookDomain object fails validation.
        /// </summary>
        [TestMethod]
        public void InvalidBookDomainShouldFailValidation()
        {
            // Arrange
            var invalidBookDomain = new BookDomain
            {
                Id = 1,
                //// Name is missing, intentionally causing failure
                ParentDomain = null, // Set a valid null parent domain
                Books = new List<Book> { new Book(), new Book { Title = "Title" }, new Book() }
            };

            // Act
            var validationResults = new List<ValidationResult>();

            // Validate individual properties
            Validator.TryValidateProperty(invalidBookDomain.Id, new ValidationContext(invalidBookDomain) { MemberName = nameof(BookDomain.Id) }, validationResults);
            Validator.TryValidateProperty(invalidBookDomain.Name, new ValidationContext(invalidBookDomain) { MemberName = nameof(BookDomain.Name) }, validationResults);
            Validator.TryValidateProperty(invalidBookDomain.ParentDomain, new ValidationContext(invalidBookDomain) { MemberName = nameof(BookDomain.ParentDomain) }, validationResults);
            Validator.TryValidateProperty(invalidBookDomain.Books.ToArray<Book>(), new ValidationContext(invalidBookDomain) { MemberName = nameof(BookDomain.Books) }, validationResults);

            // Assert
            Assert.IsTrue(validationResults.Count > 0);
        }
    }
}
