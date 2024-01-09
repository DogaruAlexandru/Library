// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EditionServiceTests.cs" company="Transilvania University of Brasov">
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
    /// Represents unit tests for the <see cref="EditionServiceImplementation"/> class.
    /// </summary>
    [TestClass]
    public class EditionServiceTests
    {
        /// <summary>
        /// Represents the mock repository used for creating mock objects during unit tests.
        /// </summary>
        private MockRepository mocks;

        /// <summary>
        /// Represents the mock service for accessing edition-related data during unit tests.
        /// </summary>
        private IEditionDataService editionDataService;

        /// <summary>
        /// Represents a collection of editions in the application.
        /// </summary>
        private List<Edition> editions;

        /// <summary>
        /// Initializes test resources before each test method is executed.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            this.mocks = new MockRepository();
            this.editionDataService = this.mocks.StrictMock<IEditionDataService>();
            Book book = new Book { Id = 1, Title = "title2", BookDomains = new List<BookDomain> { new BookDomain { Id = 1, Name = "name1" } } };
            this.editions = new List<Edition>
            {
                new Edition { Id = 0, Name = "name1", Book = book, Publisher = "asdasd", Type = BookType.LibraryBinding, PageCount = 123, CanBorrow = 23, CanNotBorrow = 23 },
                new Edition { Id = 1, Name = "name2", Book = book, Publisher = "asd aasda", Type = BookType.DustJacket,  PageCount = 232, CanBorrow = 2, CanNotBorrow = 23 },
                new Edition { Id = 2, Name = "name3", Book = book, Publisher = "asdasd", Type = BookType.TradeHardcover, PageCount = 431, CanBorrow = 0, CanNotBorrow = 2 }
            };
        }

        /// <summary>
        /// Tests the <see cref="EditionServiceImplementation.GetAllEditions"/> method when there are items.
        /// </summary>
        [TestMethod]
        public void TestGetAllEditionsHasItems()
        {
            using (this.mocks.Record())
            {
                Expect.Call(this.editionDataService.GetAllEditions()).Return(this.editions);
            }

            using (this.mocks.Playback())
            {
                // Arrange
                EditionServiceImplementation servicesImplementation = new EditionServiceImplementation(this.editionDataService);

                // Act
                var list = servicesImplementation.GetAllEditions();

                // Assert
                Assert.AreEqual(list[0].Name, "name1");
                Assert.AreEqual(list.Count, 3);
            }
        }

        /// <summary>
        /// Tests the <see cref="EditionServiceImplementation.GetAllEditions"/> method when there are no items.
        /// </summary>
        [TestMethod]
        public void TestGetAllEditionsHasNoItems()
        {
            using (this.mocks.Record())
            {
                Expect.Call(this.editionDataService.GetAllEditions()).Return(this.editions);
            }

            using (this.mocks.Playback())
            {
                // Arrange
                EditionServiceImplementation servicesImplementation = new EditionServiceImplementation(this.editionDataService);
                this.editions.Clear();

                // Act
                var list = servicesImplementation.GetAllEditions();

                // Assert
                Assert.AreEqual(list.Count, 0);
            }
        }

        /// <summary>
        /// Tests the <see cref="EditionServiceImplementation.GetEditionById"/> method when the edition exists.
        /// </summary>
        [TestMethod]
        public void TestGetEditionByIdEditionExists()
        {
            using (this.mocks.Record())
            {
                Expect.Call(() => this.editionDataService.GetEditionById(Arg<int>.Is.Anything)).WhenCalled(call =>
                {
                    int idParameter = (int)call.Arguments[0];
                    call.ReturnValue = this.editions.FirstOrDefault(edition => edition.Id == idParameter);
                });
            }

            using (this.mocks.Playback())
            {
                // Arrange
                EditionServiceImplementation servicesImplementation = new EditionServiceImplementation(this.editionDataService);
                int existingEditionId = 1;

                // Act
                Edition result = servicesImplementation.GetEditionById(existingEditionId);

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(result.Id, existingEditionId);
                Assert.AreEqual(result.Name, "name2");
            }
        }

        /// <summary>
        /// Tests the <see cref="EditionServiceImplementation.GetEditionById"/> method when the edition does not exist anymore.
        /// </summary>
        [TestMethod]
        public void TestGetEditionByIdEditionDoesNotExistAnymore()
        {
            using (this.mocks.Record())
            {
                Expect.Call(() => this.editionDataService.GetEditionById(Arg<int>.Is.Anything)).WhenCalled(call =>
                {
                    int idParameter = (int)call.Arguments[0];
                    call.ReturnValue = this.editions.FirstOrDefault(edition => edition.Id == idParameter);
                });
            }

            using (this.mocks.Playback())
            {
                // Arrange
                EditionServiceImplementation servicesImplementation = new EditionServiceImplementation(this.editionDataService);
                int existingEditionId = 1;
                this.editions.Clear();

                // Act
                Edition result = servicesImplementation.GetEditionById(existingEditionId);

                // Assert
                Assert.IsNull(result);
            }
        }

        /// <summary>
        /// Tests the <see cref="EditionServiceImplementation.GetEditionById"/> method when the edition does not exist.
        /// </summary>
        [TestMethod]
        public void TestGetEditionByIdEditionDoesNotExist()
        {
            using (this.mocks.Record())
            {
                Expect.Call(() => this.editionDataService.GetEditionById(Arg<int>.Is.Anything)).WhenCalled(call =>
                {
                    int idParameter = (int)call.Arguments[0];
                    call.ReturnValue = this.editions.FirstOrDefault(edition => edition.Id == idParameter);
                });
            }

            using (this.mocks.Playback())
            {
                // Arrange
                EditionServiceImplementation servicesImplementation = new EditionServiceImplementation(this.editionDataService);
                int nonExistingEditionId = 99;

                // Act
                Edition result = servicesImplementation.GetEditionById(nonExistingEditionId);

                // Assert
                Assert.IsNull(result);
            }
        }

        /// <summary>
        /// Tests the <see cref="EditionServiceImplementation.AddEdition"/> method when there are items.
        /// </summary>
        [TestMethod]
        public void TestAddEditionsHasItems()
        {
            using (this.mocks.Record())
            {
                Expect.Call(() => this.editionDataService.AddEdition(Arg<Edition>.Is.Anything)).WhenCalled(call =>
                {
                    Edition editionParameter = (Edition)call.Arguments[0];
                    this.editions.Add(editionParameter);
                });
            }

            using (this.mocks.Playback())
            {
                // Arrange
                EditionServiceImplementation servicesImplementation = new EditionServiceImplementation(this.editionDataService);
                Edition edition = new Edition { Id = 10, Name = "name" };

                // Act
                servicesImplementation.AddEdition(edition);

                // Assert
                Assert.IsNotNull(this.editions.First(a => a.Id == edition.Id));
                Assert.AreEqual(this.editions.Count, 4);
            }
        }

        /// <summary>
        /// Tests the <see cref="EditionServiceImplementation.AddEdition"/> method when there are 0 items initially.
        /// </summary>
        [TestMethod]
        public void TestAddEditionsHasNoItems()
        {
            using (this.mocks.Record())
            {
                Expect.Call(() => this.editionDataService.AddEdition(Arg<Edition>.Is.Anything)).WhenCalled(call =>
                {
                    Edition editionParameter = (Edition)call.Arguments[0];
                    this.editions.Add(editionParameter);
                });
            }

            using (this.mocks.Playback())
            {
                // Arrange
                EditionServiceImplementation servicesImplementation = new EditionServiceImplementation(this.editionDataService);
                Edition edition = new Edition { Id = 10, Name = "name" };
                this.editions.Clear();

                // Act
                servicesImplementation.AddEdition(edition);

                // Assert
                Assert.IsNotNull(this.editions.First(a => a.Id == edition.Id));
                Assert.AreEqual(this.editions.Count, 1);
            }
        }

        /// <summary>
        /// Tests the <see cref="EditionServiceImplementation.DeleteEdition"/> method when the edition exists.
        /// </summary>
        [TestMethod]
        public void TestDeleteEditionEditionExists()
        {
            using (this.mocks.Record())
            {
                Expect.Call(() => this.editionDataService.DeleteEdition(Arg<Edition>.Is.Anything)).WhenCalled(call =>
                {
                    Edition editionParameter = (Edition)call.Arguments[0];
                    int index = this.editions.FindIndex(a => a.Id == editionParameter.Id);
                    if (index == -1)
                    {
                        throw new Exception();
                    }

                    this.editions.RemoveAt(index);
                });
            }

            using (this.mocks.Playback())
            {
                // Arrange
                EditionServiceImplementation servicesImplementation = new EditionServiceImplementation(this.editionDataService);
                Edition editionToDelete = this.editions.First(); // Select the first edition for deletion

                // Act
                servicesImplementation.DeleteEdition(editionToDelete);

                // Assert
                Assert.AreEqual(this.editions.Count, 2); // Edition should be removed
                Assert.IsFalse(this.editions.Contains(editionToDelete));
            }
        }

        /// <summary>
        /// Tests the <see cref="EditionServiceImplementation.DeleteEdition"/> method when the edition does not exist.
        /// </summary>
        [TestMethod]
        public void TestDeleteEditionEditionDoesNotExist()
        {
            using (this.mocks.Record())
            {
                Expect.Call(() => this.editionDataService.DeleteEdition(Arg<Edition>.Is.Anything)).WhenCalled(call =>
                {
                    Edition editionParameter = (Edition)call.Arguments[0];
                    int index = this.editions.FindIndex(a => a.Id == editionParameter.Id);
                    if (index == -1)
                    {
                        throw new Exception();
                    }

                    this.editions.RemoveAt(index);
                });
            }

            using (this.mocks.Playback())
            {
                // Arrange
                EditionServiceImplementation servicesImplementation = new EditionServiceImplementation(this.editionDataService);
                Edition nonExistingEdition = new Edition { Id = 99, Name = "NonExistingEdition" };

                // Assert and Act
                Assert.ThrowsException<Exception>(() => servicesImplementation.DeleteEdition(nonExistingEdition));
            }
        }

        /// <summary>
        /// Tests the <see cref="EditionServiceImplementation.UpdateEdition"/> method when the edition exists.
        /// </summary>
        [TestMethod]
        public void TestUpdateEditionEditionExists()
        {
            using (this.mocks.Record())
            {
                Expect.Call(() => this.editionDataService.UpdateEdition(Arg<Edition>.Is.Anything)).WhenCalled(call =>
                {
                    Edition editionParameter = (Edition)call.Arguments[0];
                    int index = this.editions.FindIndex(a => a.Id == editionParameter.Id);
                    if (index == -1)
                    {
                        throw new Exception("Edition not found");
                    }

                    editions[index] = editionParameter;
                });
            }

            using (this.mocks.Playback())
            {
                // Arrange
                EditionServiceImplementation servicesImplementation = new EditionServiceImplementation(this.editionDataService);
                Edition editionToUpdate = this.editions.First(); // Select the first edition for updating

                // Act
                servicesImplementation.UpdateEdition(editionToUpdate);

                // Assert
                Assert.IsTrue(this.editions.Contains(editionToUpdate));
            }
        }

        /// <summary>
        /// Tests the <see cref="EditionServiceImplementation.UpdateEdition"/> method when the edition does not exist.
        /// </summary>
        [TestMethod]
        public void TestUpdateEditionEditionDoesNotExist()
        {
            using (this.mocks.Record())
            {
                Expect.Call(() => this.editionDataService.UpdateEdition(Arg<Edition>.Is.Anything)).WhenCalled(call =>
                {
                    Edition editionParameter = (Edition)call.Arguments[0];
                    int index = this.editions.FindIndex(a => a.Id == editionParameter.Id);
                    if (index == -1)
                    {
                        throw new Exception("Edition not found");
                    }

                    editions[index] = editionParameter;
                });
            }

            using (this.mocks.Playback())
            {
                // Arrange
                EditionServiceImplementation servicesImplementation = new EditionServiceImplementation(this.editionDataService);
                Edition nonExistingEdition = new Edition { Id = 99, Name = "NonExistingEdition" };

                // Assert and Act
                Assert.ThrowsException<Exception>(() => servicesImplementation.UpdateEdition(nonExistingEdition));
            }
        }
    }
}
