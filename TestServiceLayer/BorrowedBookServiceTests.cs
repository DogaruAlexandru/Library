// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BorrowedBookServiceTests.cs" company="Transilvania University of Brasov">
//   Copyright (c) Dogaru Alexandru.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TestServiceLayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
    [TestClass]
    public class BorrowedBookServiceTests
    {
        /// <summary>
        /// Represents the mock repository used for creating mock objects during unit tests.
        /// </summary>
        private MockRepository mocks;

        /// <summary>
        /// Represents the mock service for accessing borrowedbook-related data during unit tests.
        /// </summary>
        private IBorrowedBookDataService borrowedbookDataService;

        /// <summary>
        /// Represents a collection of borrowedbooks in the application.
        /// </summary>
        private List<BorrowedBook> borrowedbooks;

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
            this.borrowedbookDataService = this.mocks.StrictMock<IBorrowedBookDataService>();
            this.book = new Book { Id = 1, Title = "title2", BookDomains = new List<BookDomain> { new BookDomain { Id = 1, Name = "name1" } } };
            this.reader = new Person { Id = 0, FirstName = "FirstName1", LastName = "LastName1", CNP = "2491738465248", Type = PersonType.Reader, Address = "asdadsasdasdasd", EmailAddress = "asd1@mail.com" };
            this.stuff = new Person { Id = 2, FirstName = "FirstName3", LastName = "LastName3", CNP = "5010474094949", Type = PersonType.LibraryPersonnel, Address = "asdad sasdasdasd", EmailAddress = "asdasda1@mail.com" };
            this.edition = new Edition { Id = 0, Name = "name1", Book = this.book, Publisher = "asdasd", Type = BookType.LibraryBinding, PageCount = 123, CanBorrow = 23, CanNotBorrow = 23 };
            this.borrowedbooks = new List<BorrowedBook>
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
                Expect.Call(this.borrowedbookDataService.GetAllBorrowedBooks()).Return(this.borrowedbooks);
            }

            using (this.mocks.Playback())
            {
                // Arrange
                BorrowedBookServicesImplementation servicesImplementation = new BorrowedBookServicesImplementation(this.borrowedbookDataService);

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
                Expect.Call(this.borrowedbookDataService.GetAllBorrowedBooks()).Return(this.borrowedbooks);
            }

            using (this.mocks.Playback())
            {
                // Arrange
                BorrowedBookServicesImplementation servicesImplementation = new BorrowedBookServicesImplementation(this.borrowedbookDataService);
                this.borrowedbooks.Clear();

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
                Expect.Call(() => this.borrowedbookDataService.GetBorrowedBookById(Arg<int>.Is.Anything)).WhenCalled(call =>
                {
                    int idParameter = (int)call.Arguments[0];
                    call.ReturnValue = this.borrowedbooks.FirstOrDefault(borrowedbook => borrowedbook.Id == idParameter);
                });
            }

            using (this.mocks.Playback())
            {
                // Arrange
                BorrowedBookServicesImplementation servicesImplementation = new BorrowedBookServicesImplementation(this.borrowedbookDataService);
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
                Expect.Call(() => this.borrowedbookDataService.GetBorrowedBookById(Arg<int>.Is.Anything)).WhenCalled(call =>
                {
                    int idParameter = (int)call.Arguments[0];
                    call.ReturnValue = this.borrowedbooks.FirstOrDefault(borrowedbook => borrowedbook.Id == idParameter);
                });
            }

            using (this.mocks.Playback())
            {
                // Arrange
                BorrowedBookServicesImplementation servicesImplementation = new BorrowedBookServicesImplementation(this.borrowedbookDataService);
                int existingBorrowedBookId = 1;
                this.borrowedbooks.Clear();

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
                Expect.Call(() => this.borrowedbookDataService.GetBorrowedBookById(Arg<int>.Is.Anything)).WhenCalled(call =>
                {
                    int idParameter = (int)call.Arguments[0];
                    call.ReturnValue = this.borrowedbooks.FirstOrDefault(borrowedbook => borrowedbook.Id == idParameter);
                });
            }

            using (this.mocks.Playback())
            {
                // Arrange
                BorrowedBookServicesImplementation servicesImplementation = new BorrowedBookServicesImplementation(this.borrowedbookDataService);
                int nonExistingBorrowedBookId = 99;

                // Act
                BorrowedBook result = servicesImplementation.GetBorrowedBookById(nonExistingBorrowedBookId);

                // Assert
                Assert.IsNull(result);
            }
        }

        /// <summary>
        /// Tests the <see cref="BorrowedBookServicesImplementation.AddBorrowedBook"/> method when there are items.
        /// </summary>
        [TestMethod]
        public void TestAddBorrowedBooksHasItems()
        {
            using (this.mocks.Record())
            {
                Expect.Call(() => this.borrowedbookDataService.AddBorrowedBook(Arg<BorrowedBook>.Is.Anything)).WhenCalled(call =>
                {
                    BorrowedBook borrowedbookParameter = (BorrowedBook)call.Arguments[0];
                    this.borrowedbooks.Add(borrowedbookParameter);
                });
            }

            using (this.mocks.Playback())
            {
                // Arrange
                BorrowedBookServicesImplementation servicesImplementation = new BorrowedBookServicesImplementation(this.borrowedbookDataService);
                BorrowedBook borrowedBook = new BorrowedBook { Id = 10, Reader = this.reader, ReaderId = this.reader.Id, Staff = this.stuff, StaffId = this.stuff.Id, Edition = this.edition, BorrowDate = DateTime.Today, DueDate = DateTime.Today.AddDays(10) };

                // Act
                servicesImplementation.AddBorrowedBook(borrowedBook);

                // Assert
                Assert.IsNotNull(this.borrowedbooks.First(a => a.Id == borrowedBook.Id));
                Assert.AreEqual(this.borrowedbooks.Count, 4);
            }
        }

        /// <summary>
        /// Tests the <see cref="BorrowedBookServicesImplementation.AddBorrowedBook"/> method when there are 0 items initially.
        /// </summary>
        [TestMethod]
        public void TestAddBorrowedBooksHasNoItems()
        {
            using (this.mocks.Record())
            {
                Expect.Call(() => this.borrowedbookDataService.AddBorrowedBook(Arg<BorrowedBook>.Is.Anything)).WhenCalled(call =>
                {
                    BorrowedBook borrowedbookParameter = (BorrowedBook)call.Arguments[0];
                    this.borrowedbooks.Add(borrowedbookParameter);
                });
            }

            using (this.mocks.Playback())
            {
                // Arrange
                BorrowedBookServicesImplementation servicesImplementation = new BorrowedBookServicesImplementation(this.borrowedbookDataService);
                BorrowedBook borrowedBook = new BorrowedBook { Id = 10, Reader = this.reader, ReaderId = this.reader.Id, Staff = this.stuff, StaffId = this.stuff.Id, Edition = this.edition, BorrowDate = DateTime.Today, DueDate = DateTime.Today.AddDays(10) };
                this.borrowedbooks.Clear();

                // Act
                servicesImplementation.AddBorrowedBook(borrowedBook);

                // Assert
                Assert.IsNotNull(this.borrowedbooks.First(a => a.Id == borrowedBook.Id));
                Assert.AreEqual(this.borrowedbooks.Count, 1);
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
                Expect.Call(() => this.borrowedbookDataService.DeleteBorrowedBook(Arg<BorrowedBook>.Is.Anything)).WhenCalled(call =>
                {
                    BorrowedBook borrowedbookParameter = (BorrowedBook)call.Arguments[0];
                    int index = this.borrowedbooks.FindIndex(a => a.Id == borrowedbookParameter.Id);
                    if (index == -1)
                    {
                        throw new Exception();
                    }

                    this.borrowedbooks.RemoveAt(index);
                });
            }

            using (this.mocks.Playback())
            {
                // Arrange
                BorrowedBookServicesImplementation servicesImplementation = new BorrowedBookServicesImplementation(this.borrowedbookDataService);
                BorrowedBook borrowedbookToDelete = this.borrowedbooks.First(); // Select the first borrowedBook for deletion

                // Act
                servicesImplementation.DeleteBorrowedBook(borrowedbookToDelete);

                // Assert
                Assert.AreEqual(this.borrowedbooks.Count, 2); // BorrowedBook should be removed
                Assert.IsFalse(this.borrowedbooks.Contains(borrowedbookToDelete));
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
                Expect.Call(() => this.borrowedbookDataService.DeleteBorrowedBook(Arg<BorrowedBook>.Is.Anything)).WhenCalled(call =>
                {
                    BorrowedBook borrowedbookParameter = (BorrowedBook)call.Arguments[0];
                    int index = this.borrowedbooks.FindIndex(a => a.Id == borrowedbookParameter.Id);
                    if (index == -1)
                    {
                        throw new Exception();
                    }

                    this.borrowedbooks.RemoveAt(index);
                });
            }

            using (this.mocks.Playback())
            {
                // Arrange
                BorrowedBookServicesImplementation servicesImplementation = new BorrowedBookServicesImplementation(this.borrowedbookDataService);
                BorrowedBook nonExistingBorrowedBook = new BorrowedBook { Id = 99, Reader = this.reader, ReaderId = this.reader.Id, Staff = this.stuff, StaffId = this.stuff.Id, Edition = this.edition, BorrowDate = DateTime.Today, DueDate = DateTime.Today.AddDays(10) };

                // Assert and Act
                Assert.ThrowsException<Exception>(() => servicesImplementation.DeleteBorrowedBook(nonExistingBorrowedBook));
            }
        }

        /// <summary>
        /// Tests the <see cref="BorrowedBookServicesImplementation.UpdateBorrowedBook"/> method when the borrowedbook exists.
        /// </summary>
        [TestMethod]
        public void TestUpdateBorrowedBookBorrowedBookExists()
        {
            using (this.mocks.Record())
            {
                Expect.Call(() => this.borrowedbookDataService.UpdateBorrowedBook(Arg<BorrowedBook>.Is.Anything)).WhenCalled(call =>
                {
                    BorrowedBook borrowedbookParameter = (BorrowedBook)call.Arguments[0];
                    int index = this.borrowedbooks.FindIndex(a => a.Id == borrowedbookParameter.Id);
                    if (index == -1)
                    {
                        throw new Exception("BorrowedBook not found");
                    }

                    borrowedbooks[index] = borrowedbookParameter;
                });
            }

            using (this.mocks.Playback())
            {
                // Arrange
                BorrowedBookServicesImplementation servicesImplementation = new BorrowedBookServicesImplementation(this.borrowedbookDataService);
                BorrowedBook borrowedbookToUpdate = this.borrowedbooks.First(); // Select the first borrowedbook for updating

                // Act
                servicesImplementation.UpdateBorrowedBook(borrowedbookToUpdate);

                // Assert
                Assert.IsTrue(this.borrowedbooks.Contains(borrowedbookToUpdate));
            }
        }

        /// <summary>
        /// Tests the <see cref="BorrowedBookServicesImplementation.UpdateBorrowedBook"/> method when the borrowedbook does not exist.
        /// </summary>
        [TestMethod]
        public void TestUpdateBorrowedBookBorrowedBookDoesNotExist()
        {
            using (this.mocks.Record())
            {
                Expect.Call(() => this.borrowedbookDataService.UpdateBorrowedBook(Arg<BorrowedBook>.Is.Anything)).WhenCalled(call =>
                {
                    BorrowedBook borrowedbookParameter = (BorrowedBook)call.Arguments[0];
                    int index = this.borrowedbooks.FindIndex(a => a.Id == borrowedbookParameter.Id);
                    if (index == -1)
                    {
                        throw new Exception("BorrowedBook not found");
                    }

                    borrowedbooks[index] = borrowedbookParameter;
                });
            }

            using (this.mocks.Playback())
            {
                // Arrange
                BorrowedBookServicesImplementation servicesImplementation = new BorrowedBookServicesImplementation(this.borrowedbookDataService);
                BorrowedBook nonExistingBorrowedBook = new BorrowedBook { Id = 99, Reader = this.reader, ReaderId = this.reader.Id, Staff = this.stuff, StaffId = this.stuff.Id, Edition = this.edition, BorrowDate = DateTime.Today, DueDate = DateTime.Today.AddDays(10) };

                // Assert and Act
                Assert.ThrowsException<Exception>(() => servicesImplementation.UpdateBorrowedBook(nonExistingBorrowedBook));
            }
        }
    }
}
