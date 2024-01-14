// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BorrowedBookServiceTests.cs" company="Transilvania University of Brasov">
//   Copyright (c) Dogaru Alexandru.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TestServiceLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Configuration;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Runtime.Remoting.Contexts;
    using DataMapper;
    using DomainModel;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Rhino.Mocks;
    using Rhino.Mocks.Constraints;
    using ServiceLayer.ServiceImplementation;
    using static log4net.Appender.RollingFileAppender;

    /// <summary>
    /// Represents unit tests for the <see cref="BorrowedBookServicesImplementation"/> class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class BorrowedBookServiceTests
    {
        /// <summary>
        /// Represents the mock repository used for creating mock objects during unit tests.
        /// </summary>
        private MockRepository mocks;

        /// <summary>
        /// Represents the mock service for accessing borrowedBook-related data during unit tests.
        /// </summary>
        private IBorrowedBookDataService borrowedBookDataService;

        /// <summary>
        /// Represents a collection of borrowedBooks in the application.
        /// </summary>
        private List<BorrowedBook> borrowedBooks;

        /// <summary>
        /// Represents a book in the application.
        /// </summary>
        private Book book;

        /// <summary>
        /// Represents a reader person in the application.
        /// </summary>
        private Person reader;

        /// <summary>
        /// Represents a stuff person in the application.
        /// </summary>
        private Person stuff;

        /// <summary>
        /// Represents an edition in the application.
        /// </summary>
        private Edition edition;

        /// <summary>
        /// Initializes test resources before each test method is executed.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            this.mocks = new MockRepository();
            this.borrowedBookDataService = this.mocks.StrictMock<IBorrowedBookDataService>();
            this.book = new Book { Id = 1, Title = "title2", BookDomains = new List<BookDomain> { new BookDomain { Id = 1, Name = "name1" } } };
            this.reader = new Person { Id = 0, FirstName = "FirstName1", LastName = "LastName1", CNP = "2491738465248", Type = PersonType.Reader, Address = "asdadsasdasdasd", EmailAddress = "asd1@mail.com" };
            this.stuff = new Person { Id = 2, FirstName = "FirstName3", LastName = "LastName3", CNP = "5010474094949", Type = PersonType.LibraryPersonnel, Address = "asdad sasdasdasd", EmailAddress = "asdasda1@mail.com" };
            this.edition = new Edition { Id = 0, Name = "name1", Book = this.book, Publisher = "asdasd", Type = BookType.LibraryBinding, PageCount = 123, CanBorrow = 23, CanNotBorrow = 23 };
            this.borrowedBooks = new List<BorrowedBook>
            {
                new BorrowedBook { Id = 0, Reader = this.reader, ReaderId = this.reader.Id, Staff = this.stuff, StaffId = this.stuff.Id, Edition = this.edition, BorrowDate = DateTime.Today, DueDate = DateTime.Today.AddDays(10) },
                new BorrowedBook { Id = 1, Reader = this.reader, ReaderId = this.reader.Id, Staff = this.stuff, StaffId = this.stuff.Id, Edition = this.edition, BorrowDate = DateTime.Today.AddDays(-10), DueDate = DateTime.Today, ReturnedDate = DateTime.Today },
                new BorrowedBook { Id = 2, Reader = this.reader, ReaderId = this.reader.Id, Staff = this.stuff, StaffId = this.stuff.Id, Edition = this.edition, BorrowDate = DateTime.Today.AddDays(-5), DueDate = DateTime.Today.AddDays(5) },
            };
        }

        /// <summary>
        /// Tests the <see cref="BorrowedBookServicesImplementation.GetAllBorrowedBooks"/> method when there are items.
        /// </summary>
        [TestMethod]
        public void TestGetAllBorrowedBooksHasItems()
        {
            using (this.mocks.Record())
            {
                Expect.Call(this.borrowedBookDataService.GetAllBorrowedBooks()).Return(this.borrowedBooks);
            }

            using (this.mocks.Playback())
            {
                // Arrange
                BorrowedBookServicesImplementation servicesImplementation = new BorrowedBookServicesImplementation(this.borrowedBookDataService);

                // Act
                var list = servicesImplementation.GetAllBorrowedBooks();

                // Assert
                Assert.AreEqual(list[0].ReturnedDate, null);
                Assert.AreNotEqual(list[1].ReturnedDate, null);
                Assert.AreEqual(list.Count, 3);
            }
        }

        /// <summary>
        /// Tests the <see cref="BorrowedBookServicesImplementation.GetAllBorrowedBooks"/> method when there are no items.
        /// </summary>
        [TestMethod]
        public void TestGetAllBorrowedBooksHasNoItems()
        {
            using (this.mocks.Record())
            {
                Expect.Call(this.borrowedBookDataService.GetAllBorrowedBooks()).Return(this.borrowedBooks);
            }

            using (this.mocks.Playback())
            {
                // Arrange
                BorrowedBookServicesImplementation servicesImplementation = new BorrowedBookServicesImplementation(this.borrowedBookDataService);
                this.borrowedBooks.Clear();

                // Act
                var list = servicesImplementation.GetAllBorrowedBooks();

                // Assert
                Assert.AreEqual(list.Count, 0);
            }
        }

        /// <summary>
        /// Tests the <see cref="BorrowedBookServicesImplementation.GetBorrowedBookById"/> method when the borrowedbook exists.
        /// </summary>
        [TestMethod]
        public void TestGetBorrowedBookByIdBorrowedBookExists()
        {
            using (this.mocks.Record())
            {
                Expect.Call(() => this.borrowedBookDataService.GetBorrowedBookById(Arg<int>.Is.Anything)).WhenCalled(call =>
                {
                    int idParameter = (int)call.Arguments[0];
                    call.ReturnValue = this.borrowedBooks.FirstOrDefault(borrowedbook => borrowedbook.Id == idParameter);
                });
            }

            using (this.mocks.Playback())
            {
                // Arrange
                BorrowedBookServicesImplementation servicesImplementation = new BorrowedBookServicesImplementation(this.borrowedBookDataService);
                int existingBorrowedBookId = 1;

                // Act
                BorrowedBook result = servicesImplementation.GetBorrowedBookById(existingBorrowedBookId);

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(result.Id, existingBorrowedBookId);
            }
        }

        /// <summary>
        /// Tests the <see cref="BorrowedBookServicesImplementation.GetBorrowedBookById"/> method when the borrowedbook does not exist anymore.
        /// </summary>
        [TestMethod]
        public void TestGetBorrowedBookByIdBorrowedBookDoesNotExistAnymore()
        {
            using (this.mocks.Record())
            {
                Expect.Call(() => this.borrowedBookDataService.GetBorrowedBookById(Arg<int>.Is.Anything)).WhenCalled(call =>
                {
                    int idParameter = (int)call.Arguments[0];
                    call.ReturnValue = this.borrowedBooks.FirstOrDefault(borrowedbook => borrowedbook.Id == idParameter);
                });
            }

            using (this.mocks.Playback())
            {
                // Arrange
                BorrowedBookServicesImplementation servicesImplementation = new BorrowedBookServicesImplementation(this.borrowedBookDataService);
                int existingBorrowedBookId = 1;
                this.borrowedBooks.Clear();

                // Act
                BorrowedBook result = servicesImplementation.GetBorrowedBookById(existingBorrowedBookId);

                // Assert
                Assert.IsNull(result);
            }
        }

        /// <summary>
        /// Tests the <see cref="BorrowedBookServicesImplementation.GetBorrowedBookById"/> method when the borrowedbook does not exist.
        /// </summary>
        [TestMethod]
        public void TestGetBorrowedBookByIdBorrowedBookDoesNotExist()
        {
            using (this.mocks.Record())
            {
                Expect.Call(() => this.borrowedBookDataService.GetBorrowedBookById(Arg<int>.Is.Anything)).WhenCalled(call =>
                {
                    int idParameter = (int)call.Arguments[0];
                    call.ReturnValue = this.borrowedBooks.FirstOrDefault(borrowedbook => borrowedbook.Id == idParameter);
                });
            }

            using (this.mocks.Playback())
            {
                // Arrange
                BorrowedBookServicesImplementation servicesImplementation = new BorrowedBookServicesImplementation(this.borrowedBookDataService);
                int nonExistingBorrowedBookId = 99;

                // Act
                BorrowedBook result = servicesImplementation.GetBorrowedBookById(nonExistingBorrowedBookId);

                // Assert
                Assert.IsNull(result);
            }
        }

        /// <summary>
        /// Tests the <see cref="BorrowedBookServicesImplementation.DeleteBorrowedBook"/> method when the borrowedBook exists.
        /// </summary>
        [TestMethod]
        public void TestDeleteBorrowedBookBorrowedBookExists()
        {
            using (this.mocks.Record())
            {
                Expect.Call(() => this.borrowedBookDataService.DeleteBorrowedBook(Arg<BorrowedBook>.Is.Anything)).WhenCalled(call =>
                {
                    BorrowedBook borrowedbookParameter = (BorrowedBook)call.Arguments[0];
                    int index = this.borrowedBooks.FindIndex(a => a.Id == borrowedbookParameter.Id);
                    if (index == -1)
                    {
                        throw new Exception();
                    }

                    this.borrowedBooks.RemoveAt(index);
                });
            }

            using (this.mocks.Playback())
            {
                // Arrange
                BorrowedBookServicesImplementation servicesImplementation = new BorrowedBookServicesImplementation(this.borrowedBookDataService);
                BorrowedBook borrowedbookToDelete = this.borrowedBooks.First(); // Select the first borrowedBook for deletion

                // Act
                servicesImplementation.DeleteBorrowedBook(borrowedbookToDelete);

                // Assert
                Assert.AreEqual(this.borrowedBooks.Count, 2); // BorrowedBook should be removed
                Assert.IsFalse(this.borrowedBooks.Contains(borrowedbookToDelete));
            }
        }

        /// <summary>
        /// Tests the <see cref="BorrowedBookServicesImplementation.DeleteBorrowedBook"/> method when the borrowedBook does not exist.
        /// </summary>
        [TestMethod]
        public void TestDeleteBorrowedBookBorrowedBookDoesNotExist()
        {
            using (this.mocks.Record())
            {
                Expect.Call(() => this.borrowedBookDataService.DeleteBorrowedBook(Arg<BorrowedBook>.Is.Anything)).WhenCalled(call =>
                {
                    BorrowedBook borrowedbookParameter = (BorrowedBook)call.Arguments[0];
                    int index = this.borrowedBooks.FindIndex(a => a.Id == borrowedbookParameter.Id);
                    if (index == -1)
                    {
                        throw new Exception();
                    }

                    this.borrowedBooks.RemoveAt(index);
                });
            }

            using (this.mocks.Playback())
            {
                // Arrange
                BorrowedBookServicesImplementation servicesImplementation = new BorrowedBookServicesImplementation(this.borrowedBookDataService);
                BorrowedBook nonExistingBorrowedBook = new BorrowedBook { Id = 99, Reader = this.reader, ReaderId = this.reader.Id, Staff = this.stuff, StaffId = this.stuff.Id, Edition = this.edition, BorrowDate = DateTime.Today, DueDate = DateTime.Today.AddDays(10) };

                // Assert and Act
                Assert.ThrowsException<Exception>(() => servicesImplementation.DeleteBorrowedBook(nonExistingBorrowedBook));
            }
        }

        /// <summary>
        /// Test method to verify the behavior of CanBorrowMoreBooks in BorrowedBookServicesImplementation.
        /// </summary>
        [TestMethod]
        public void TestCanBorrowMoreBooks()
        {
            // Create a dynamic mock for IBorrowedBookDataService
            var borrowedBookDataServiceMock = MockRepository.GenerateMock<IBorrowedBookDataService>();

            // Setup the expectation for CountBorrowedBooksByEditionWithNullReturnedDate
            borrowedBookDataServiceMock.Stub(x => x.CountBorrowedBooksByEditionWithNullReturnedDate(Arg<Edition>.Is.Anything)).Return(-1)
                .WhenCalled(call =>
                {
                    Edition edition = (Edition)call.Arguments[0];
                    call.ReturnValue = this.borrowedBooks.Count(b => b.Edition.Id == edition.Id && b.ReturnedDate == null);
                });

            // Arrange
            BorrowedBookServicesImplementation servicesImplementation = new BorrowedBookServicesImplementation(borrowedBookDataServiceMock);

            // Act
            bool result = servicesImplementation.CanBorrowMoreBooks(this.edition);

            // Assert
            Assert.AreEqual(result, true);
        }

        /// <summary>
        /// Test method to verify the behavior of CountBorrowedSinceDateForPerson in BorrowedBookServicesImplementation.
        /// </summary>
        [TestMethod]
        public void TestCountBorrowedSinceDateForPerson()
        {
            // Create a dynamic stub for IBorrowedBookDataService
            var borrowedBookDataServiceStub = MockRepository.GenerateStub<IBorrowedBookDataService>();

            // Setup the behavior for CountBorrowedBooksByPersonAndDate using WhenCalled
            borrowedBookDataServiceStub.Stub(x => x.CountBorrowedBooksByPersonAndDate(Arg<Person>.Is.Anything, Arg<DateTime>.Is.Anything)).Return(-1)
                .WhenCalled(call =>
                {
                    Person person = (Person)call.Arguments[0];
                    DateTime date = (DateTime)call.Arguments[1];
                    call.ReturnValue = this.borrowedBooks.Count(b => b.Reader.Id == person.Id && b.BorrowDate > date);
                })
                .Repeat.Any();

            // Arrange
            BorrowedBookServicesImplementation servicesImplementation = new BorrowedBookServicesImplementation(borrowedBookDataServiceStub);

            // Act
            int result = servicesImplementation.CountBorrowedSinceDateForPerson(this.reader, DateTime.Today);

            // Assert
            Assert.AreEqual(result, 0);
        }

        /// <summary>
        /// Test method to verify the behavior of CountBorrowedSinceDateForPerson in BorrowedBookServicesImplementation.
        /// </summary>
        [TestMethod]
        public void TestCountBorrowedBooksForPersonAndDomainAfterDate()
        {
            // Create a dynamic mock for IBorrowedBookDataService
            var borrowedBookDataServiceMock = MockRepository.GenerateMock<IBorrowedBookDataService>();

            // Setup the expectation for CountBorrowedBooksForPersonAndDomainAfterDate
            borrowedBookDataServiceMock.Stub(x => x.CountBorrowedBooksForPersonAndDomainAfterDate(
                Arg<Person>.Is.Anything,
                Arg<BookDomain>.Is.Anything,
                Arg<DateTime>.Is.Anything)).Return(-1)
                .WhenCalled(call =>
                {
                    Person person = (Person)call.Arguments[0];
                    BookDomain bookDomain = (BookDomain)call.Arguments[1];
                    DateTime date = (DateTime)call.Arguments[2];
                    call.ReturnValue = this.borrowedBooks.Count(b =>
                        b.Edition.Book.BookDomains.Any(bd => bd.Id == bookDomain.Id)
                        && b.BorrowDate > date
                        && b.Reader.Id == person.Id);
                })
                .Repeat.Any();

            // Arrange
            BorrowedBookServicesImplementation servicesImplementation = new BorrowedBookServicesImplementation(borrowedBookDataServiceMock);

            // Act
            int result = servicesImplementation.CountBorrowedBooksForPersonAndDomainAfterDate(this.reader, this.book.BookDomains.First(), DateTime.Today);

            // Assert
            Assert.AreEqual(result, 0);
        }

        /// <summary>
        /// Test method to verify the behavior of GetDueDateDifferencesForPersonAfterDate in BorrowedBookServicesImplementation.
        /// </summary>
        [TestMethod]
        public void TestGetDueDateDifferencesForPersonAfterDate()
        {
            using (this.mocks.Record())
            {
                Expect.Call(() => this.borrowedBookDataService.GetDueDateDifferencesForPersonAfterDate(Arg<Person>.Is.Anything, Arg<DateTime>.Is.Anything)).WhenCalled(call =>
                {
                    Person person = (Person)call.Arguments[0];
                    DateTime date = (DateTime)call.Arguments[1];
                    call.ReturnValue = this.borrowedBooks
                        .Where(b => b.Reader.Id == person.Id && b.BorrowDate > date)
                        .Select(b => (b.DueDate - b.BorrowDate).Days)
                        .ToList();
                }).Repeat.Any();
            }

            using (this.mocks.Playback())
            {
                // Arrange
                BorrowedBookServicesImplementation servicesImplementation = new BorrowedBookServicesImplementation(this.borrowedBookDataService);

                // Act
                int result = servicesImplementation.GetDueDateDifferencesForPersonAfterDate(this.reader, DateTime.Today.AddDays(-15)).Sum();

                // Assert
                Assert.AreEqual(30, result);
            }
        }

        /// <summary>
        /// Test method to verify the behavior of TestCountBorrowedBooksByEditionForPersonAfterDate in BorrowedBookServicesImplementation.
        /// </summary>
        [TestMethod]
        public void TestCountBorrowedBooksByEditionForPersonAfterDate()
        {
            // Create a dynamic mock for IBorrowedBookDataService
            var borrowedBookDataServiceMock = MockRepository.GenerateMock<IBorrowedBookDataService>();

            // Setup the expectation for CountBorrowedBooksByEditionForPersonAfterDate
            borrowedBookDataServiceMock.Stub(x => x.CountBorrowedBooksByEditionForPersonAfterDate(
                Arg<Person>.Is.Anything,
                Arg<Edition>.Is.Anything,
                Arg<DateTime>.Is.Anything)).Return(-1)
                .WhenCalled(call =>
                {
                    Person person = (Person)call.Arguments[0];
                    Edition edition = (Edition)call.Arguments[1];
                    DateTime date = (DateTime)call.Arguments[2];
                    call.ReturnValue = this.borrowedBooks.Count(b => b.Reader.Id == person.Id && b.Edition.Id == edition.Id && b.BorrowDate > date);
                });

            // Arrange
            BorrowedBookServicesImplementation servicesImplementation = new BorrowedBookServicesImplementation(borrowedBookDataServiceMock);

            // Act
            int result = servicesImplementation.CountBorrowedBooksByEditionForPersonAfterDate(this.reader, this.edition, DateTime.Today.AddDays(-11));

            // Assert
            Assert.AreEqual(3, result);
        }

        /// <summary>
        /// Test method to verify the behavior of CountBooksBorrowedByPersonOnDate in BorrowedBookServicesImplementation.
        /// </summary>
        [TestMethod]
        public void TestCountBooksBorrowedByPersonOnDate()
        {
            // Create a dynamic mock for IBorrowedBookDataService
            var borrowedBookDataServiceMock = MockRepository.GenerateMock<IBorrowedBookDataService>();

            // Setup the expectation for CountBooksBorrowedByPersonOnDate
            borrowedBookDataServiceMock.Stub(x => x.CountBooksBorrowedByPersonOnDate(
                Arg<Person>.Is.Anything,
                Arg<DateTime>.Is.Anything)).Return(-1)
                .WhenCalled(call =>
                {
                    Person person = (Person)call.Arguments[0];
                    DateTime date = (DateTime)call.Arguments[1];
                    call.ReturnValue = this.borrowedBooks.Count(b => b.Reader.Id == person.Id && b.BorrowDate.Date == date.Date);
                })
                .Repeat.Any();

            // Arrange
            BorrowedBookServicesImplementation servicesImplementation = new BorrowedBookServicesImplementation(borrowedBookDataServiceMock);

            // Act
            int result = servicesImplementation.CountBooksBorrowedByPersonOnDate(this.reader, DateTime.Today);

            // Assert
            Assert.AreEqual(1, result);
        }

        /// <summary>
        /// Test method to verify the behavior of CountBooksBorrowedBySuffOnDate in BorrowedBookServicesImplementation.
        /// </summary>
        [TestMethod]
        public void TestCountBooksBorrowedBySuffOnDate()
        {
            // Create a dynamic mock for IBorrowedBookDataService
            var borrowedBookDataServiceMock = MockRepository.GenerateMock<IBorrowedBookDataService>();

            // Setup the expectation for CountBooksBorrowedBySuffOnDate
            borrowedBookDataServiceMock.Stub(x => x.CountBooksBorrowedBySuffOnDate(
                Arg<Person>.Is.Anything,
                Arg<DateTime>.Is.Anything)).Return(-1)
                .WhenCalled(call =>
                {
                    Person person = (Person)call.Arguments[0];
                    DateTime date = (DateTime)call.Arguments[1];
                    call.ReturnValue = this.borrowedBooks.Count(b => b.Staff.Id == person.Id && b.BorrowDate.Date == date.Date);
                })
                .Repeat.Any();

            // Arrange
            BorrowedBookServicesImplementation servicesImplementation = new BorrowedBookServicesImplementation(borrowedBookDataServiceMock);

            // Act
            int result = servicesImplementation.CountBooksBorrowedBySuffOnDate(this.stuff, DateTime.Today);

            // Assert
            Assert.AreEqual(1, result);
        }

        /// <summary>
        /// Verifies that a person cannot borrow more than a specified number of books at once.
        /// </summary>
        [TestMethod]
        public void TestVerifyTooManyBorrowedAtOnce()
        {
            // Arrange
            BorrowedBookServicesImplementation servicesImplementation = new BorrowedBookServicesImplementation(this.borrowedBookDataService);
            List<BorrowedBook> multipleBorrowedBooks = new List<BorrowedBook>
            {
                new BorrowedBook { Id = 10, Reader = this.reader, ReaderId = this.reader.Id, Staff = this.reader, StaffId = this.reader.Id, Edition = this.edition, BorrowDate = DateTime.Today, DueDate = DateTime.Today.AddDays(10) },
                new BorrowedBook { Id = 11, Reader = this.reader, ReaderId = this.reader.Id, Staff = this.reader, StaffId = this.reader.Id, Edition = this.edition, BorrowDate = DateTime.Today, DueDate = DateTime.Today.AddDays(10) },
                new BorrowedBook { Id = 12, Reader = this.reader, ReaderId = this.reader.Id, Staff = this.reader, StaffId = this.reader.Id, Edition = this.edition, BorrowDate = DateTime.Today, DueDate = DateTime.Today.AddDays(10) },
                new BorrowedBook { Id = 13, Reader = this.reader, ReaderId = this.reader.Id, Staff = this.reader, StaffId = this.reader.Id, Edition = this.edition, BorrowDate = DateTime.Today, DueDate = DateTime.Today.AddDays(10) },
            };

            // Act and Assert
            Assert.ThrowsException<ValidationException>(() => servicesImplementation.BorrowMultipleBook(multipleBorrowedBooks), "Can borrow a maximum of 3 books at once");
        }

        /// <summary>
        /// Verifies if there are enough distinct domains when borrowing multiple books.
        /// </summary>
        [TestMethod]
        public void TestVerifyDistinctCategories()
        {
            // Arrange
            BorrowedBookServicesImplementation servicesImplementation = new BorrowedBookServicesImplementation(this.borrowedBookDataService);

            // Act and Assert
            Assert.ThrowsException<ValidationException>(() => servicesImplementation.BorrowMultipleBook(this.borrowedBooks), "Not enough distinct domains");
        }

        /// <summary>
        /// Verifies independent validation for borrowing multiple books.
        /// </summary>
        [TestMethod]
        public void TestVerifyIndependentValidation()
        {
            // Arrange
            BorrowedBookServicesImplementation servicesImplementation = new BorrowedBookServicesImplementation(this.borrowedBookDataService);
            Book book1 = new Book { Title = "Title1", Authors = this.book.Authors, BookDomains = new List<BookDomain> { new BookDomain { Id = 2, Name = "asd" } } };
            Edition edition1 = new Edition { Name = "Name1", Book = book1, Type = BookType.MassMarketHardcover, PageCount = 123, CanBorrow = 20, CanNotBorrow = 20, Publisher = "pub" };
            Book book2 = new Book { Title = "Title2", Authors = this.book.Authors, BookDomains = new List<BookDomain> { new BookDomain { Id = 3, Name = "asdasd" } } };
            Edition edition2 = new Edition { Name = "Name2", Book = book2, Type = BookType.MassMarketHardcover, PageCount = 123, CanBorrow = 20, CanNotBorrow = 20, Publisher = "pub" };
            List<BorrowedBook> multipleBorrowedBooks = new List<BorrowedBook>
            {
                new BorrowedBook { Id = 11, Reader = this.reader, ReaderId = this.reader.Id, Staff = this.reader, StaffId = this.reader.Id, Edition = edition2, BorrowDate = DateTime.Today, DueDate = DateTime.Today.AddDays(10) },
                new BorrowedBook { Id = 12, Reader = this.reader, ReaderId = this.reader.Id, Staff = this.stuff, StaffId = this.stuff.Id, Edition = edition1, BorrowDate = DateTime.Today, DueDate = DateTime.Today.AddDays(10) },
                new BorrowedBook { Id = 12, Reader = this.reader, ReaderId = this.reader.Id, Staff = this.stuff, StaffId = this.stuff.Id, Edition = this.edition, BorrowDate = DateTime.Today, DueDate = DateTime.Today.AddDays(10) },
            };

            // Act and Assert
            Assert.ThrowsException<ValidationException>(() => servicesImplementation.BorrowMultipleBook(multipleBorrowedBooks), "Validation failed at index 3; A reader cannot borrow a book to another reader");
        }

        /// <summary>
        /// Verifies successful borrowing of multiple books by a reader. Utilizes Rhino Mocks to set up expected calls for counting borrowed books by edition, person, and domain. Ensures correct behavior of BorrowedBookServicesImplementation during the borrowing operation.
        /// </summary>
        [TestMethod]
        public void TestBorrowMultipleBookSucceses()
        {
            using (this.mocks.Record())
            {
                Expect.Call(this.borrowedBookDataService.CountBorrowedBooksByEditionWithNullReturnedDate(Arg<Edition>.Is.Anything)).Return(2);
                Expect.Call(this.borrowedBookDataService.CountBorrowedBooksByPersonAndDate(Arg<Person>.Is.Anything, Arg<DateTime>.Is.Anything)).Return(2);
                Expect.Call(this.borrowedBookDataService.CountBorrowedBooksForPersonAndDomainAfterDate(Arg<Person>.Is.Anything, Arg<BookDomain>.Is.Anything, Arg<DateTime>.Is.Anything)).Return(2);
                Expect.Call(this.borrowedBookDataService.GetDueDateDifferencesForPersonAfterDate(Arg<Person>.Is.Anything, Arg<DateTime>.Is.Anything)).Return(new List<int> { 7, 14 });
                Expect.Call(this.borrowedBookDataService.CountBorrowedBooksByEditionForPersonAfterDate(Arg<Person>.Is.Anything, Arg<Edition>.Is.Anything, Arg<DateTime>.Is.Anything)).Return(0);
                Expect.Call(this.borrowedBookDataService.CountBooksBorrowedByPersonOnDate(Arg<Person>.Is.Anything, Arg<DateTime>.Is.Anything)).Return(3);
                Expect.Call(this.borrowedBookDataService.CountBooksBorrowedBySuffOnDate(Arg<Person>.Is.Anything, Arg<DateTime>.Is.Anything)).Return(5);
                Expect.Call(() => this.borrowedBookDataService.AddBorrowedBook(Arg<BorrowedBook>.Is.Anything)).WhenCalled(call =>
                {
                    BorrowedBook borrowedBook = (BorrowedBook)call.Arguments[0];
                    this.borrowedBooks.Add(borrowedBook);
                }).Repeat.Any();
                Expect.Call(() => this.borrowedBookDataService.DeleteBorrowedBook(Arg<BorrowedBook>.Is.Anything)).WhenCalled(call =>
                {
                    BorrowedBook borrowedbookParameter = (BorrowedBook)call.Arguments[0];
                    int index = this.borrowedBooks.FindIndex(a => a.Id == borrowedbookParameter.Id);
                    if (index == -1)
                    {
                        throw new Exception();
                    }

                    this.borrowedBooks.RemoveAt(index);
                }).Repeat.Any();
            }

            using (this.mocks.Playback())
            {
                // Arrange
                BorrowedBookServicesImplementation servicesImplementation = new BorrowedBookServicesImplementation(this.borrowedBookDataService);
                Book book1 = new Book { Title = "Title1", Authors = this.book.Authors, BookDomains = new List<BookDomain> { new BookDomain { Id = 2, Name = "asd" } } };
                Edition edition1 = new Edition { Name = "Name1", Book = book1, Type = BookType.MassMarketHardcover, PageCount = 123, CanBorrow = 20, CanNotBorrow = 20, Publisher = "pub" };
                Book book2 = new Book { Title = "Title2", Authors = this.book.Authors, BookDomains = new List<BookDomain> { new BookDomain { Id = 3, Name = "asdasd" } } };
                Edition edition2 = new Edition { Name = "Name2", Book = book2, Type = BookType.MassMarketHardcover, PageCount = 123, CanBorrow = 20, CanNotBorrow = 20, Publisher = "pub" };
                List<BorrowedBook> multipleBorrowedBooks = new List<BorrowedBook>
                {
                    new BorrowedBook { Id = 11, Reader = this.reader, ReaderId = this.reader.Id, Staff = this.stuff, StaffId = this.stuff.Id, Edition = edition2, BorrowDate = DateTime.Today, DueDate = DateTime.Today.AddDays(10) },
                    new BorrowedBook { Id = 12, Reader = this.reader, ReaderId = this.reader.Id, Staff = this.stuff, StaffId = this.stuff.Id, Edition = edition1, BorrowDate = DateTime.Today, DueDate = DateTime.Today.AddDays(10) },
                };

                // Act
                servicesImplementation.BorrowMultipleBook(multipleBorrowedBooks);

                // Assert
                Assert.AreEqual(this.borrowedBooks.Count, 5);
                Assert.AreEqual(this.borrowedBooks[3].Id, 11);
            }
        }

        /// <summary>
        /// Verifies that a borrowed book cannot be added when the staff member is of type reader, as a reader cannot borrow a book to another reader.
        /// </summary>
        [TestMethod]
        public void TestVerifyStuffPersonIsTypeStuff_OnAdd()
        {
            // Arrange
            BorrowedBookServicesImplementation servicesImplementation = new BorrowedBookServicesImplementation(this.borrowedBookDataService);
            BorrowedBook borrowedBook = new BorrowedBook { Id = 10, Reader = this.reader, ReaderId = this.reader.Id, Staff = this.reader, StaffId = this.reader.Id, Edition = this.edition, BorrowDate = DateTime.Today, DueDate = DateTime.Today.AddDays(10) };

            // Act and Assert
            Assert.ThrowsException<ValidationException>(() => servicesImplementation.AddBorrowedBook(borrowedBook), "A reader cannot borrow a book to another reader");
        }

        /// <summary>
        /// Verifies that the BorrowedDate cannot be set later than the DueDate when adding a borrowed book.
        /// </summary>
        [TestMethod]
        public void TestVerifyDates_OnAdd_BorrowedDateCanNotBeAfterDueDate()
        {
            // Arrange
            BorrowedBookServicesImplementation servicesImplementation = new BorrowedBookServicesImplementation(this.borrowedBookDataService);
            BorrowedBook borrowedBook = new BorrowedBook { Id = 10, Reader = this.reader, ReaderId = this.reader.Id, Staff = this.stuff, StaffId = this.stuff.Id, Edition = this.edition, BorrowDate = DateTime.Today, DueDate = DateTime.Today.AddDays(-10) };

            // Act and Assert
            Assert.ThrowsException<ValidationException>(() => servicesImplementation.AddBorrowedBook(borrowedBook), "The BorrowDate cannot be later than the DueDate");
        }

        /// <summary>
        /// Verifies that the BorrowedDate cannot be set later than the ReturnedDate when adding a borrowed book.
        /// </summary>
        [TestMethod]
        public void TestVerifyDates_OnAdd_BorrowedDateCanNotBeAfterReturnedDate()
        {
            // Arrange
            BorrowedBookServicesImplementation servicesImplementation = new BorrowedBookServicesImplementation(this.borrowedBookDataService);
            BorrowedBook borrowedBook = new BorrowedBook { Id = 10, Reader = this.reader, ReaderId = this.reader.Id, Staff = this.stuff, StaffId = this.stuff.Id, Edition = this.edition, BorrowDate = DateTime.Today, DueDate = DateTime.Today.AddDays(10), ReturnedDate = DateTime.Today.AddDays(-10) };

            // Act and Assert
            Assert.ThrowsException<ValidationException>(() => servicesImplementation.AddBorrowedBook(borrowedBook), "The BorrowDate cannot be later than the ReturnedDate");
        }

        /// <summary>
        /// Verifies that a new borrowed book cannot be added when the maximum number of allowed borrowed books for a specific edition has been reached.
        /// </summary>
        [TestMethod]
        public void TestVerifyCanBorrowMoreBooks_OnAdd()
        {
            using (this.mocks.Record())
            {
                Expect.Call(this.borrowedBookDataService.CountBorrowedBooksByEditionWithNullReturnedDate(Arg<Edition>.Is.Anything)).Return(20);
            }

            using (this.mocks.Playback())
            {
                // Arrange
                BorrowedBookServicesImplementation servicesImplementation = new BorrowedBookServicesImplementation(this.borrowedBookDataService);
                BorrowedBook borrowedBook = new BorrowedBook { Id = 10, Reader = this.reader, ReaderId = this.reader.Id, Staff = this.stuff, StaffId = this.stuff.Id, Edition = this.edition, BorrowDate = DateTime.Today, DueDate = DateTime.Today.AddDays(10) };

                // Act and Assert
                Assert.ThrowsException<ValidationException>(() => servicesImplementation.AddBorrowedBook(borrowedBook), "There are not enough books to be borrowed");
            }
        }

        /// <summary>
        /// Verifies that a new borrowed book cannot be added when the maximum number of allowed borrowed books for a person in a specific interval has been reached.
        /// </summary>
        [TestMethod]
        public void TestVerifyLimitForPersonReached_OnAdd()
        {
            using (this.mocks.Record())
            {
                Expect.Call(this.borrowedBookDataService.CountBorrowedBooksByEditionWithNullReturnedDate(Arg<Edition>.Is.Anything)).Return(2);
                Expect.Call(this.borrowedBookDataService.CountBorrowedBooksByPersonAndDate(Arg<Person>.Is.Anything, Arg<DateTime>.Is.Anything)).Return(15);
            }

            using (this.mocks.Playback())
            {
                // Arrange
                BorrowedBookServicesImplementation servicesImplementation = new BorrowedBookServicesImplementation(this.borrowedBookDataService);
                BorrowedBook borrowedBook = new BorrowedBook { Id = 10, Reader = this.reader, ReaderId = this.reader.Id, Staff = this.stuff, StaffId = this.stuff.Id, Edition = this.edition, BorrowDate = DateTime.Today, DueDate = DateTime.Today.AddDays(10) };

                // Act and Assert
                Assert.ThrowsException<ValidationException>(() => servicesImplementation.AddBorrowedBook(borrowedBook), "The borrow limit in the interval has been reached");
            }
        }

        /// <summary>
        /// Verifies that a new borrowed book cannot be added when the maximum number of allowed borrowed books for a person within a specific book domain and interval has been reached.
        /// </summary>
        [TestMethod]
        public void TestVerifyDomainLimitForPersonReached_OnAdd()
        {
            using (this.mocks.Record())
            {
                Expect.Call(this.borrowedBookDataService.CountBorrowedBooksByEditionWithNullReturnedDate(Arg<Edition>.Is.Anything)).Return(2);
                Expect.Call(this.borrowedBookDataService.CountBorrowedBooksByPersonAndDate(Arg<Person>.Is.Anything, Arg<DateTime>.Is.Anything)).Return(2);
                Expect.Call(this.borrowedBookDataService.CountBorrowedBooksForPersonAndDomainAfterDate(Arg<Person>.Is.Anything, Arg<BookDomain>.Is.Anything, Arg<DateTime>.Is.Anything)).Return(20);
            }

            using (this.mocks.Playback())
            {
                // Arrange
                BorrowedBookServicesImplementation servicesImplementation = new BorrowedBookServicesImplementation(this.borrowedBookDataService);
                BorrowedBook borrowedBook = new BorrowedBook { Id = 10, Reader = this.reader, ReaderId = this.reader.Id, Staff = this.stuff, StaffId = this.stuff.Id, Edition = this.edition, BorrowDate = DateTime.Today, DueDate = DateTime.Today.AddDays(10) };

                // Act and Assert
                Assert.ThrowsException<ValidationException>(() => servicesImplementation.AddBorrowedBook(borrowedBook), $"The borrow domain limit in the interval has been reached for {this.book.BookDomains.First().Name}");
            }
        }

        /// <summary>
        /// Verifies that a new borrowed book cannot be added when the maximum number of allowed borrow time extensions for a person within a specific interval has been reached.
        /// </summary>
        [TestMethod]
        public void TestVerifyBorrowExtensionsReached_OnAdd()
        {
            using (this.mocks.Record())
            {
                Expect.Call(this.borrowedBookDataService.CountBorrowedBooksByEditionWithNullReturnedDate(Arg<Edition>.Is.Anything)).Return(2);
                Expect.Call(this.borrowedBookDataService.CountBorrowedBooksByPersonAndDate(Arg<Person>.Is.Anything, Arg<DateTime>.Is.Anything)).Return(2);
                Expect.Call(this.borrowedBookDataService.CountBorrowedBooksForPersonAndDomainAfterDate(Arg<Person>.Is.Anything, Arg<BookDomain>.Is.Anything, Arg<DateTime>.Is.Anything)).Return(2);
                Expect.Call(this.borrowedBookDataService.GetDueDateDifferencesForPersonAfterDate(Arg<Person>.Is.Anything, Arg<DateTime>.Is.Anything)).Return(new List<int> { 28, 29 });
            }

            using (this.mocks.Playback())
            {
                // Arrange
                BorrowedBookServicesImplementation servicesImplementation = new BorrowedBookServicesImplementation(this.borrowedBookDataService);
                BorrowedBook borrowedBook = new BorrowedBook { Id = 10, Reader = this.reader, ReaderId = this.reader.Id, Staff = this.stuff, StaffId = this.stuff.Id, Edition = this.edition, BorrowDate = DateTime.Today, DueDate = DateTime.Today.AddDays(10) };

                // Act and Assert
                Assert.ThrowsException<ValidationException>(() => servicesImplementation.AddBorrowedBook(borrowedBook), "The limit for borrow time extensions in the interval has been reached");
            }
        }

        /// <summary>
        /// Verifies that a new borrowed book cannot be added when the specified person has borrowed a book from the same edition within a recently defined interval.
        /// </summary>
        [TestMethod]
        public void TestVerifyBorrowedTooRecently_OnAdd()
        {
            using (this.mocks.Record())
            {
                Expect.Call(this.borrowedBookDataService.CountBorrowedBooksByEditionWithNullReturnedDate(Arg<Edition>.Is.Anything)).Return(2);
                Expect.Call(this.borrowedBookDataService.CountBorrowedBooksByPersonAndDate(Arg<Person>.Is.Anything, Arg<DateTime>.Is.Anything)).Return(2);
                Expect.Call(this.borrowedBookDataService.CountBorrowedBooksForPersonAndDomainAfterDate(Arg<Person>.Is.Anything, Arg<BookDomain>.Is.Anything, Arg<DateTime>.Is.Anything)).Return(2);
                Expect.Call(this.borrowedBookDataService.GetDueDateDifferencesForPersonAfterDate(Arg<Person>.Is.Anything, Arg<DateTime>.Is.Anything)).Return(new List<int> { 7, 14 });
                Expect.Call(this.borrowedBookDataService.CountBorrowedBooksByEditionForPersonAfterDate(Arg<Person>.Is.Anything, Arg<Edition>.Is.Anything, Arg<DateTime>.Is.Anything)).Return(1);
            }

            using (this.mocks.Playback())
            {
                // Arrange
                BorrowedBookServicesImplementation servicesImplementation = new BorrowedBookServicesImplementation(this.borrowedBookDataService);
                BorrowedBook borrowedBook = new BorrowedBook { Id = 10, Reader = this.reader, ReaderId = this.reader.Id, Staff = this.stuff, StaffId = this.stuff.Id, Edition = this.edition, BorrowDate = DateTime.Today, DueDate = DateTime.Today.AddDays(10) };

                // Act and Assert
                Assert.ThrowsException<ValidationException>(() => servicesImplementation.AddBorrowedBook(borrowedBook), "The interval since the last borrow has not passed");
            }
        }

        /// <summary>
        /// Verifies that a new borrowed book cannot be added when the specified person has reached the maximum number of borrows on the current date.
        /// </summary>
        [TestMethod]
        public void TestVerifyTodayBorrowsReached_OnAdd()
        {
            using (this.mocks.Record())
            {
                Expect.Call(this.borrowedBookDataService.CountBorrowedBooksByEditionWithNullReturnedDate(Arg<Edition>.Is.Anything)).Return(2);
                Expect.Call(this.borrowedBookDataService.CountBorrowedBooksByPersonAndDate(Arg<Person>.Is.Anything, Arg<DateTime>.Is.Anything)).Return(2);
                Expect.Call(this.borrowedBookDataService.CountBorrowedBooksForPersonAndDomainAfterDate(Arg<Person>.Is.Anything, Arg<BookDomain>.Is.Anything, Arg<DateTime>.Is.Anything)).Return(2);
                Expect.Call(this.borrowedBookDataService.GetDueDateDifferencesForPersonAfterDate(Arg<Person>.Is.Anything, Arg<DateTime>.Is.Anything)).Return(new List<int> { 7, 14 });
                Expect.Call(this.borrowedBookDataService.CountBorrowedBooksByEditionForPersonAfterDate(Arg<Person>.Is.Anything, Arg<Edition>.Is.Anything, Arg<DateTime>.Is.Anything)).Return(0);
                Expect.Call(this.borrowedBookDataService.CountBooksBorrowedByPersonOnDate(Arg<Person>.Is.Anything, Arg<DateTime>.Is.Anything)).Return(5);
            }

            using (this.mocks.Playback())
            {
                // Arrange
                BorrowedBookServicesImplementation servicesImplementation = new BorrowedBookServicesImplementation(this.borrowedBookDataService);
                BorrowedBook borrowedBook = new BorrowedBook { Id = 10, Reader = this.reader, ReaderId = this.reader.Id, Staff = this.stuff, StaffId = this.stuff.Id, Edition = this.edition, BorrowDate = DateTime.Today, DueDate = DateTime.Today.AddDays(10) };

                // Act and Assert
                Assert.ThrowsException<ValidationException>(() => servicesImplementation.AddBorrowedBook(borrowedBook), "The maximum borrows today have been reached");
            }
        }

        /// <summary>
        /// Verifies that a new borrowed book cannot be added when the maximum number of borrows by staff on the current date has been reached.
        /// </summary>
        [TestMethod]
        public void TestVerifyStuffCanBorrow_OnAdd()
        {
            using (this.mocks.Record())
            {
                Expect.Call(this.borrowedBookDataService.CountBorrowedBooksByEditionWithNullReturnedDate(Arg<Edition>.Is.Anything)).Return(2);
                Expect.Call(this.borrowedBookDataService.CountBorrowedBooksByPersonAndDate(Arg<Person>.Is.Anything, Arg<DateTime>.Is.Anything)).Return(2);
                Expect.Call(this.borrowedBookDataService.CountBorrowedBooksForPersonAndDomainAfterDate(Arg<Person>.Is.Anything, Arg<BookDomain>.Is.Anything, Arg<DateTime>.Is.Anything)).Return(2);
                Expect.Call(this.borrowedBookDataService.GetDueDateDifferencesForPersonAfterDate(Arg<Person>.Is.Anything, Arg<DateTime>.Is.Anything)).Return(new List<int> { 7, 14 });
                Expect.Call(this.borrowedBookDataService.CountBorrowedBooksByEditionForPersonAfterDate(Arg<Person>.Is.Anything, Arg<Edition>.Is.Anything, Arg<DateTime>.Is.Anything)).Return(0);
                Expect.Call(this.borrowedBookDataService.CountBooksBorrowedByPersonOnDate(Arg<Person>.Is.Anything, Arg<DateTime>.Is.Anything)).Return(3);
                Expect.Call(this.borrowedBookDataService.CountBooksBorrowedBySuffOnDate(Arg<Person>.Is.Anything, Arg<DateTime>.Is.Anything)).Return(10);
            }

            using (this.mocks.Playback())
            {
                // Arrange
                BorrowedBookServicesImplementation servicesImplementation = new BorrowedBookServicesImplementation(this.borrowedBookDataService);
                BorrowedBook borrowedBook = new BorrowedBook { Id = 10, Reader = this.reader, ReaderId = this.reader.Id, Staff = this.stuff, StaffId = this.stuff.Id, Edition = this.edition, BorrowDate = DateTime.Today, DueDate = DateTime.Today.AddDays(10) };

                // Act and Assert
                Assert.ThrowsException<ValidationException>(() => servicesImplementation.AddBorrowedBook(borrowedBook), "The maximum borrows by staff today have been reached");
            }
        }

        /// <summary>
        /// Verifies that a new borrowed book can be successfully added when all validation checks pass.
        /// </summary>
        [TestMethod]
        public void TestAddBorrowedBook()
        {
            using (this.mocks.Record())
            {
                Expect.Call(this.borrowedBookDataService.CountBorrowedBooksByEditionWithNullReturnedDate(Arg<Edition>.Is.Anything)).Return(2);
                Expect.Call(this.borrowedBookDataService.CountBorrowedBooksByPersonAndDate(Arg<Person>.Is.Anything, Arg<DateTime>.Is.Anything)).Return(2);
                Expect.Call(this.borrowedBookDataService.CountBorrowedBooksForPersonAndDomainAfterDate(Arg<Person>.Is.Anything, Arg<BookDomain>.Is.Anything, Arg<DateTime>.Is.Anything)).Return(2);
                Expect.Call(this.borrowedBookDataService.GetDueDateDifferencesForPersonAfterDate(Arg<Person>.Is.Anything, Arg<DateTime>.Is.Anything)).Return(new List<int> { 7, 14 });
                Expect.Call(this.borrowedBookDataService.CountBorrowedBooksByEditionForPersonAfterDate(Arg<Person>.Is.Anything, Arg<Edition>.Is.Anything, Arg<DateTime>.Is.Anything)).Return(0);
                Expect.Call(this.borrowedBookDataService.CountBooksBorrowedByPersonOnDate(Arg<Person>.Is.Anything, Arg<DateTime>.Is.Anything)).Return(3);
                Expect.Call(this.borrowedBookDataService.CountBooksBorrowedBySuffOnDate(Arg<Person>.Is.Anything, Arg<DateTime>.Is.Anything)).Return(5);
                Expect.Call(() => this.borrowedBookDataService.AddBorrowedBook(Arg<BorrowedBook>.Is.Anything)).WhenCalled(call =>
                {
                    BorrowedBook borrowedBook = (BorrowedBook)call.Arguments[0];
                    this.borrowedBooks.Add(borrowedBook);
                });
            }

            using (this.mocks.Playback())
            {
                // Arrange
                BorrowedBookServicesImplementation servicesImplementation = new BorrowedBookServicesImplementation(this.borrowedBookDataService);
                BorrowedBook borrowedBook = new BorrowedBook { Id = 10, Reader = this.reader, ReaderId = this.reader.Id, Staff = this.stuff, StaffId = this.stuff.Id, Edition = this.edition, BorrowDate = DateTime.Today, DueDate = DateTime.Today.AddDays(10) };

                // Act
                servicesImplementation.AddBorrowedBook(borrowedBook);

                // Assert
                Assert.IsNotNull(this.borrowedBooks.First(a => a.Id == borrowedBook.Id));
                Assert.AreEqual(this.borrowedBooks.Count, 4);
            }
        }

        /// <summary>
        /// Verifies that a staff member cannot borrow a book when the maximum number of borrows by staff on the current date has been reached.
        /// </summary>
        public void TestVerifyTodayBorrowsReached_Stuff_OnAdd()
        {
            using (this.mocks.Record())
            {
                Expect.Call(this.borrowedBookDataService.CountBorrowedBooksByEditionWithNullReturnedDate(Arg<Edition>.Is.Anything)).Return(2);
                Expect.Call(this.borrowedBookDataService.CountBorrowedBooksByPersonAndDate(Arg<Person>.Is.Anything, Arg<DateTime>.Is.Anything)).Return(2);
                Expect.Call(this.borrowedBookDataService.CountBorrowedBooksForPersonAndDomainAfterDate(Arg<Person>.Is.Anything, Arg<BookDomain>.Is.Anything, Arg<DateTime>.Is.Anything)).Return(2);
                Expect.Call(this.borrowedBookDataService.GetDueDateDifferencesForPersonAfterDate(Arg<Person>.Is.Anything, Arg<DateTime>.Is.Anything)).Return(new List<int> { 7, 14 });
                Expect.Call(this.borrowedBookDataService.CountBorrowedBooksByEditionForPersonAfterDate(Arg<Person>.Is.Anything, Arg<Edition>.Is.Anything, Arg<DateTime>.Is.Anything)).Return(0);
                Expect.Call(this.borrowedBookDataService.CountBooksBorrowedBySuffOnDate(Arg<Person>.Is.Anything, Arg<DateTime>.Is.Anything)).Return(5);
                Expect.Call(() => this.borrowedBookDataService.AddBorrowedBook(Arg<BorrowedBook>.Is.Anything)).WhenCalled(call =>
                {
                    BorrowedBook borrowedBook = (BorrowedBook)call.Arguments[0];
                    this.borrowedBooks.Add(borrowedBook);
                });
            }

            using (this.mocks.Playback())
            {
                // Arrange
                BorrowedBookServicesImplementation servicesImplementation = new BorrowedBookServicesImplementation(this.borrowedBookDataService);
                BorrowedBook borrowedBook = new BorrowedBook { Id = 10, Reader = this.stuff, ReaderId = this.stuff.Id, Staff = this.stuff, StaffId = this.stuff.Id, Edition = this.edition, BorrowDate = DateTime.Today, DueDate = DateTime.Today.AddDays(10), ReturnedDate = DateTime.Today.AddDays(10) };

                // Act
                servicesImplementation.AddBorrowedBook(borrowedBook);

                // Assert
                Assert.IsNotNull(this.borrowedBooks.First(a => a.Id == borrowedBook.Id));
                Assert.AreEqual(this.borrowedBooks.Count, 4);
            }
        }

        /// <summary>
        /// Verifies that a staff member cannot be updated to a reader when updating a borrowed book.
        /// </summary>
        [TestMethod]
        public void TestVerifyStuffPersonIsTypeStuff_OnUpdate()
        {
            // Arrange
            BorrowedBookServicesImplementation servicesImplementation = new BorrowedBookServicesImplementation(this.borrowedBookDataService);
            BorrowedBook borrowedBook = this.borrowedBooks.First();
            borrowedBook.Staff.Id = this.reader.Id;
            borrowedBook.Staff = this.reader;

            // Act and Assert
            Assert.ThrowsException<ValidationException>(() => servicesImplementation.UpdateBorrowedBook(borrowedBook), "A reader cannot borrow a book to another reader");
        }

        /// <summary>
        /// Verifies that the BorrowedDate cannot be set later than the DueDate when updating a borrowed book.
        /// </summary>
        [TestMethod]
        public void TestVerifyDates_OnUpdate_BorrowedDateCanNotBeAfterDueDate()
        {
            // Arrange
            BorrowedBookServicesImplementation servicesImplementation = new BorrowedBookServicesImplementation(this.borrowedBookDataService);
            BorrowedBook borrowedBook = this.borrowedBooks.First();
            borrowedBook.DueDate = borrowedBook.BorrowDate.AddDays(-1);

            // Act and Assert
            Assert.ThrowsException<ValidationException>(() => servicesImplementation.UpdateBorrowedBook(borrowedBook), "The BorrowDate cannot be later than the DueDate");
        }

        /// <summary>
        /// Verifies that the BorrowedDate cannot be set later than the ReturnedDate when updating a borrowed book.
        /// </summary>
        [TestMethod]
        public void TestVerifyDates_OnUpdate_BorrowedDateCanNotBeAfterReturnedDate()
        {
            // Arrange
            BorrowedBookServicesImplementation servicesImplementation = new BorrowedBookServicesImplementation(this.borrowedBookDataService);
            BorrowedBook borrowedBook = this.borrowedBooks.First();
            borrowedBook.ReturnedDate = borrowedBook.BorrowDate.AddDays(-1);

            // Act and Assert
            Assert.ThrowsException<ValidationException>(() => servicesImplementation.UpdateBorrowedBook(borrowedBook), "The BorrowDate cannot be later than the ReturnedDate");
        }

        /// <summary>
        /// Verifies that a borrowed book cannot be updated when the maximum number of allowed borrowed books for a specific edition has been reached.
        /// </summary>
        [TestMethod]
        public void TestVerifyCanBorrowMoreBooks_OnUpdate()
        {
            using (this.mocks.Record())
            {
                Expect.Call(this.borrowedBookDataService.CountBorrowedBooksByEditionWithNullReturnedDate(Arg<Edition>.Is.Anything)).Return(20);
            }

            using (this.mocks.Playback())
            {
                // Arrange
                BorrowedBookServicesImplementation servicesImplementation = new BorrowedBookServicesImplementation(this.borrowedBookDataService);
                BorrowedBook borrowedBook = this.borrowedBooks.First();

                // Act and Assert
                Assert.ThrowsException<ValidationException>(() => servicesImplementation.UpdateBorrowedBook(borrowedBook), "There are not enough books to be borrowed");
            }
        }

        /// <summary>
        /// Verifies that a borrowed book cannot be updated when the maximum number of allowed borrowed books for a person in a specific interval has been reached.
        /// </summary>
        [TestMethod]
        public void TestVerifyLimitForPersonReached_OnUpdate()
        {
            using (this.mocks.Record())
            {
                Expect.Call(this.borrowedBookDataService.CountBorrowedBooksByEditionWithNullReturnedDate(Arg<Edition>.Is.Anything)).Return(2);
                Expect.Call(this.borrowedBookDataService.CountBorrowedBooksByPersonAndDate(Arg<Person>.Is.Anything, Arg<DateTime>.Is.Anything)).Return(15);
            }

            using (this.mocks.Playback())
            {
                // Arrange
                BorrowedBookServicesImplementation servicesImplementation = new BorrowedBookServicesImplementation(this.borrowedBookDataService);
                BorrowedBook borrowedBook = this.borrowedBooks.First();

                // Act and Assert
                Assert.ThrowsException<ValidationException>(() => servicesImplementation.UpdateBorrowedBook(borrowedBook), "The borrow limit in the interval has been reached");
            }
        }

        /// <summary>
        /// Verifies that a borrowed book cannot be updated when the maximum number of allowed borrowed books for a person within a specific book domain and interval has been reached.
        /// </summary>
        [TestMethod]
        public void TestVerifyDomainLimitForPersonReached_OnUpdate()
        {
            using (this.mocks.Record())
            {
                Expect.Call(this.borrowedBookDataService.CountBorrowedBooksByEditionWithNullReturnedDate(Arg<Edition>.Is.Anything)).Return(2);
                Expect.Call(this.borrowedBookDataService.CountBorrowedBooksByPersonAndDate(Arg<Person>.Is.Anything, Arg<DateTime>.Is.Anything)).Return(2);
                Expect.Call(this.borrowedBookDataService.CountBorrowedBooksForPersonAndDomainAfterDate(Arg<Person>.Is.Anything, Arg<BookDomain>.Is.Anything, Arg<DateTime>.Is.Anything)).Return(20);
            }

            using (this.mocks.Playback())
            {
                // Arrange
                BorrowedBookServicesImplementation servicesImplementation = new BorrowedBookServicesImplementation(this.borrowedBookDataService);
                BorrowedBook borrowedBook = this.borrowedBooks.First();

                // Act and Assert
                Assert.ThrowsException<ValidationException>(() => servicesImplementation.UpdateBorrowedBook(borrowedBook), $"The borrow domain limit in the interval has been reached for {this.book.BookDomains.First().Name}");
            }
        }

        /// <summary>
        /// Verifies that a borrowed book cannot be updated when the maximum number of allowed borrow time extensions for a person within a specific interval has been reached.
        /// </summary>
        [TestMethod]
        public void TestVerifyBorrowExtensionsReached_OnUpdate()
        {
            using (this.mocks.Record())
            {
                Expect.Call(this.borrowedBookDataService.CountBorrowedBooksByEditionWithNullReturnedDate(Arg<Edition>.Is.Anything)).Return(2);
                Expect.Call(this.borrowedBookDataService.CountBorrowedBooksByPersonAndDate(Arg<Person>.Is.Anything, Arg<DateTime>.Is.Anything)).Return(2);
                Expect.Call(this.borrowedBookDataService.CountBorrowedBooksForPersonAndDomainAfterDate(Arg<Person>.Is.Anything, Arg<BookDomain>.Is.Anything, Arg<DateTime>.Is.Anything)).Return(2);
                Expect.Call(this.borrowedBookDataService.GetDueDateDifferencesForPersonAfterDate(Arg<Person>.Is.Anything, Arg<DateTime>.Is.Anything)).Return(new List<int> { 28, 29 });
            }

            using (this.mocks.Playback())
            {
                // Arrange
                BorrowedBookServicesImplementation servicesImplementation = new BorrowedBookServicesImplementation(this.borrowedBookDataService);
                BorrowedBook borrowedBook = this.borrowedBooks.First();

                // Act and Assert
                Assert.ThrowsException<ValidationException>(() => servicesImplementation.UpdateBorrowedBook(borrowedBook), "The limit for borrow time extensions in the interval has been reached");
            }
        }

        /// <summary>
        /// Verifies that a borrowed book cannot be updated when the specified person has borrowed a book from the same edition within a recently defined interval.
        /// </summary>
        [TestMethod]
        public void TestVerifyBorrowedTooRecently_OnUpdate()
        {
            using (this.mocks.Record())
            {
                Expect.Call(this.borrowedBookDataService.CountBorrowedBooksByEditionWithNullReturnedDate(Arg<Edition>.Is.Anything)).Return(2);
                Expect.Call(this.borrowedBookDataService.CountBorrowedBooksByPersonAndDate(Arg<Person>.Is.Anything, Arg<DateTime>.Is.Anything)).Return(2);
                Expect.Call(this.borrowedBookDataService.CountBorrowedBooksForPersonAndDomainAfterDate(Arg<Person>.Is.Anything, Arg<BookDomain>.Is.Anything, Arg<DateTime>.Is.Anything)).Return(2);
                Expect.Call(this.borrowedBookDataService.GetDueDateDifferencesForPersonAfterDate(Arg<Person>.Is.Anything, Arg<DateTime>.Is.Anything)).Return(new List<int> { 7, 14 });
                Expect.Call(this.borrowedBookDataService.CountBorrowedBooksByEditionForPersonAfterDate(Arg<Person>.Is.Anything, Arg<Edition>.Is.Anything, Arg<DateTime>.Is.Anything)).Return(1);
            }

            using (this.mocks.Playback())
            {
                // Arrange
                BorrowedBookServicesImplementation servicesImplementation = new BorrowedBookServicesImplementation(this.borrowedBookDataService);
                BorrowedBook borrowedBook = this.borrowedBooks.First();

                // Act and Assert
                Assert.ThrowsException<ValidationException>(() => servicesImplementation.UpdateBorrowedBook(borrowedBook), "The interval since the last borrow has not passed");
            }
        }

        /// <summary>
        /// Verifies that a borrowed book cannot be updated when the specified person has reached the maximum number of borrows on the current date.
        /// </summary>
        [TestMethod]
        public void TestVerifyTodayBorrowsReached_OnUpdate()
        {
            using (this.mocks.Record())
            {
                Expect.Call(this.borrowedBookDataService.CountBorrowedBooksByEditionWithNullReturnedDate(Arg<Edition>.Is.Anything)).Return(2);
                Expect.Call(this.borrowedBookDataService.CountBorrowedBooksByPersonAndDate(Arg<Person>.Is.Anything, Arg<DateTime>.Is.Anything)).Return(2);
                Expect.Call(this.borrowedBookDataService.CountBorrowedBooksForPersonAndDomainAfterDate(Arg<Person>.Is.Anything, Arg<BookDomain>.Is.Anything, Arg<DateTime>.Is.Anything)).Return(2);
                Expect.Call(this.borrowedBookDataService.GetDueDateDifferencesForPersonAfterDate(Arg<Person>.Is.Anything, Arg<DateTime>.Is.Anything)).Return(new List<int> { 7, 14 });
                Expect.Call(this.borrowedBookDataService.CountBorrowedBooksByEditionForPersonAfterDate(Arg<Person>.Is.Anything, Arg<Edition>.Is.Anything, Arg<DateTime>.Is.Anything)).Return(0);
                Expect.Call(this.borrowedBookDataService.CountBooksBorrowedByPersonOnDate(Arg<Person>.Is.Anything, Arg<DateTime>.Is.Anything)).Return(5);
            }

            using (this.mocks.Playback())
            {
                // Arrange
                BorrowedBookServicesImplementation servicesImplementation = new BorrowedBookServicesImplementation(this.borrowedBookDataService);
                BorrowedBook borrowedBook = this.borrowedBooks.First();

                // Act and Assert
                Assert.ThrowsException<ValidationException>(() => servicesImplementation.UpdateBorrowedBook(borrowedBook), "The maximum borrows today have been reached");
            }
        }

        /// <summary>
        /// Verifies that a borrowed book cannot be updated when the maximum number of borrows by staff on the current date has been reached.
        /// </summary>
        [TestMethod]
        public void TestVerifyStuffCanBorrow_OnUpdate()
        {
            using (this.mocks.Record())
            {
                Expect.Call(this.borrowedBookDataService.CountBorrowedBooksByEditionWithNullReturnedDate(Arg<Edition>.Is.Anything)).Return(2);
                Expect.Call(this.borrowedBookDataService.CountBorrowedBooksByPersonAndDate(Arg<Person>.Is.Anything, Arg<DateTime>.Is.Anything)).Return(2);
                Expect.Call(this.borrowedBookDataService.CountBorrowedBooksForPersonAndDomainAfterDate(Arg<Person>.Is.Anything, Arg<BookDomain>.Is.Anything, Arg<DateTime>.Is.Anything)).Return(2);
                Expect.Call(this.borrowedBookDataService.GetDueDateDifferencesForPersonAfterDate(Arg<Person>.Is.Anything, Arg<DateTime>.Is.Anything)).Return(new List<int> { 7, 14 });
                Expect.Call(this.borrowedBookDataService.CountBorrowedBooksByEditionForPersonAfterDate(Arg<Person>.Is.Anything, Arg<Edition>.Is.Anything, Arg<DateTime>.Is.Anything)).Return(0);
                Expect.Call(this.borrowedBookDataService.CountBooksBorrowedByPersonOnDate(Arg<Person>.Is.Anything, Arg<DateTime>.Is.Anything)).Return(3);
                Expect.Call(this.borrowedBookDataService.CountBooksBorrowedBySuffOnDate(Arg<Person>.Is.Anything, Arg<DateTime>.Is.Anything)).Return(10);
            }

            using (this.mocks.Playback())
            {
                // Arrange
                BorrowedBookServicesImplementation servicesImplementation = new BorrowedBookServicesImplementation(this.borrowedBookDataService);
                BorrowedBook borrowedBook = this.borrowedBooks.First();

                // Act and Assert
                Assert.ThrowsException<ValidationException>(() => servicesImplementation.UpdateBorrowedBook(borrowedBook), "The maximum borrows by staff today have been reached");
            }
        }

        /// <summary>
        /// Verifies that a borrowed book can be successfully updated when all validation checks pass.
        /// </summary>
        [TestMethod]
        public void TestUpdateBorrowedBook()
        {
            using (this.mocks.Record())
            {
                Expect.Call(this.borrowedBookDataService.CountBorrowedBooksByEditionWithNullReturnedDate(Arg<Edition>.Is.Anything)).Return(2);
                Expect.Call(this.borrowedBookDataService.CountBorrowedBooksByPersonAndDate(Arg<Person>.Is.Anything, Arg<DateTime>.Is.Anything)).Return(2);
                Expect.Call(this.borrowedBookDataService.CountBorrowedBooksForPersonAndDomainAfterDate(Arg<Person>.Is.Anything, Arg<BookDomain>.Is.Anything, Arg<DateTime>.Is.Anything)).Return(2);
                Expect.Call(this.borrowedBookDataService.GetDueDateDifferencesForPersonAfterDate(Arg<Person>.Is.Anything, Arg<DateTime>.Is.Anything)).Return(new List<int> { 7, 14 });
                Expect.Call(this.borrowedBookDataService.CountBorrowedBooksByEditionForPersonAfterDate(Arg<Person>.Is.Anything, Arg<Edition>.Is.Anything, Arg<DateTime>.Is.Anything)).Return(0);
                Expect.Call(this.borrowedBookDataService.CountBooksBorrowedByPersonOnDate(Arg<Person>.Is.Anything, Arg<DateTime>.Is.Anything)).Return(3);
                Expect.Call(this.borrowedBookDataService.CountBooksBorrowedBySuffOnDate(Arg<Person>.Is.Anything, Arg<DateTime>.Is.Anything)).Return(5);
                Expect.Call(() => this.borrowedBookDataService.UpdateBorrowedBook(Arg<BorrowedBook>.Is.Anything)).WhenCalled(call =>
                {
                    BorrowedBook borrowedBook = (BorrowedBook)call.Arguments[0];
                    int index = this.borrowedBooks.FindIndex(a => a.Id == borrowedBook.Id);
                    if (index == -1)
                    {
                        throw new Exception("BorrowedBook not found");
                    }

                    this.borrowedBooks[index] = borrowedBook;
                });
            }

            using (this.mocks.Playback())
            {
                // Arrange
                BorrowedBookServicesImplementation servicesImplementation = new BorrowedBookServicesImplementation(this.borrowedBookDataService);
                BorrowedBook borrowedBook = this.borrowedBooks.First();
                borrowedBook.DueDate = borrowedBook.BorrowDate.AddDays(10);

                // Act
                servicesImplementation.UpdateBorrowedBook(borrowedBook);

                // Assert
                Assert.AreEqual(this.borrowedBooks.First().DueDate, borrowedBook.BorrowDate.AddDays(10));
                Assert.AreEqual(this.borrowedBooks.Count, 3);
            }
        }

        /// <summary>
        /// Verifies that a borrowed book cannot be updated when a staff member tries to return a book, and the maximum number of borrows by staff on the current date has been reached.
        /// </summary>
        [TestMethod]
        public void TestVerifyTodayBorrowsReached_Stuff_OnUpdate()
        {
            using (this.mocks.Record())
            {
                Expect.Call(this.borrowedBookDataService.CountBorrowedBooksByEditionWithNullReturnedDate(Arg<Edition>.Is.Anything)).Return(2);
                Expect.Call(this.borrowedBookDataService.CountBorrowedBooksByPersonAndDate(Arg<Person>.Is.Anything, Arg<DateTime>.Is.Anything)).Return(2);
                Expect.Call(this.borrowedBookDataService.CountBorrowedBooksForPersonAndDomainAfterDate(Arg<Person>.Is.Anything, Arg<BookDomain>.Is.Anything, Arg<DateTime>.Is.Anything)).Return(2);
                Expect.Call(this.borrowedBookDataService.GetDueDateDifferencesForPersonAfterDate(Arg<Person>.Is.Anything, Arg<DateTime>.Is.Anything)).Return(new List<int> { 7, 14 });
                Expect.Call(this.borrowedBookDataService.CountBorrowedBooksByEditionForPersonAfterDate(Arg<Person>.Is.Anything, Arg<Edition>.Is.Anything, Arg<DateTime>.Is.Anything)).Return(0);
                Expect.Call(this.borrowedBookDataService.CountBooksBorrowedBySuffOnDate(Arg<Person>.Is.Anything, Arg<DateTime>.Is.Anything)).Return(5);
                Expect.Call(() => this.borrowedBookDataService.UpdateBorrowedBook(Arg<BorrowedBook>.Is.Anything)).WhenCalled(call =>
                {
                    BorrowedBook borrowedBook = (BorrowedBook)call.Arguments[0];
                    int index = this.borrowedBooks.FindIndex(a => a.Id == borrowedBook.Id);
                    if (index == -1)
                    {
                        throw new Exception("BorrowedBook not found");
                    }

                    this.borrowedBooks[index] = borrowedBook;
                });
            }

            using (this.mocks.Playback())
            {
                // Arrange
                BorrowedBookServicesImplementation servicesImplementation = new BorrowedBookServicesImplementation(this.borrowedBookDataService);
                BorrowedBook borrowedBook = this.borrowedBooks.First();
                borrowedBook.ReturnedDate = borrowedBook.BorrowDate.AddDays(7);
                borrowedBook.Reader = this.stuff;
                borrowedBook.ReaderId = this.stuff.Id;

                // Act
                servicesImplementation.UpdateBorrowedBook(borrowedBook);

                // Assert
                Assert.IsNotNull(this.borrowedBooks.First().ReturnedDate);
                Assert.AreEqual(this.borrowedBooks.First().ReturnedDate, borrowedBook.BorrowDate.AddDays(7));
                Assert.AreEqual(this.borrowedBooks.Count, 3);
            }
        }
    }
}
