// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EditionTests.cs" company="Transilvania University of Brasov">
//   Copyright (c) Dogaru Alexandru.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TestDomainModel
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using DomainModel;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Represents a set of tests for the Edition class in the domain model.
    /// </summary>
    [TestClass]
    public class EditionTests
    {
        /// <summary>
        /// Verifies that the Id property of the Book class is greater than zero.
        /// </summary>
        [TestMethod]
        public void IdShouldBeGreaterThanZero()
        {
            // Arrange
            var edition = new Edition();

            // Act
            edition.Id = 1; // Set a valid Id

            // Assert
            Assert.IsTrue(edition.Id > 0);
        }

        /// <summary>
        /// Verifies that the Type property of the Edition class must be a valid BookType enum value.
        /// </summary>
        [TestMethod]
        public void TypeShouldBeTradeHardcover()
        {
            // Arrange
            var edition = new Edition();

            // Act
            edition.Type = BookType.TradeHardcover; // Set a valid enum value

            // Assert
            var validationContext = new ValidationContext(edition) { MemberName = nameof(Edition.Type) };
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(edition.Type, validationContext, validationResults);

            Assert.IsTrue(isValid);
            Assert.AreEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Verifies that the Type property of the Edition class must be a valid BookType enum value.
        /// </summary>
        [TestMethod]
        public void TypeShouldBeMassMarketHardcover()
        {
            // Arrange
            var edition = new Edition();

            // Act
            edition.Type = BookType.MassMarketHardcover; // Set a valid enum value

            // Assert
            var validationContext = new ValidationContext(edition) { MemberName = nameof(Edition.Type) };
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(edition.Type, validationContext, validationResults);

            Assert.IsTrue(isValid);
            Assert.AreEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Verifies that the Type property of the Edition class must be a valid BookType enum value.
        /// </summary>
        [TestMethod]
        public void TypeShouldBeOversizedHardcover()
        {
            // Arrange
            var edition = new Edition();

            // Act
            edition.Type = BookType.OversizedHardcover; // Set a valid enum value

            // Assert
            var validationContext = new ValidationContext(edition) { MemberName = nameof(Edition.Type) };
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(edition.Type, validationContext, validationResults);

            Assert.IsTrue(isValid);
            Assert.AreEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Verifies that the Type property of the Edition class must be a valid BookType enum value.
        /// </summary>
        [TestMethod]
        public void TypeShouldBeLibraryBinding()
        {
            // Arrange
            var edition = new Edition();

            // Act
            edition.Type = BookType.LibraryBinding; // Set a valid enum value

            // Assert
            var validationContext = new ValidationContext(edition) { MemberName = nameof(Edition.Type) };
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(edition.Type, validationContext, validationResults);

            Assert.IsTrue(isValid);
            Assert.AreEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Verifies that the Type property of the Edition class must be a valid BookType enum value.
        /// </summary>
        [TestMethod]
        public void TypeShouldBeDeluxeEditions()
        {
            // Arrange
            var edition = new Edition();

            // Act
            edition.Type = BookType.DeluxeEditions; // Set a valid enum value

            // Assert
            var validationContext = new ValidationContext(edition) { MemberName = nameof(Edition.Type) };
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(edition.Type, validationContext, validationResults);

            Assert.IsTrue(isValid);
            Assert.AreEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Verifies that the Type property of the Edition class must be a valid BookType enum value.
        /// </summary>
        [TestMethod]
        public void TypeShouldBeCollectorEdition()
        {
            // Arrange
            var edition = new Edition();

            // Act
            edition.Type = BookType.CollectorEdition; // Set a valid enum value

            // Assert
            var validationContext = new ValidationContext(edition) { MemberName = nameof(Edition.Type) };
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(edition.Type, validationContext, validationResults);

            Assert.IsTrue(isValid);
            Assert.AreEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Verifies that the Type property of the Edition class must be a valid BookType enum value.
        /// </summary>
        [TestMethod]
        public void TypeShouldBeCaseWrapHardcover()
        {
            // Arrange
            var edition = new Edition();

            // Act
            edition.Type = BookType.CaseWrapHardcover; // Set a valid enum value

            // Assert
            var validationContext = new ValidationContext(edition) { MemberName = nameof(Edition.Type) };
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(edition.Type, validationContext, validationResults);

            Assert.IsTrue(isValid);
            Assert.AreEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Verifies that the Type property of the Edition class must be a valid BookType enum value.
        /// </summary>
        [TestMethod]
        public void TypeShouldBeDustJacket()
        {
            // Arrange
            var edition = new Edition();

            // Act
            edition.Type = BookType.DustJacket; // Set a valid enum value

            // Assert
            var validationContext = new ValidationContext(edition) { MemberName = nameof(Edition.Type) };
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(edition.Type, validationContext, validationResults);

            Assert.IsTrue(isValid);
            Assert.AreEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Verifies that setting different enum values format for the Type property results in equality.
        /// </summary>
        [TestMethod]
        public void DifferentEnumValuesFormatShouldBeEqual()
        {
            // Arrange
            var editionA = new Edition();
            var editionB = new Edition();

            // Act
            editionA.Type = BookType.TradeHardcover;
            editionB.Type = 0;

            // Assert
            Assert.IsTrue(editionA.Type == editionB.Type);
        }

        /// <summary>
        /// Verifies that the PageCount property of the Edition class cannot be null.
        /// </summary>
        [TestMethod]
        public void PageCountShouldNotBeNull()
        {
            // Arrange
            var edition = new Edition();

            // Act

            // Assert
            var validationContext = new ValidationContext(edition) { MemberName = nameof(Edition.PageCount) };
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(edition.PageCount, validationContext, validationResults);

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, validationResults.Count);
            Assert.AreEqual("The PageCount must be at least 1", validationResults[0].ErrorMessage);
        }

        /// <summary>
        /// Verifies that the PageCount property of the Edition class must be greater than or equal to 1.
        /// </summary>
        [TestMethod]
        public void PageCountShouldBeGreaterThanZero()
        {
            // Arrange
            var edition = new Edition();

            // Act
            edition.PageCount = 0; // Set an invalid page count

            // Assert
            var validationContext = new ValidationContext(edition) { MemberName = nameof(Edition.PageCount) };
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(edition.PageCount, validationContext, validationResults);

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, validationResults.Count);
            Assert.AreEqual("The PageCount must be at least 1", validationResults[0].ErrorMessage);
        }

        /// <summary>
        /// Verifies that the PageCount property of the Edition class is valid when set to a positive value.
        /// </summary>
        [TestMethod]
        public void ValidPageCountShouldPassValidation()
        {
            // Arrange
            var edition = new Edition();

            // Act
            edition.PageCount = 100; // Set a valid page count

            // Assert
            var validationContext = new ValidationContext(edition) { MemberName = nameof(Edition.PageCount) };
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(edition.PageCount, validationContext, validationResults);

            Assert.IsTrue(isValid);
            Assert.AreEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Verifies that the Name property of the Edition class cannot be null.
        /// </summary>
        [TestMethod]
        public void NameShouldNotBeNull()
        {
            // Arrange
            var edition = new Edition();

            // Act
            edition.Name = null;

            // Assert
            var validationContext = new ValidationContext(edition) { MemberName = nameof(Edition.Name) };
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(edition.Name, validationContext, validationResults);

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, validationResults.Count);
            Assert.AreEqual("The Name cannot be null", validationResults[0].ErrorMessage);
        }

        /// <summary>
        /// Verifies that the Name property of the Edition class must have a length between 1 and 100 characters.
        /// </summary>
        [TestMethod]
        public void NameLengthShouldBeBetweenOneAndHundred()
        {
            // Arrange
            var edition = new Edition();

            // Act
            edition.Name = "ValidName"; // Set a valid name

            // Assert
            var validationContext = new ValidationContext(edition) { MemberName = nameof(Edition.Name) };
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(edition.Name, validationContext, validationResults);

            Assert.IsTrue(isValid);
            Assert.AreEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Verifies that the Name property of the Edition class must have a length greater than 0.
        /// </summary>
        [TestMethod]
        public void NameLengthShouldBeGreaterThanZero()
        {
            // Arrange
            var edition = new Edition();

            // Act
            edition.Name = string.Empty; // Set an invalid empty name

            // Assert
            var validationContext = new ValidationContext(edition) { MemberName = nameof(Edition.Name) };
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(edition.Name, validationContext, validationResults);

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, validationResults.Count);
            Assert.AreEqual("The Name cannot be null", validationResults[0].ErrorMessage);
        }

        /// <summary>
        /// Verifies that the Name property of the Edition class must have a length less than or equal to 100.
        /// </summary>
        [TestMethod]
        public void NameLengthShouldBeLessThanOrEqualToHundred()
        {
            // Arrange
            var edition = new Edition();

            // Act
            edition.Name = new string('A', 101); // Set an invalid long name

            // Assert
            var validationContext = new ValidationContext(edition) { MemberName = nameof(Edition.Name) };
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(edition.Name, validationContext, validationResults);

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, validationResults.Count);
            Assert.AreEqual("The length must be between 1 and 100", validationResults[0].ErrorMessage);
        }

        /// <summary>
        /// Verifies that the Publisher property of the Edition class cannot be null.
        /// </summary>
        [TestMethod]
        public void PublisherShouldNotBeNull()
        {
            // Arrange
            var edition = new Edition();

            // Act
            edition.Publisher = null;

            // Assert
            var validationContext = new ValidationContext(edition) { MemberName = nameof(Edition.Publisher) };
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(edition.Publisher, validationContext, validationResults);

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, validationResults.Count);
            Assert.AreEqual("The Publisher cannot be null", validationResults[0].ErrorMessage);
        }

        /// <summary>
        /// Verifies that the Publisher property of the Edition class must have a length between 1 and 100 characters.
        /// </summary>
        [TestMethod]
        public void PublisherLengthShouldBeBetweenOneAndHundred()
        {
            // Arrange
            var edition = new Edition();

            // Act
            edition.Publisher = "ValidPublisher"; // Set a valid publisher

            // Assert
            var validationContext = new ValidationContext(edition) { MemberName = nameof(Edition.Publisher) };
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(edition.Publisher, validationContext, validationResults);

            Assert.IsTrue(isValid);
            Assert.AreEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Verifies that the Publisher property of the Edition class must have a length greater than 0.
        /// </summary>
        [TestMethod]
        public void PublisherLengthShouldBeGreaterThanZero()
        {
            // Arrange
            var edition = new Edition();

            // Act
            edition.Publisher = string.Empty; // Set an invalid empty publisher

            // Assert
            var validationContext = new ValidationContext(edition) { MemberName = nameof(Edition.Publisher) };
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(edition.Publisher, validationContext, validationResults);

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, validationResults.Count);
            Assert.AreEqual("The Publisher cannot be null", validationResults[0].ErrorMessage);
        }

        /// <summary>
        /// Verifies that the Publisher property of the Edition class must have a length less than or equal to 100.
        /// </summary>
        [TestMethod]
        public void PublisherLengthShouldBeLessThanOrEqualToHundred()
        {
            // Arrange
            var edition = new Edition();

            // Act
            edition.Publisher = new string('A', 101); // Set an invalid long publisher

            // Assert
            var validationContext = new ValidationContext(edition) { MemberName = nameof(Edition.Publisher) };
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(edition.Publisher, validationContext, validationResults);

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, validationResults.Count);
            Assert.AreEqual("The length must be between 1 and 100", validationResults[0].ErrorMessage);
        }

        /// <summary>
        /// Verifies that the CanNotBorrow property of the Edition class can be set to 0.
        /// </summary>
        [TestMethod]
        public void CanNotBorrowCanBeZero()
        {
            // Arrange
            var edition = new Edition();

            // Act
            edition.CanNotBorrow = 0;

            // Assert
            var validationContext = new ValidationContext(edition) { MemberName = nameof(Edition.CanNotBorrow) };
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(edition.CanNotBorrow, validationContext, validationResults);

            Assert.IsTrue(isValid);
            Assert.AreEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Verifies that the CanNotBorrow property of the Edition class cannot be set to null.
        /// </summary>
        [TestMethod]
        public void CanNotBorrowCannotBeNull()
        {
            // Arrange
            var edition = new Edition();

            // Act
            edition.CanNotBorrow = null;

            // Assert
            var validationContext = new ValidationContext(edition) { MemberName = nameof(Edition.CanNotBorrow) };
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(edition.CanNotBorrow, validationContext, validationResults);

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, validationResults.Count);
            Assert.AreEqual("The CanNotBorrow cannot be null", validationResults[0].ErrorMessage);
        }

        /// <summary>
        /// Verifies that the CanNotBorrow property of the Edition class cannot be set to a value below 0.
        /// </summary>
        [TestMethod]
        public void CanNotBorrowCannotBeBelowZero()
        {
            // Arrange
            var edition = new Edition();

            // Act
            edition.CanNotBorrow = -1;

            // Assert
            var validationContext = new ValidationContext(edition) { MemberName = nameof(Edition.CanNotBorrow) };
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(edition.CanNotBorrow, validationContext, validationResults);

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, validationResults.Count);
            Assert.AreEqual("The CanNotBorrow must be above 0", validationResults[0].ErrorMessage);
        }

        /// <summary>
        /// Verifies that the CanNotBorrow property of the Edition class can be set to a value above 0.
        /// </summary>
        [TestMethod]
        public void CanNotBorrowCanBeAboveZero()
        {
            // Arrange
            var edition = new Edition();

            // Act
            edition.CanNotBorrow = 1;

            // Assert
            var validationContext = new ValidationContext(edition) { MemberName = nameof(Edition.CanNotBorrow) };
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(edition.CanNotBorrow, validationContext, validationResults);

            Assert.IsTrue(isValid);
            Assert.AreEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Verifies that the CanBorrow property of the Edition class can be set to 0.
        /// </summary>
        [TestMethod]
        public void CanBorrowCanBeZero()
        {
            // Arrange
            var edition = new Edition();

            // Act
            edition.CanBorrow = 0;

            // Assert
            var validationContext = new ValidationContext(edition) { MemberName = nameof(Edition.CanBorrow) };
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(edition.CanBorrow, validationContext, validationResults);

            Assert.IsTrue(isValid);
            Assert.AreEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Verifies that the CanBorrow property of the Edition class cannot be set to null.
        /// </summary>
        [TestMethod]
        public void CanBorrowCannotBeNull()
        {
            // Arrange
            var edition = new Edition();

            // Act
            edition.CanBorrow = null;

            // Assert
            var validationContext = new ValidationContext(edition) { MemberName = nameof(Edition.CanBorrow) };
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(edition.CanBorrow, validationContext, validationResults);

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, validationResults.Count);
            Assert.AreEqual("The CanBorrow cannot be null", validationResults[0].ErrorMessage);
        }

        /// <summary>
        /// Verifies that the CanBorrow property of the Edition class cannot be set to a value below 0.
        /// </summary>
        [TestMethod]
        public void CanBorrowCannotBeBelowZero()
        {
            // Arrange
            var edition = new Edition();

            // Act
            edition.CanBorrow = -1;

            // Assert
            var validationContext = new ValidationContext(edition) { MemberName = nameof(Edition.CanBorrow) };
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(edition.CanBorrow, validationContext, validationResults);

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, validationResults.Count);
            Assert.AreEqual("The CanBorrow must be above 0", validationResults[0].ErrorMessage);
        }

        /// <summary>
        /// Verifies that the CanBorrow property of the Edition class can be set to a value above 0.
        /// </summary>
        [TestMethod]
        public void CanBorrowCanBeAboveZero()
        {
            // Arrange
            var edition = new Edition();

            // Act
            edition.CanBorrow = 1;

            // Assert
            var validationContext = new ValidationContext(edition) { MemberName = nameof(Edition.CanBorrow) };
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(edition.CanBorrow, validationContext, validationResults);

            Assert.IsTrue(isValid);
            Assert.AreEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Verifies that the Book property of the Edition class cannot be null.
        /// </summary>
        [TestMethod]
        public void BookShouldNotBeNull()
        {
            // Arrange
            var edition = new Edition();

            // Act
            edition.Book = null;

            // Assert
            var validationContext = new ValidationContext(edition) { MemberName = nameof(Edition.Book) };
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(edition.Book, validationContext, validationResults);

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, validationResults.Count);
            Assert.AreEqual("The Book cannot be null", validationResults[0].ErrorMessage);
        }

        /// <summary>
        /// Verifies that the Book property of the Edition class passes validation when set to a valid book.
        /// </summary>
        [TestMethod]
        public void ValidBookShouldPassValidation()
        {
            // Arrange
            var edition = new Edition();
            var validBook = new Book(); // Create a valid book

            // Act
            edition.Book = validBook;

            // Assert
            var validationContext = new ValidationContext(edition) { MemberName = nameof(Edition.Book) };
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(edition.Book, validationContext, validationResults);

            Assert.IsTrue(isValid);
            Assert.AreEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Verifies that a valid Edition object passes validation.
        /// </summary>
        [TestMethod]
        public void ValidEditionShouldPassValidation()
        {
            // Arrange
            var validEdition = new Edition
            {
                Id = 1,
                Type = BookType.TradeHardcover,
                PageCount = 300,
                Name = "ValidEdition",
                Publisher = "ValidPublisher",
                CanNotBorrow = 5,
                CanBorrow = 10,
                Book = new Book()
            };

            // Act
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(validEdition, new ValidationContext(validEdition), validationResults, validateAllProperties: true);

            // Assert
            Assert.IsTrue(validationResults.Count == 0);
        }

        /// <summary>
        /// Verifies that an invalid Edition object fails validation.
        /// </summary>
        [TestMethod]
        public void InvalidEditionShouldFailValidation()
        {
            // Arrange
            var invalidEdition = new Edition
            {
                Id = 1,
                Type = BookType.TradeHardcover,
                PageCount = 300,
                //// Name is missing, intentionally causing failure
                Publisher = "ValidPublisher",
                CanNotBorrow = 5,
                CanBorrow = 10,
                Book = new Book()
            };

            // Act
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(invalidEdition, new ValidationContext(invalidEdition), validationResults, validateAllProperties: true);

            // Assert
            Assert.IsTrue(validationResults.Count > 0);
        }
    }
}
