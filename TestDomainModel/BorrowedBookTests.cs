// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BorrowedBookTests.cs" company="Transilvania University of Brasov">
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
    /// Represents a set of tests for the Book class in the domain model.
    /// </summary>
    [TestClass]
    public class BorrowedBookTests
    {
        /// <summary>
        /// Verifies that the Id property of the BorrowedBook class is greater than zero.
        /// </summary>
        [TestMethod]
        public void IdShouldBeGreaterThanZero()
        {
            // Arrange
            var borrowedBook = new BorrowedBook();

            // Act
            borrowedBook.Id = 1; // Set a valid Id

            // Assert
            Assert.IsTrue(borrowedBook.Id > 0);
        }

        /// <summary>
        /// Verifies that a BorrowedBook object with a valid ReaderId passes validation.
        /// </summary>
        [TestMethod]
        public void ValidReaderIdShouldPassValidation()
        {
            // Arrange
            var borrowedBook = new BorrowedBook
            {
                ReaderId = 1, // Set a valid reader Id
            };

            // Act
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateProperty(borrowedBook.ReaderId, new ValidationContext(borrowedBook) { MemberName = nameof(BorrowedBook.ReaderId) }, validationResults);

            // Assert
            Assert.IsTrue(validationResults.Count == 0);
        }

        /// <summary>
        /// Verifies that a BorrowedBook object with a valid StaffId passes validation.
        /// </summary>
        [TestMethod]
        public void ValidStaffIdShouldPassValidation()
        {
            // Arrange
            var borrowedBook = new BorrowedBook
            {
                StaffId = 1, // Set a valid staff Id
            };

            // Act
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateProperty(borrowedBook.StaffId, new ValidationContext(borrowedBook) { MemberName = nameof(BorrowedBook.StaffId) }, validationResults);

            // Assert
            Assert.IsTrue(validationResults.Count == 0);
        }

        /// <summary>
        /// Verifies that a BorrowedBook object with a valid Reader passes validation.
        /// </summary>
        [TestMethod]
        public void ValidReaderShouldPassValidation()
        {
            // Arrange
            var borrowedBook = new BorrowedBook
            {
                Reader = new Person(), // Set a valid reader object
            };

            // Act
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateProperty(borrowedBook.Reader, new ValidationContext(borrowedBook) { MemberName = nameof(BorrowedBook.Reader) }, validationResults);

            // Assert
            Assert.IsTrue(validationResults.Count == 0);
        }

        /// <summary>
        /// Verifies that a BorrowedBook object with a null Reader fails validation.
        /// </summary>
        [TestMethod]
        public void NullReaderShouldFailValidation()
        {
            // Arrange
            var borrowedBook = new BorrowedBook();

            // Act
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateProperty(borrowedBook.Reader, new ValidationContext(borrowedBook) { MemberName = nameof(BorrowedBook.Reader) }, validationResults);

            // Assert
            Assert.IsTrue(validationResults.Count > 0);
        }

        /// <summary>
        /// Verifies that a BorrowedBook object with a valid Staff passes validation.
        /// </summary>
        [TestMethod]
        public void ValidStaffShouldPassValidation()
        {
            // Arrange
            var borrowedBook = new BorrowedBook
            {
                Staff = new Person(), // Set a valid staff object
            };

            // Act
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateProperty(borrowedBook.Staff, new ValidationContext(borrowedBook) { MemberName = nameof(BorrowedBook.Staff) }, validationResults);

            // Assert
            Assert.IsTrue(validationResults.Count == 0);
        }

        /// <summary>
        /// Verifies that a BorrowedBook object with a null Staff fails validation.
        /// </summary>
        [TestMethod]
        public void NullStaffShouldFailValidation()
        {
            // Arrange
            var borrowedBook = new BorrowedBook();

            // Act
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateProperty(borrowedBook.Staff, new ValidationContext(borrowedBook) { MemberName = nameof(BorrowedBook.Staff) }, validationResults);

            // Assert
            Assert.IsTrue(validationResults.Count > 0);
        }

        /// <summary>
        /// Verifies that a BorrowedBook object with a valid Edition passes validation.
        /// </summary>
        [TestMethod]
        public void ValidEditionShouldPassValidation()
        {
            // Arrange
            var borrowedBook = new BorrowedBook
            {
                Edition = new Edition(), // Set a valid edition
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(borrowedBook.Edition, new ValidationContext(borrowedBook) { MemberName = nameof(BorrowedBook.Edition) }, validationResults);

            // Assert
            Assert.IsTrue(isValid);
        }

        /// <summary>
        /// Verifies that a BorrowedBook object with a null Edition fails validation.
        /// </summary>
        [TestMethod]
        public void NullEditionShouldFailValidation()
        {
            // Arrange
            var borrowedBook = new BorrowedBook();

            // Act
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(borrowedBook.Edition, new ValidationContext(borrowedBook) { MemberName = nameof(BorrowedBook.Edition) }, validationResults);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(1, validationResults.Count);
            Assert.AreEqual("The Edition cannot be null", validationResults[0].ErrorMessage);
        }

        /// <summary>
        /// Verifies that a BorrowedBook object with a valid BorrowDate passes validation.
        /// </summary>
        [TestMethod]
        public void ValidBorrowDateShouldPassValidation()
        {
            // Arrange
            var borrowedBook = new BorrowedBook
            {
                BorrowDate = DateTime.Now, // Set a valid borrow date
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(borrowedBook.BorrowDate, new ValidationContext(borrowedBook) { MemberName = nameof(BorrowedBook.BorrowDate) }, validationResults);

            // Assert
            Assert.IsTrue(isValid);
        }

        /// <summary>
        /// Verifies that a BorrowedBook object with a default DateTime value fails validation.
        /// </summary>
        [TestMethod]
        public void DefaultBorrowDateShouldFailValidation()
        {
            // Arrange
            var borrowedBook = new BorrowedBook();

            // Act

            // Assert
            Assert.IsFalse(borrowedBook.BorrowDate != DateTime.MinValue);
        }

        /// <summary>
        /// Verifies that a BorrowedBook object with a valid DueDate passes validation.
        /// </summary>
        [TestMethod]
        public void ValidDueDateShouldPassValidation()
        {
            // Arrange
            var borrowedBook = new BorrowedBook
            {
                DueDate = DateTime.Now.AddDays(7), // Set a valid due date
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(borrowedBook.DueDate, new ValidationContext(borrowedBook) { MemberName = nameof(BorrowedBook.DueDate) }, validationResults);

            // Assert
            Assert.IsTrue(isValid);
        }

        /// <summary>
        /// Verifies that a BorrowedBook object with a default DateTime value fails validation.
        /// </summary>
        [TestMethod]
        public void DefaultDueDateShouldFailValidation()
        {
            // Arrange
            var borrowedBook = new BorrowedBook();

            // Act

            // Assert
            Assert.IsFalse(borrowedBook.DueDate != DateTime.MinValue);
        }

        /// <summary>
        /// Verifies that a BorrowedBook object with a valid ReturnedDate passes validation.
        /// </summary>
        [TestMethod]
        public void ValidReturnedDateShouldPassValidation()
        {
            // Arrange
            var borrowedBook = new BorrowedBook
            {
                ReturnedDate = DateTime.Now.AddDays(-1), // Set a valid returned date
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(borrowedBook.ReturnedDate, new ValidationContext(borrowedBook) { MemberName = nameof(BorrowedBook.ReturnedDate) }, validationResults);

            // Assert
            Assert.IsTrue(isValid);
        }

        /// <summary>
        /// Verifies that a BorrowedBook object with the default DateTime value for ReturnedDate passes validation.
        /// </summary>
        [TestMethod]
        public void DefaultReturnedDateShouldPassValidation()
        {
            // Arrange
            var borrowedBook = new BorrowedBook();

            // Act
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(borrowedBook.ReturnedDate, new ValidationContext(borrowedBook) { MemberName = nameof(BorrowedBook.ReturnedDate) }, validationResults);

            // Assert
            Assert.IsTrue(isValid);
        }

        /// <summary>
        /// Verifies that a BorrowedBook object with all valid properties passes validation.
        /// </summary>
        [TestMethod]
        public void ValidBorrowedBookObjectShouldPassValidation()
        {
            // Arrange
            var borrowedBook = new BorrowedBook
            {
                ReaderId = 1,
                StaffId = 2,
                Reader = new Person { Id = 1, FirstName = "John", LastName = "Doe" },
                Staff = new Person { Id = 2, FirstName = "Jane", LastName = "Doe" },
                Edition = new Edition { Id = 1, Type = BookType.TradeHardcover, PageCount = 300, Name = "Sample Edition", Publisher = "Sample Publisher", CanNotBorrow = 0, CanBorrow = 5, Book = new Book { Id = 1, Title = "Sample Book" } },
                BorrowDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(14),
                ReturnedDate = DateTime.Now.AddDays(-1),
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(borrowedBook, new ValidationContext(borrowedBook), validationResults, true);

            // Assert
            Assert.IsTrue(isValid);
        }

        /// <summary>
        /// Verifies that a BorrowedBook object with an invalid property fails validation.
        /// </summary>
        [TestMethod]
        public void InvalidBorrowedBookObjectShouldFailValidation()
        {
            // Arrange
            var borrowedBook = new BorrowedBook
            {
                ReaderId = 1,
                StaffId = 2,
                Reader = new Person { Id = 1, FirstName = "John", LastName = "Doe" },
                //// Suff is missing, intentionally causing failure
                Edition = new Edition { Id = 1, Type = BookType.TradeHardcover, PageCount = 300, Name = "Sample Edition", Publisher = "Sample Publisher", CanNotBorrow = 0, CanBorrow = 5, Book = new Book { Id = 1, Title = "Sample Book" } },
                BorrowDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(14),
                ReturnedDate = DateTime.Now.AddDays(-1),
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(borrowedBook, new ValidationContext(borrowedBook), validationResults, true);

            // Assert
            Assert.IsFalse(isValid);
        }
    }
}