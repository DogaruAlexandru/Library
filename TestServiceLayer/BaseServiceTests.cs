// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseServiceTests.cs" company="Transilvania University of Brasov">
//   Copyright (c) Dogaru Alexandru.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TestServiceLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
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
    /// Test class for the BaseService.
    /// </summary>
    [TestClass]
    public class BaseServiceTests
    {
        /// <summary>
        /// Tests the ValidateEntity method when validation fails.
        /// </summary>
        [TestMethod]
        public void TestValidateEntityFails()
        {
            var service = new AuthorServicesImplementation(null);
            Author author = new Author();

            Assert.ThrowsException<ValidationException>(() => service.ValidateEntity(author), "The Name cannot be null");
        }

        /// <summary>
        /// Tests the ValidateEntity method when validation succeeds.
        /// </summary>
        [TestMethod]
        public void TestValidateEntitySucceses()
        {
            var service = new AuthorServicesImplementation(null);
            Author author = new Author { Name = "name" };

            service.ValidateEntity(author);
        }
    }
}
