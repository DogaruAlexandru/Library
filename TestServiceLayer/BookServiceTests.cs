// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BookServiceTests.cs" company="Transilvania University of Brasov">
//   Copyright (c) Dogaru Alexandru.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TestServiceLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Configuration;
    using System.Linq;
    using DataMapper;
    using DomainModel;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Rhino.Mocks;
    using Rhino.Mocks.Constraints;
    using ServiceLayer.ServiceImplementation;
    using static log4net.Appender.RollingFileAppender;

    /// <summary>
    /// Represents unit tests for the <see cref="BookServicesImplementation"/> class.
    /// </summary>
    [TestClass]
    public class BookServiceTests
    {
        /// <summary>
        /// Represents the mock repository used for creating mock objects during unit tests.
        /// </summary>
        private MockRepository mocks;

        /// <summary>
        /// Represents the mock service for accessing Book-related data during unit tests.
        /// </summary>
        private IBookDataService bookDataService;

        /// <summary>
        /// Represents the mock service for accessing Book-related data during unit tests.
        /// </summary>
        private IBookDomainDataService bookDomainDataService;

        /// <summary>
        /// Represents a collection of books in the application.
        /// </summary>
        private List<Book> books;

        /// <summary>
        /// Represents a collection of bookDomains in the application.
        /// </summary>
        private List<BookDomain> bookDomains;

        /// <summary>
        /// Initializes test resources before each test method is executed.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            this.mocks = new MockRepository();
            this.bookDataService = this.mocks.StrictMock<IBookDataService>();
            this.bookDomainDataService = this.mocks.StrictMock<IBookDomainDataService>();

            List<Author> authorList = new List<Author> { new Author { Id = 1, Name = "name1" } };
            this.bookDomains = new List<BookDomain>
            {
                new BookDomain { Id = 0, Name = "name1" },
                new BookDomain { Id = 1, Name = "name2" },
                new BookDomain { Id = 2, Name = "name3" }
            };
            this.bookDomains[2].ParentDomain = this.bookDomains[0];
            this.books = new List<Book>
            {
                new Book { Id = 0, Title = "title1",  Authors = authorList, BookDomains = new List<BookDomain> { this.bookDomains[0] } },
                new Book { Id = 1, Title = "title2",  Authors = authorList, BookDomains = new List<BookDomain> { this.bookDomains[1] } },
                new Book { Id = 2, Title = "title3",  BookDomains = new List<BookDomain> { this.bookDomains[0], this.bookDomains[2] }, Description = "description" }
            };
        }

        /// <summary>
        /// Tests the <see cref="BookServicesImplementation.GetAllBooks"/> method when there are items.
        /// </summary>
        [TestMethod]
        public void TestGetAllBooksHasItems()
        {
            using (this.mocks.Record())
            {
                Expect.Call(this.bookDataService.GetAllBooks()).Return(this.books);
            }

            using (this.mocks.Playback())
            {
                // Arrange
                BookServicesImplementation servicesImplementation = new BookServicesImplementation(this.bookDataService, this.bookDomainDataService);

                // Act
                var list = servicesImplementation.GetAllBooks();

                // Assert
                Assert.AreEqual(list[0].Title, "title1");
                Assert.AreEqual(list.Count, 3);
            }
        }

        /// <summary>
        /// Tests the <see cref="BookServicesImplementation.GetAllBooks"/> method when there are no items.
        /// </summary>
        [TestMethod]
        public void TestGetAllBooksHasNoItems()
        {
            using (this.mocks.Record())
            {
                Expect.Call(this.bookDataService.GetAllBooks()).Return(this.books);
            }

            using (this.mocks.Playback())
            {
                // Arrange
                BookServicesImplementation servicesImplementation = new BookServicesImplementation(this.bookDataService, this.bookDomainDataService);
                this.books.Clear();

                // Act
                var list = servicesImplementation.GetAllBooks();

                // Assert
                Assert.AreEqual(list.Count, 0);
            }
        }

        /// <summary>
        /// Tests the <see cref="BookServicesImplementation.GetBookById"/> method when the Book exists.
        /// </summary>
        [TestMethod]
        public void TestGetBookByIdBookExists()
        {
            using (this.mocks.Record())
            {
                Expect.Call(() => this.bookDataService.GetBookById(Arg<int>.Is.Anything)).WhenCalled(call =>
                {
                    int idParameter = (int)call.Arguments[0];
                    call.ReturnValue = this.books.FirstOrDefault(Book => Book.Id == idParameter);
                });
            }

            using (this.mocks.Playback())
            {
                // Arrange
                BookServicesImplementation servicesImplementation = new BookServicesImplementation(this.bookDataService, this.bookDomainDataService);
                int existingBookId = 2;

                // Act
                Book result = servicesImplementation.GetBookById(existingBookId);

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(result.Id, existingBookId);
                Assert.AreEqual(result.Title, "title3");
                Assert.AreEqual(result.Authors, null);
            }
        }

        /// <summary>
        /// Tests the <see cref="BookServicesImplementation.GetBookById"/> method when the Book does not exist anymore.
        /// </summary>
        [TestMethod]
        public void TestGetBookByIdBookDoesNotExistAnymore()
        {
            using (this.mocks.Record())
            {
                Expect.Call(() => this.bookDataService.GetBookById(Arg<int>.Is.Anything)).WhenCalled(call =>
                {
                    int idParameter = (int)call.Arguments[0];
                    call.ReturnValue = this.books.FirstOrDefault(Book => Book.Id == idParameter);
                });
            }

            using (this.mocks.Playback())
            {
                // Arrange
                BookServicesImplementation servicesImplementation = new BookServicesImplementation(this.bookDataService, this.bookDomainDataService);
                int existingBookId = 1;
                this.books.Clear();

                // Act
                Book result = servicesImplementation.GetBookById(existingBookId);

                // Assert
                Assert.IsNull(result);
            }
        }

        /// <summary>
        /// Tests the <see cref="BookServicesImplementation.GetBookById"/> method when the Book does not exist.
        /// </summary>
        [TestMethod]
        public void TestGetBookByIdBookDoesNotExist()
        {
            using (this.mocks.Record())
            {
                Expect.Call(() => this.bookDataService.GetBookById(Arg<int>.Is.Anything)).WhenCalled(call =>
                {
                    int idParameter = (int)call.Arguments[0];
                    call.ReturnValue = this.books.FirstOrDefault(Book => Book.Id == idParameter);
                });
            }

            using (this.mocks.Playback())
            {
                // Arrange
                BookServicesImplementation servicesImplementation = new BookServicesImplementation(this.bookDataService, this.bookDomainDataService);
                int nonExistingBookId = 99;

                // Act
                Book result = servicesImplementation.GetBookById(nonExistingBookId);

                // Assert
                Assert.IsNull(result);
            }
        }

        /// <summary>
        /// Tests the <see cref="BookServicesImplementation.AddBook"/> method when there are items.
        /// </summary>
        [TestMethod]
        public void TestAddBooksHasItems()
        {
            using (this.mocks.Record())
            {
                Expect.Call(() => this.bookDomainDataService.GetBookDomainById(Arg<int>.Is.Anything)).WhenCalled(call =>
                {
                    int idParameter = (int)call.Arguments[0];
                    call.ReturnValue = this.bookDomains.FirstOrDefault(BookDomain => BookDomain.Id == idParameter);
                }).Repeat.Any();

                Expect.Call(() => this.bookDataService.AddBook(Arg<Book>.Is.Anything)).WhenCalled(call =>
                {
                    Book authorParameter = (Book)call.Arguments[0];
                    this.books.Add(authorParameter);
                });
            }

            using (this.mocks.Playback())
            {
                // Arrange
                BookServicesImplementation servicesImplementation = new BookServicesImplementation(this.bookDataService, this.bookDomainDataService);
                Book book = new Book { Id = 10, Title = "title", BookDomains = new List<BookDomain> { this.bookDomains[0] }, Editions = new List<Edition>() };

                // Act
                servicesImplementation.AddBook(book);

                // Assert
                Assert.IsNotNull(this.books.First(a => a.Id == book.Id));
                Assert.AreEqual(this.books.Count, 4);
            }
        }

        /// <summary>
        /// Tests the <see cref="BookServicesImplementation.AddBook"/> method when there are 0 items initially.
        /// </summary>
        [TestMethod]
        public void TestAddBooksHasNoItems()
        {
            using (this.mocks.Record())
            {
                Expect.Call(() => this.bookDomainDataService.GetBookDomainById(Arg<int>.Is.Anything)).WhenCalled(call =>
                {
                    int idParameter = (int)call.Arguments[0];
                    call.ReturnValue = this.bookDomains.FirstOrDefault(BookDomain => BookDomain.Id == idParameter);
                }).Repeat.Any();

                Expect.Call(() => this.bookDataService.AddBook(Arg<Book>.Is.Anything)).WhenCalled(call =>
                {
                    Book authorParameter = (Book)call.Arguments[0];
                    this.books.Add(authorParameter);
                });
            }

            using (this.mocks.Playback())
            {
                // Arrange
                BookServicesImplementation servicesImplementation = new BookServicesImplementation(this.bookDataService, this.bookDomainDataService);
                Book book = new Book { Id = 10, Title = "title", BookDomains = new List<BookDomain> { this.bookDomains[0] }, Editions = new List<Edition>() };
                this.books.Clear();

                // Act
                servicesImplementation.AddBook(book);

                // Assert
                Assert.IsNotNull(this.books.First(a => a.Id == book.Id));
                Assert.AreEqual(this.books.Count, 1);
            }
        }

        /// <summary>
        /// Tests the <see cref="BookServicesImplementation.DeleteBook"/> method when the Book exists.
        /// </summary>
        [TestMethod]
        public void TestDeleteBookBookExists()
        {
            using (this.mocks.Record())
            {
                Expect.Call(() => this.bookDataService.DeleteBook(Arg<Book>.Is.Anything)).WhenCalled(call =>
                {
                    Book authorParameter = (Book)call.Arguments[0];
                    int index = this.books.FindIndex(a => a.Id == authorParameter.Id);
                    if (index == -1)
                    {
                        throw new Exception();
                    }

                    this.books.RemoveAt(index);
                });
            }

            using (this.mocks.Playback())
            {
                // Arrange
                BookServicesImplementation servicesImplementation = new BookServicesImplementation(this.bookDataService, this.bookDomainDataService);
                Book authorToDelete = this.books.First(); // Select the first Book for deletion

                // Act
                servicesImplementation.DeleteBook(authorToDelete);

                // Assert
                Assert.AreEqual(this.books.Count, 2); // Book should be removed
                Assert.IsFalse(this.books.Contains(authorToDelete));
            }
        }

        /// <summary>
        /// Tests the <see cref="BookServicesImplementation.DeleteBook"/> method when the Book does not exist.
        /// </summary>
        [TestMethod]
        public void TestDeleteBookBookDoesNotExist()
        {
            using (this.mocks.Record())
            {
                Expect.Call(() => this.bookDataService.DeleteBook(Arg<Book>.Is.Anything)).WhenCalled(call =>
                {
                    Book authorParameter = (Book)call.Arguments[0];
                    int index = this.books.FindIndex(a => a.Id == authorParameter.Id);
                    if (index == -1)
                    {
                        throw new Exception();
                    }

                    this.books.RemoveAt(index);
                });
            }

            using (this.mocks.Playback())
            {
                // Arrange
                BookServicesImplementation servicesImplementation = new BookServicesImplementation(this.bookDataService, this.bookDomainDataService);
                Book nonExistingBook = new Book { Id = 99, Title = "NonExistingBook", BookDomains = new List<BookDomain> { this.bookDomains[0] }, Editions = new List<Edition>() };

                // Assert and Act
                Assert.ThrowsException<Exception>(() => servicesImplementation.DeleteBook(nonExistingBook));
            }
        }

        /// <summary>
        /// Tests the <see cref="BookServicesImplementation.UpdateBook"/> method when the Book exists.
        /// </summary>
        [TestMethod]
        public void TestUpdateBookBookExists()
        {
            using (this.mocks.Record())
            {
                Expect.Call(() => this.bookDomainDataService.GetBookDomainById(Arg<int>.Is.Anything)).WhenCalled(call =>
                {
                    int idParameter = (int)call.Arguments[0];
                    call.ReturnValue = this.bookDomains.FirstOrDefault(BookDomain => BookDomain.Id == idParameter);
                }).Repeat.Any();

                Expect.Call(() => this.bookDataService.UpdateBook(Arg<Book>.Is.Anything)).WhenCalled(call =>
                {
                    Book authorParameter = (Book)call.Arguments[0];
                    int index = this.books.FindIndex(a => a.Id == authorParameter.Id);
                    if (index == -1)
                    {
                        throw new Exception("Book not found");
                    }

                    books[index] = authorParameter;
                });
            }

            using (this.mocks.Playback())
            {
                // Arrange
                BookServicesImplementation servicesImplementation = new BookServicesImplementation(this.bookDataService, this.bookDomainDataService);
                Book book = this.books.First(); // Select the first Book for updating

                // Act
                servicesImplementation.UpdateBook(book);

                // Assert
                Assert.IsTrue(this.books.Contains(book));
            }
        }

        /// <summary>
        /// Tests the <see cref="BookServicesImplementation.UpdateBook"/> method when the Book does not exist.
        /// </summary>
        [TestMethod]
        public void TestUpdateBookBookDoesNotExist()
        {
            using (this.mocks.Record())
            {
                Expect.Call(() => this.bookDomainDataService.GetBookDomainById(Arg<int>.Is.Anything)).WhenCalled(call =>
                {
                    int idParameter = (int)call.Arguments[0];
                    call.ReturnValue = this.bookDomains.FirstOrDefault(BookDomain => BookDomain.Id == idParameter);
                }).Repeat.Any();

                Expect.Call(() => this.bookDataService.UpdateBook(Arg<Book>.Is.Anything)).WhenCalled(call =>
                {
                    Book authorParameter = (Book)call.Arguments[0];
                    int index = this.books.FindIndex(a => a.Id == authorParameter.Id);
                    if (index == -1)
                    {
                        throw new Exception("Book not found");
                    }

                    books[index] = authorParameter;
                });
            }

            using (this.mocks.Playback())
            {
                // Arrange
                BookServicesImplementation servicesImplementation = new BookServicesImplementation(this.bookDataService, this.bookDomainDataService);
                Book nonExistingBook = new Book { Id = 99, Title = "NonExistingBook", BookDomains = new List<BookDomain> { this.bookDomains[0] }, Editions = new List<Edition>() };

                // Assert and Act
                Assert.ThrowsException<Exception>(() => servicesImplementation.UpdateBook(nonExistingBook));
            }
        }

        /// <summary>
        /// Tests the <see cref="BookServicesImplementation.AddBook"/> method to ensure that a <see cref="ValidationException"/> is thrown
        /// when attempting to add a book with domains having the same root domain.
        /// </summary>
        [TestMethod]
        public void TestBookCanNotHaveDomeninsWithSameDomainRootOnAdd()
        {
            using (this.mocks.Record())
            {
                Expect.Call(() => this.bookDomainDataService.GetBookDomainById(Arg<int>.Is.Anything)).WhenCalled(call =>
                {
                    int idParameter = (int)call.Arguments[0];
                    call.ReturnValue = this.bookDomains.FirstOrDefault(BookDomain => BookDomain.Id == idParameter);
                }).Repeat.Any();

                Expect.Call(() => this.bookDataService.AddBook(Arg<Book>.Is.Anything)).WhenCalled(call =>
                {
                    Book authorParameter = (Book)call.Arguments[0];
                    this.books.Add(authorParameter);
                }).Repeat.Any();
            }

            using (this.mocks.Playback())
            {
                // Arrange
                BookServicesImplementation servicesImplementation = new BookServicesImplementation(this.bookDataService, this.bookDomainDataService);
                Book book = new Book { Id = 10, Title = "title", BookDomains = new List<BookDomain> { this.bookDomains[0], this.bookDomains[2] } };

                // Act and Assert
                Assert.ThrowsException<ValidationException>(() => servicesImplementation.AddBook(book), "The book domains cannot have the same root domain");
            }
        }

        /// <summary>
        /// Tests the <see cref="BookServicesImplementation.AddBook"/> method to ensure that a <see cref="ValidationException"/> is thrown
        /// when attempting to add a book with too many domains.
        /// </summary>
        [TestMethod]
        public void TestBookCanNotHaveTooManyDomainsOnAdd()
        {
            using (this.mocks.Record())
            {
                Expect.Call(() => this.bookDomainDataService.GetBookDomainById(Arg<int>.Is.Anything)).WhenCalled(call =>
                {
                    int idParameter = (int)call.Arguments[0];
                    call.ReturnValue = this.bookDomains.FirstOrDefault(BookDomain => BookDomain.Id == idParameter);
                }).Repeat.Any();

                Expect.Call(() => this.bookDataService.AddBook(Arg<Book>.Is.Anything)).WhenCalled(call =>
                {
                    Book authorParameter = (Book)call.Arguments[0];
                    this.books.Add(authorParameter);
                }).Repeat.Any();
            }

            using (this.mocks.Playback())
            {
                // Arrange
                BookServicesImplementation servicesImplementation = new BookServicesImplementation(this.bookDataService, this.bookDomainDataService);
                Book book = new Book { Id = 10, Title = "title", BookDomains = new List<BookDomain> { this.bookDomains[0], this.bookDomains[1], new BookDomain { Id = 4, Name = "asd" }, new BookDomain { Id = 5, Name = "asdewe" } } };
                int maxDomainCount = Convert.ToInt32(ConfigurationManager.AppSettings["DOMENII"]);

                // Act and Assert
                Assert.ThrowsException<ValidationException>(() => servicesImplementation.AddBook(book), $"A Book cannot have more than {maxDomainCount} BookDomains");
            }
        }

        /// <summary>
        /// Tests the <see cref="BookServicesImplementation.UpdateBook"/> method to ensure that a <see cref="ValidationException"/> is thrown
        /// when attempting to update a book with domains having the same root domain.
        /// </summary>
        [TestMethod]
        public void TestBookCanNotHaveDomeninsWithSameDomainRootOnUpdate()
        {
            using (this.mocks.Record())
            {
                Expect.Call(() => this.bookDomainDataService.GetBookDomainById(Arg<int>.Is.Anything)).WhenCalled(call =>
                {
                    int idParameter = (int)call.Arguments[0];
                    call.ReturnValue = this.bookDomains.FirstOrDefault(BookDomain => BookDomain.Id == idParameter);
                }).Repeat.Any();

                Expect.Call(() => this.bookDataService.UpdateBook(Arg<Book>.Is.Anything)).WhenCalled(call =>
                {
                    Book authorParameter = (Book)call.Arguments[0];
                    int index = this.books.FindIndex(a => a.Id == authorParameter.Id);
                    if (index == -1)
                    {
                        throw new Exception("Book not found");
                    }

                    books[index] = authorParameter;
                }).Repeat.Any();
            }

            using (this.mocks.Playback())
            {
                // Arrange
                BookServicesImplementation servicesImplementation = new BookServicesImplementation(this.bookDataService, this.bookDomainDataService);
                Book book = new Book { Id = 1, Title = "title", BookDomains = new List<BookDomain> { this.bookDomains[0], this.bookDomains[2] } };

                // Act and Assert
                Assert.ThrowsException<ValidationException>(() => servicesImplementation.UpdateBook(book), "The book domains cannot have the same root domain");
            }
        }

        /// <summary>
        /// Tests the <see cref="BookServicesImplementation.UpdateBook"/> method to ensure that a <see cref="ValidationException"/> is thrown
        /// when attempting to update a book with too many domains.
        /// </summary>
        [TestMethod]
        public void TestBookCanNotHaveTooManyDomainsOnUpdate()
        {
            using (this.mocks.Record())
            {
                Expect.Call(() => this.bookDomainDataService.GetBookDomainById(Arg<int>.Is.Anything)).WhenCalled(call =>
                {
                    int idParameter = (int)call.Arguments[0];
                    call.ReturnValue = this.bookDomains.FirstOrDefault(BookDomain => BookDomain.Id == idParameter);
                }).Repeat.Any();

                Expect.Call(() => this.bookDataService.UpdateBook(Arg<Book>.Is.Anything)).WhenCalled(call =>
                {
                    Book authorParameter = (Book)call.Arguments[0];
                    int index = this.books.FindIndex(a => a.Id == authorParameter.Id);
                    if (index == -1)
                    {
                        throw new Exception("Book not found");
                    }

                    books[index] = authorParameter;
                }).Repeat.Any();
            }

            using (this.mocks.Playback())
            {
                // Arrange
                BookServicesImplementation servicesImplementation = new BookServicesImplementation(this.bookDataService, this.bookDomainDataService);
                Book book = new Book { Id = 1, Title = "title", BookDomains = new List<BookDomain> { this.bookDomains[0], this.bookDomains[1], new BookDomain { Id = 4, Name = "asd" }, new BookDomain { Id = 5, Name = "asdewe" } } };
                int maxDomainCount = Convert.ToInt32(ConfigurationManager.AppSettings["DOMENII"]);

                // Act and Assert
                Assert.ThrowsException<ValidationException>(() => servicesImplementation.UpdateBook(book), $"A Book cannot have more than {maxDomainCount} BookDomains");
            }
        }
    }
}