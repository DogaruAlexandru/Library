// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuthorServicesImplementationTests.cs" company="Transilvania University of Brasov">
//   Copyright (c) Dogaru Alexandru.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TestServiceLayer
{
    using System;
    using System.Collections.Generic;
    using DataMapper;
    using DomainModel;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Rhino.Mocks;
    using ServiceLayer.ServiceImplementation;
    using static log4net.Appender.RollingFileAppender;

    /// <summary>
    /// Represents unit tests for the <see cref="AuthorServicesImplementation"/> class.
    /// </summary>
    [TestClass]
    public class AuthorServicesImplementationTests
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
        /// Initializes test resources before each test method is executed.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            this.mocks = new MockRepository();
            this.authorDataService = this.mocks.StrictMock<IAuthorDataService>();

            using (this.mocks.Record())
            {
                Expect.Call(this.authorDataService.GetAllAuthors()).Return(new List<Author>
                {
                    new Author { Id = 1, Name = "name1" },
                    new Author { Id = 2, Name = "name2" }
                });
            }
        }

        /// <summary>
        /// Tests the <see cref="AuthorServicesImplementation.GetAllAuthors"/> method.
        /// </summary>
        [TestMethod]
        public void TestGetAllAuthors()
        {
            using (this.mocks.Playback())
            {
                AuthorServicesImplementation servicesImplementation = new AuthorServicesImplementation(this.authorDataService);
                var list = servicesImplementation.GetAllAuthors();
                Assert.AreEqual(list[0].Name, "name1");
                Assert.AreEqual(list.Count, 2);
            }
        }
    }
}
