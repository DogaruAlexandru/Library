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

    [TestClass]
    public class AuthorServicesImplementationTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            MockRepository mocks = new MockRepository();
            IAuthorDataService authorDataService = mocks.StrictMock<IAuthorDataService>();

            using (mocks.Record())
            {
                Expect.Call(authorDataService.GetAllAuthors()).Return(new List<Author> { new Author { Id = 1, Name = "name" } });
            }

            using (mocks.Playback())
            {
                AuthorServicesImplementation servicesImplementation = new AuthorServicesImplementation(authorDataService);
                var list = servicesImplementation.GetAllAuthors();
                Assert.AreEqual(list[0].Name, "name");
            }
        }
    }
}
