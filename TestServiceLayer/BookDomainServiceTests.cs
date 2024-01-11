// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BookDomainServiceTests.cs" company="Transilvania University of Brasov">
//   Copyright (c) Dogaru Alexandru.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TestServiceLayer
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using DataMapper;
    using DomainModel;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Rhino.Mocks;
    using Rhino.Mocks.Constraints;
    using ServiceLayer.ServiceImplementation;
    using static log4net.Appender.RollingFileAppender;

    /// <summary>
    /// Represents unit tests for the <see cref="BookDomainServicesImplementation"/> class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class BookDomainServiceTests
    {
        /// <summary>
        /// Represents the mock repository used for creating mock objects during unit tests.
        /// </summary>
        private MockRepository mocks;

        /// <summary>
        /// Represents the mock service for accessing BookDomain-related data during unit tests.
        /// </summary>
        private IBookDomainDataService bookDomainDataService;

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
            this.bookDomainDataService = this.mocks.StrictMock<IBookDomainDataService>();
            this.bookDomains = new List<BookDomain>
            {
                new BookDomain { Id = 0, Name = "name1" },
                new BookDomain { Id = 1, Name = "name2" },
                new BookDomain { Id = 2, Name = "name3" }
            };
            this.bookDomains[2].ParentDomain = this.bookDomains[0];
        }

        /// <summary>
        /// Tests the <see cref="BookDomainServicesImplementation.GetAllBookDomains"/> method when there are items.
        /// </summary>
        [TestMethod]
        public void TestGetAllBookDomainsHasItems()
        {
            using (this.mocks.Record())
            {
                Expect.Call(this.bookDomainDataService.GetAllBookDomains()).Return(this.bookDomains);
            }

            using (this.mocks.Playback())
            {
                // Arrange
                BookDomainServicesImplementation servicesImplementation = new BookDomainServicesImplementation(this.bookDomainDataService);

                // Act
                var list = servicesImplementation.GetAllBookDomains();

                // Assert
                Assert.AreEqual(list[0].Name, "name1");
                Assert.AreEqual(list.Count, 3);
            }
        }

        /// <summary>
        /// Tests the <see cref="BookDomainServicesImplementation.GetAllBookDomains"/> method when there are no items.
        /// </summary>
        [TestMethod]
        public void TestGetAllBookDomainsHasNoItems()
        {
            using (this.mocks.Record())
            {
                Expect.Call(this.bookDomainDataService.GetAllBookDomains()).Return(this.bookDomains);
            }

            using (this.mocks.Playback())
            {
                // Arrange
                BookDomainServicesImplementation servicesImplementation = new BookDomainServicesImplementation(this.bookDomainDataService);
                this.bookDomains.Clear();

                // Act
                var list = servicesImplementation.GetAllBookDomains();

                // Assert
                Assert.AreEqual(list.Count, 0);
            }
        }

        /// <summary>
        /// Tests the <see cref="BookDomainServicesImplementation.GetBookDomainById"/> method when the BookDomain exists.
        /// </summary>
        [TestMethod]
        public void TestGetBookDomainByIdBookDomainExists()
        {
            using (this.mocks.Record())
            {
                Expect.Call(() => this.bookDomainDataService.GetBookDomainById(Arg<int>.Is.Anything)).WhenCalled(call =>
                {
                    int idParameter = (int)call.Arguments[0];
                    call.ReturnValue = this.bookDomains.FirstOrDefault(BookDomain => BookDomain.Id == idParameter);
                });
            }

            using (this.mocks.Playback())
            {
                // Arrange
                BookDomainServicesImplementation servicesImplementation = new BookDomainServicesImplementation(this.bookDomainDataService);
                int existingBookDomainId = 1;

                // Act
                BookDomain result = servicesImplementation.GetBookDomainById(existingBookDomainId);

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(result.Id, existingBookDomainId);
                Assert.AreEqual(result.Name, "name2");
                Assert.AreEqual(result.Books, null);
            }
        }

        /// <summary>
        /// Tests the <see cref="BookDomainServicesImplementation.GetBookDomainById"/> method when the BookDomain does not exist anymore.
        /// </summary>
        [TestMethod]
        public void TestGetBookDomainByIdBookDomainDoesNotExistAnymore()
        {
            using (this.mocks.Record())
            {
                Expect.Call(() => this.bookDomainDataService.GetBookDomainById(Arg<int>.Is.Anything)).WhenCalled(call =>
                {
                    int idParameter = (int)call.Arguments[0];
                    call.ReturnValue = this.bookDomains.FirstOrDefault(BookDomain => BookDomain.Id == idParameter);
                });
            }

            using (this.mocks.Playback())
            {
                // Arrange
                BookDomainServicesImplementation servicesImplementation = new BookDomainServicesImplementation(this.bookDomainDataService);
                int existingBookDomainId = 1;
                this.bookDomains.Clear();

                // Act
                BookDomain result = servicesImplementation.GetBookDomainById(existingBookDomainId);

                // Assert
                Assert.IsNull(result);
            }
        }

        /// <summary>
        /// Tests the <see cref="BookDomainServicesImplementation.GetBookDomainById"/> method when the BookDomain does not exist.
        /// </summary>
        [TestMethod]
        public void TestGetBookDomainByIdBookDomainDoesNotExist()
        {
            using (this.mocks.Record())
            {
                Expect.Call(() => this.bookDomainDataService.GetBookDomainById(Arg<int>.Is.Anything)).WhenCalled(call =>
                {
                    int idParameter = (int)call.Arguments[0];
                    call.ReturnValue = this.bookDomains.FirstOrDefault(BookDomain => BookDomain.Id == idParameter);
                });
            }

            using (this.mocks.Playback())
            {
                // Arrange
                BookDomainServicesImplementation servicesImplementation = new BookDomainServicesImplementation(this.bookDomainDataService);
                int nonExistingBookDomainId = 99;

                // Act
                BookDomain result = servicesImplementation.GetBookDomainById(nonExistingBookDomainId);

                // Assert
                Assert.IsNull(result);
            }
        }

        /// <summary>
        /// Tests the <see cref="BookDomainServicesImplementation.AddBookDomain"/> method when there are items.
        /// </summary>
        [TestMethod]
        public void TestAddBookDomainsHasItems()
        {
            using (this.mocks.Record())
            {
                Expect.Call(() => this.bookDomainDataService.AddBookDomain(Arg<BookDomain>.Is.Anything)).WhenCalled(call =>
                {
                    BookDomain authorParameter = (BookDomain)call.Arguments[0];
                    this.bookDomains.Add(authorParameter);
                });
            }

            using (this.mocks.Playback())
            {
                // Arrange
                BookDomainServicesImplementation servicesImplementation = new BookDomainServicesImplementation(this.bookDomainDataService);
                BookDomain bookDomain = new BookDomain { Id = 10, Name = "name" };

                // Act
                servicesImplementation.AddBookDomain(bookDomain);

                // Assert
                Assert.IsNotNull(this.bookDomains.First(a => a.Id == bookDomain.Id));
                Assert.AreEqual(this.bookDomains.Count, 4);
            }
        }

        /// <summary>
        /// Tests the <see cref="BookDomainServicesImplementation.AddBookDomain"/> method when there are 0 items initially.
        /// </summary>
        [TestMethod]
        public void TestAddBookDomainsHasNoItems()
        {
            using (this.mocks.Record())
            {
                Expect.Call(() => this.bookDomainDataService.AddBookDomain(Arg<BookDomain>.Is.Anything)).WhenCalled(call =>
                {
                    BookDomain authorParameter = (BookDomain)call.Arguments[0];
                    this.bookDomains.Add(authorParameter);
                });
            }

            using (this.mocks.Playback())
            {
                // Arrange
                BookDomainServicesImplementation servicesImplementation = new BookDomainServicesImplementation(this.bookDomainDataService);
                BookDomain bookDomain = new BookDomain { Id = 10, Name = "name" };
                this.bookDomains.Clear();

                // Act
                servicesImplementation.AddBookDomain(bookDomain);

                // Assert
                Assert.IsNotNull(this.bookDomains.First(a => a.Id == bookDomain.Id));
                Assert.AreEqual(this.bookDomains.Count, 1);
            }
        }

        /// <summary>
        /// Tests the <see cref="BookDomainServicesImplementation.DeleteBookDomain"/> method when the BookDomain exists.
        /// </summary>
        [TestMethod]
        public void TestDeleteBookDomainBookDomainExists()
        {
            using (this.mocks.Record())
            {
                Expect.Call(() => this.bookDomainDataService.DeleteBookDomain(Arg<BookDomain>.Is.Anything)).WhenCalled(call =>
                {
                    BookDomain authorParameter = (BookDomain)call.Arguments[0];
                    int index = this.bookDomains.FindIndex(a => a.Id == authorParameter.Id);
                    if (index == -1)
                    {
                        throw new Exception();
                    }

                    this.bookDomains.RemoveAt(index);
                });
            }

            using (this.mocks.Playback())
            {
                // Arrange
                BookDomainServicesImplementation servicesImplementation = new BookDomainServicesImplementation(this.bookDomainDataService);
                BookDomain authorToDelete = this.bookDomains.First(); // Select the first BookDomain for deletion

                // Act
                servicesImplementation.DeleteBookDomain(authorToDelete);

                // Assert
                Assert.AreEqual(this.bookDomains.Count, 2); // BookDomain should be removed
                Assert.IsFalse(this.bookDomains.Contains(authorToDelete));
            }
        }

        /// <summary>
        /// Tests the <see cref="BookDomainServicesImplementation.DeleteBookDomain"/> method when the BookDomain does not exist.
        /// </summary>
        [TestMethod]
        public void TestDeleteBookDomainBookDomainDoesNotExist()
        {
            using (this.mocks.Record())
            {
                Expect.Call(() => this.bookDomainDataService.DeleteBookDomain(Arg<BookDomain>.Is.Anything)).WhenCalled(call =>
                {
                    BookDomain authorParameter = (BookDomain)call.Arguments[0];
                    int index = this.bookDomains.FindIndex(a => a.Id == authorParameter.Id);
                    if (index == -1)
                    {
                        throw new Exception();
                    }

                    this.bookDomains.RemoveAt(index);
                });
            }

            using (this.mocks.Playback())
            {
                // Arrange
                BookDomainServicesImplementation servicesImplementation = new BookDomainServicesImplementation(this.bookDomainDataService);
                BookDomain nonExistingBookDomain = new BookDomain { Id = 99, Name = "NonExistingBookDomain" };

                // Assert and Act
                Assert.ThrowsException<Exception>(() => servicesImplementation.DeleteBookDomain(nonExistingBookDomain));
            }
        }

        /// <summary>
        /// Tests the <see cref="BookDomainServicesImplementation.UpdateBookDomain"/> method when the BookDomain exists.
        /// </summary>
        [TestMethod]
        public void TestUpdateBookDomainBookDomainExists()
        {
            using (this.mocks.Record())
            {
                Expect.Call(() => this.bookDomainDataService.UpdateBookDomain(Arg<BookDomain>.Is.Anything)).WhenCalled(call =>
                {
                    BookDomain authorParameter = (BookDomain)call.Arguments[0];
                    int index = this.bookDomains.FindIndex(a => a.Id == authorParameter.Id);
                    if (index == -1)
                    {
                        throw new Exception("BookDomain not found");
                    }

                    bookDomains[index] = authorParameter;
                });
            }

            using (this.mocks.Playback())
            {
                // Arrange
                BookDomainServicesImplementation servicesImplementation = new BookDomainServicesImplementation(this.bookDomainDataService);
                BookDomain authorToUpdate = this.bookDomains.First(); // Select the first BookDomain for updating

                // Act
                servicesImplementation.UpdateBookDomain(authorToUpdate);

                // Assert
                Assert.IsTrue(this.bookDomains.Contains(authorToUpdate));
            }
        }

        /// <summary>
        /// Tests the <see cref="BookDomainServicesImplementation.UpdateBookDomain"/> method when the BookDomain does not exist.
        /// </summary>
        [TestMethod]
        public void TestUpdateBookDomainBookDomainDoesNotExist()
        {
            using (this.mocks.Record())
            {
                Expect.Call(() => this.bookDomainDataService.UpdateBookDomain(Arg<BookDomain>.Is.Anything)).WhenCalled(call =>
                {
                    BookDomain authorParameter = (BookDomain)call.Arguments[0];
                    int index = this.bookDomains.FindIndex(a => a.Id == authorParameter.Id);
                    if (index == -1)
                    {
                        throw new Exception("BookDomain not found");
                    }

                    bookDomains[index] = authorParameter;
                });
            }

            using (this.mocks.Playback())
            {
                // Arrange
                BookDomainServicesImplementation servicesImplementation = new BookDomainServicesImplementation(this.bookDomainDataService);
                BookDomain nonExistingBookDomain = new BookDomain { Id = 99, Name = "NonExistingBookDomain" };

                // Assert and Act
                Assert.ThrowsException<Exception>(() => servicesImplementation.UpdateBookDomain(nonExistingBookDomain));
            }
        }
    }
}
