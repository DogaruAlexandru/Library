// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuthorServicesTests.cs" company="Transilvania University of Brasov">
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
    /// Represents unit tests for the <see cref="AuthorServicesImplementation"/> class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class AuthorServicesTests
    {
        /// <summary>
        /// Represents the mock repository used for creating mock objects during unit tests.
        /// </summary>
        private MockRepository mocks;

        /// <summary>
        /// Represents the mock service for accessing author-related data during unit tests.
        /// </summary>
        private IAuthorDataService authorDataService;

        /// <summary>
        /// Represents a collection of authors in the application.
        /// </summary>
        private List<Author> authors;

        /// <summary>
        /// Initializes test resources before each test method is executed.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            this.mocks = new MockRepository();
            this.authorDataService = this.mocks.StrictMock<IAuthorDataService>();
            this.authors = new List<Author>
            {
                new Author { Id = 0, Name = "name1" },
                new Author { Id = 1, Name = "name2" },
                new Author { Id = 2, Name = "name3" },
            };
        }

        /// <summary>
        /// Unit test for the GetAllAuthors method of the AuthorServicesImplementation class,
        /// validating the retrieval of all authors from a database.
        /// </summary>
        [TestMethod]
        public void TestGetAllAuthorsFromDB()
        {
            // Arrange
            AuthorServicesImplementation servicesImplementation = new AuthorServicesImplementation(new SQLAuthorDataService());

            // Act
            var list = servicesImplementation.GetAllAuthors();

            // Assert
            Assert.AreEqual(list.Count, 8);
            Assert.AreEqual(list[6].Name, "SIU");
        }

        /// <summary>
        /// Tests the <see cref="AuthorServicesImplementation.GetAllAuthors"/> method when there are items.
        /// </summary>
        [TestMethod]
        public void TestGetAllAuthorsHasItems()
        {
            using (this.mocks.Record())
            {
                Expect.Call(this.authorDataService.GetAllAuthors()).Return(this.authors);
            }

            using (this.mocks.Playback())
            {
                // Arrange
                AuthorServicesImplementation servicesImplementation = new AuthorServicesImplementation(this.authorDataService);

                // Act
                var list = servicesImplementation.GetAllAuthors();

                // Assert
                Assert.AreEqual(list[0].Name, "name1");
                Assert.AreEqual(list.Count, 3);
            }
        }

        /// <summary>
        /// Tests the <see cref="AuthorServicesImplementation.GetAllAuthors"/> method when there are no items.
        /// </summary>
        [TestMethod]
        public void TestGetAllAuthorsHasNoItems()
        {
            using (this.mocks.Record())
            {
                Expect.Call(this.authorDataService.GetAllAuthors()).Return(this.authors);
            }

            using (this.mocks.Playback())
            {
                // Arrange
                AuthorServicesImplementation servicesImplementation = new AuthorServicesImplementation(this.authorDataService);
                this.authors.Clear();

                // Act
                var list = servicesImplementation.GetAllAuthors();

                // Assert
                Assert.AreEqual(list.Count, 0);
            }
        }

        /// <summary>
        /// Tests the <see cref="AuthorServicesImplementation.GetAuthorById"/> method when the author exists.
        /// </summary>
        [TestMethod]
        public void TestGetAuthorByIdAuthorExists()
        {
            using (this.mocks.Record())
            {
                Expect.Call(() => this.authorDataService.GetAuthorById(Arg<int>.Is.Anything)).WhenCalled(call =>
                {
                    int idParameter = (int)call.Arguments[0];
                    call.ReturnValue = this.authors.FirstOrDefault(author => author.Id == idParameter);
                });
            }

            using (this.mocks.Playback())
            {
                // Arrange
                AuthorServicesImplementation servicesImplementation = new AuthorServicesImplementation(this.authorDataService);
                int existingAuthorId = 1;

                // Act
                Author result = servicesImplementation.GetAuthorById(existingAuthorId);

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(result.Id, existingAuthorId);
                Assert.AreEqual(result.Name, "name2");
                Assert.AreEqual(result.Books, null);
            }
        }

        /// <summary>
        /// Tests the <see cref="AuthorServicesImplementation.GetAuthorById"/> method when the author does not exist anymore.
        /// </summary>
        [TestMethod]
        public void TestGetAuthorByIdAuthorDoesNotExistAnymore()
        {
            using (this.mocks.Record())
            {
                Expect.Call(() => this.authorDataService.GetAuthorById(Arg<int>.Is.Anything)).WhenCalled(call =>
                {
                    int idParameter = (int)call.Arguments[0];
                    call.ReturnValue = this.authors.FirstOrDefault(author => author.Id == idParameter);
                });
            }

            using (this.mocks.Playback())
            {
                // Arrange
                AuthorServicesImplementation servicesImplementation = new AuthorServicesImplementation(this.authorDataService);
                int existingAuthorId = 1;
                this.authors.Clear();

                // Act
                Author result = servicesImplementation.GetAuthorById(existingAuthorId);

                // Assert
                Assert.IsNull(result);
            }
        }

        /// <summary>
        /// Tests the <see cref="AuthorServicesImplementation.GetAuthorById"/> method when the author does not exist.
        /// </summary>
        [TestMethod]
        public void TestGetAuthorByIdAuthorDoesNotExist()
        {
            using (this.mocks.Record())
            {
                Expect.Call(() => this.authorDataService.GetAuthorById(Arg<int>.Is.Anything)).WhenCalled(call =>
                {
                    int idParameter = (int)call.Arguments[0];
                    call.ReturnValue = this.authors.FirstOrDefault(author => author.Id == idParameter);
                });
            }

            using (this.mocks.Playback())
            {
                // Arrange
                AuthorServicesImplementation servicesImplementation = new AuthorServicesImplementation(this.authorDataService);
                int nonExistingAuthorId = 99;

                // Act
                Author result = servicesImplementation.GetAuthorById(nonExistingAuthorId);

                // Assert
                Assert.IsNull(result);
            }
        }

        /// <summary>
        /// Tests the <see cref="AuthorServicesImplementation.AddAuthor"/> method when there are items.
        /// </summary>
        [TestMethod]
        public void TestAddAuthorsHasItems()
        {
            using (this.mocks.Record())
            {
                Expect.Call(() => this.authorDataService.AddAuthor(Arg<Author>.Is.Anything)).WhenCalled(call =>
                {
                    Author authorParameter = (Author)call.Arguments[0];
                    this.authors.Add(authorParameter);
                });
            }

            using (this.mocks.Playback())
            {
                // Arrange
                AuthorServicesImplementation servicesImplementation = new AuthorServicesImplementation(this.authorDataService);
                Author author = new Author { Id = 10, Name = "name" };

                // Act
                servicesImplementation.AddAuthor(author);

                // Assert
                Assert.IsNotNull(this.authors.First(a => a.Id == author.Id));
                Assert.AreEqual(this.authors.Count, 4);
            }
        }

        /// <summary>
        /// Tests the <see cref="AuthorServicesImplementation.AddAuthor"/> method when there are 0 items initially.
        /// </summary>
        [TestMethod]
        public void TestAddAuthorsHasNoItems()
        {
            using (this.mocks.Record())
            {
                Expect.Call(() => this.authorDataService.AddAuthor(Arg<Author>.Is.Anything)).WhenCalled(call =>
                {
                    Author authorParameter = (Author)call.Arguments[0];
                    this.authors.Add(authorParameter);
                });
            }

            using (this.mocks.Playback())
            {
                // Arrange
                AuthorServicesImplementation servicesImplementation = new AuthorServicesImplementation(this.authorDataService);
                Author author = new Author { Id = 10, Name = "name" };
                this.authors.Clear();

                // Act
                servicesImplementation.AddAuthor(author);

                // Assert
                Assert.IsNotNull(this.authors.First(a => a.Id == author.Id));
                Assert.AreEqual(this.authors.Count, 1);
            }
        }

        /// <summary>
        /// Tests the <see cref="AuthorServicesImplementation.DeleteAuthor"/> method when the author exists.
        /// </summary>
        [TestMethod]
        public void TestDeleteAuthorAuthorExists()
        {
            using (this.mocks.Record())
            {
                Expect.Call(() => this.authorDataService.DeleteAuthor(Arg<Author>.Is.Anything)).WhenCalled(call =>
                {
                    Author authorParameter = (Author)call.Arguments[0];
                    int index = this.authors.FindIndex(a => a.Id == authorParameter.Id);
                    if (index == -1)
                    {
                        throw new Exception();
                    }

                    this.authors.RemoveAt(index);
                });
            }

            using (this.mocks.Playback())
            {
                // Arrange
                AuthorServicesImplementation servicesImplementation = new AuthorServicesImplementation(this.authorDataService);
                Author authorToDelete = this.authors.First(); // Select the first author for deletion

                // Act
                servicesImplementation.DeleteAuthor(authorToDelete);

                // Assert
                Assert.AreEqual(this.authors.Count, 2); // Author should be removed
                Assert.IsFalse(this.authors.Contains(authorToDelete));
            }
        }

        /// <summary>
        /// Tests the <see cref="AuthorServicesImplementation.DeleteAuthor"/> method when the author does not exist.
        /// </summary>
        [TestMethod]
        public void TestDeleteAuthorAuthorDoesNotExist()
        {
            using (this.mocks.Record())
            {
                Expect.Call(() => this.authorDataService.DeleteAuthor(Arg<Author>.Is.Anything)).WhenCalled(call =>
                {
                    Author authorParameter = (Author)call.Arguments[0];
                    int index = this.authors.FindIndex(a => a.Id == authorParameter.Id);
                    if (index == -1)
                    {
                        throw new Exception();
                    }

                    this.authors.RemoveAt(index);
                });
            }

            using (this.mocks.Playback())
            {
                // Arrange
                AuthorServicesImplementation servicesImplementation = new AuthorServicesImplementation(this.authorDataService);
                Author nonExistingAuthor = new Author { Id = 99, Name = "NonExistingAuthor" };

                // Assert and Act
                Assert.ThrowsException<Exception>(() => servicesImplementation.DeleteAuthor(nonExistingAuthor));
            }
        }

        /// <summary>
        /// Tests the <see cref="AuthorServicesImplementation.UpdateAuthor"/> method when the author exists.
        /// </summary>
        [TestMethod]
        public void TestUpdateAuthorAuthorExists()
        {
            using (this.mocks.Record())
            {
                Expect.Call(() => this.authorDataService.UpdateAuthor(Arg<Author>.Is.Anything)).WhenCalled(call =>
                {
                    Author authorParameter = (Author)call.Arguments[0];
                    int index = this.authors.FindIndex(a => a.Id == authorParameter.Id);
                    if (index == -1)
                    {
                        throw new Exception("Author not found");
                    }

                    this.authors[index] = authorParameter;
                });
            }

            using (this.mocks.Playback())
            {
                // Arrange
                AuthorServicesImplementation servicesImplementation = new AuthorServicesImplementation(this.authorDataService);
                Author authorToUpdate = this.authors.First(); // Select the first author for updating

                // Act
                servicesImplementation.UpdateAuthor(authorToUpdate);

                // Assert
                Assert.IsTrue(this.authors.Contains(authorToUpdate));
            }
        }

        /// <summary>
        /// Tests the <see cref="AuthorServicesImplementation.UpdateAuthor"/> method when the author does not exist.
        /// </summary>
        [TestMethod]
        public void TestUpdateAuthorAuthorDoesNotExist()
        {
            using (this.mocks.Record())
            {
                Expect.Call(() => this.authorDataService.UpdateAuthor(Arg<Author>.Is.Anything)).WhenCalled(call =>
                {
                    Author authorParameter = (Author)call.Arguments[0];
                    int index = this.authors.FindIndex(a => a.Id == authorParameter.Id);
                    if (index == -1)
                    {
                        throw new Exception("Author not found");
                    }

                    this.authors[index] = authorParameter;
                });
            }

            using (this.mocks.Playback())
            {
                // Arrange
                AuthorServicesImplementation servicesImplementation = new AuthorServicesImplementation(this.authorDataService);
                Author nonExistingAuthor = new Author { Id = 99, Name = "NonExistingAuthor" };

                // Assert and Act
                Assert.ThrowsException<Exception>(() => servicesImplementation.UpdateAuthor(nonExistingAuthor));
            }
        }
    }
}
