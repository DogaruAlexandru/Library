// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PersonServiceTests.cs" company="Transilvania University of Brasov">
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
    /// Represents unit tests for the <see cref="PersonServiceImplementation"/> class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class PersonServiceTests
    {
        /// <summary>
        /// Represents the mock repository used for creating mock objects during unit tests.
        /// </summary>
        private MockRepository mocks;

        /// <summary>
        /// Represents the mock service for accessing person-related data during unit tests.
        /// </summary>
        private IPersonDataService personDataService;

        /// <summary>
        /// Represents a collection of persons in the application.
        /// </summary>
        private List<Person> persons;

        /// <summary>
        /// Initializes test resources before each test method is executed.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            this.mocks = new MockRepository();
            this.personDataService = this.mocks.StrictMock<IPersonDataService>();
            this.persons = new List<Person>
            {
                new Person { Id = 0, FirstName = "FirstName1", LastName = "LastName1", CNP = "2491738465248", Type = PersonType.Reader, Address = "asdadsasdasdasd", EmailAddress = "asd1@mail.com" },
                new Person { Id = 1, FirstName = "FirstName2", LastName = "LastName2", CNP = "1461591791659", Type = PersonType.Reader, Address = "asdadsasdas dasd", EmailAddress = "assdd1@mail.com" },
                new Person { Id = 2, FirstName = "FirstName3", LastName = "LastName3", CNP = "5010474094949", Type = PersonType.LibraryPersonnel, Address = "asdad sasdasdasd", EmailAddress = "asdasda1@mail.com" },
            };
        }

        /// <summary>
        /// Tests the <see cref="PersonServiceImplementation.GetAllPersons"/> method when there are items.
        /// </summary>
        [TestMethod]
        public void TestGetAllPersonsHasItems()
        {
            using (this.mocks.Record())
            {
                Expect.Call(this.personDataService.GetAllPersons()).Return(this.persons);
            }

            using (this.mocks.Playback())
            {
                // Arrange
                PersonServiceImplementation servicesImplementation = new PersonServiceImplementation(this.personDataService);

                // Act
                var list = servicesImplementation.GetAllPersons();

                // Assert
                Assert.AreEqual(list[0].CNP, "2491738465248");
                Assert.AreEqual(list.Count, 3);
            }
        }

        /// <summary>
        /// Tests the <see cref="PersonServiceImplementation.GetAllPersons"/> method when there are no items.
        /// </summary>
        [TestMethod]
        public void TestGetAllPersonsHasNoItems()
        {
            using (this.mocks.Record())
            {
                Expect.Call(this.personDataService.GetAllPersons()).Return(this.persons);
            }

            using (this.mocks.Playback())
            {
                // Arrange
                PersonServiceImplementation servicesImplementation = new PersonServiceImplementation(this.personDataService);
                this.persons.Clear();

                // Act
                var list = servicesImplementation.GetAllPersons();

                // Assert
                Assert.AreEqual(list.Count, 0);
            }
        }

        /// <summary>
        /// Tests the <see cref="PersonServiceImplementation.GetPersonById"/> method when the person exists.
        /// </summary>
        [TestMethod]
        public void TestGetPersonByIdPersonExists()
        {
            using (this.mocks.Record())
            {
                Expect.Call(() => this.personDataService.GetPersonById(Arg<int>.Is.Anything)).WhenCalled(call =>
                {
                    int idParameter = (int)call.Arguments[0];
                    call.ReturnValue = this.persons.FirstOrDefault(person => person.Id == idParameter);
                });
            }

            using (this.mocks.Playback())
            {
                // Arrange
                PersonServiceImplementation servicesImplementation = new PersonServiceImplementation(this.personDataService);
                int existingPersonId = 1;

                // Act
                Person result = servicesImplementation.GetPersonById(existingPersonId);

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(result.Id, existingPersonId);
                Assert.AreEqual(result.CNP, "1461591791659");
            }
        }

        /// <summary>
        /// Tests the <see cref="PersonServiceImplementation.GetPersonById"/> method when the person does not exist anymore.
        /// </summary>
        [TestMethod]
        public void TestGetPersonByIdPersonDoesNotExistAnymore()
        {
            using (this.mocks.Record())
            {
                Expect.Call(() => this.personDataService.GetPersonById(Arg<int>.Is.Anything)).WhenCalled(call =>
                {
                    int idParameter = (int)call.Arguments[0];
                    call.ReturnValue = this.persons.FirstOrDefault(person => person.Id == idParameter);
                });
            }

            using (this.mocks.Playback())
            {
                // Arrange
                PersonServiceImplementation servicesImplementation = new PersonServiceImplementation(this.personDataService);
                int existingPersonId = 1;
                this.persons.Clear();

                // Act
                Person result = servicesImplementation.GetPersonById(existingPersonId);

                // Assert
                Assert.IsNull(result);
            }
        }

        /// <summary>
        /// Tests the <see cref="PersonServiceImplementation.GetPersonById"/> method when the person does not exist.
        /// </summary>
        [TestMethod]
        public void TestGetPersonByIdPersonDoesNotExist()
        {
            using (this.mocks.Record())
            {
                Expect.Call(() => this.personDataService.GetPersonById(Arg<int>.Is.Anything)).WhenCalled(call =>
                {
                    int idParameter = (int)call.Arguments[0];
                    call.ReturnValue = this.persons.FirstOrDefault(person => person.Id == idParameter);
                });
            }

            using (this.mocks.Playback())
            {
                // Arrange
                PersonServiceImplementation servicesImplementation = new PersonServiceImplementation(this.personDataService);
                int nonExistingPersonId = 99;

                // Act
                Person result = servicesImplementation.GetPersonById(nonExistingPersonId);

                // Assert
                Assert.IsNull(result);
            }
        }

        /// <summary>
        /// Tests the <see cref="PersonServiceImplementation.AddPerson"/> method when there are items.
        /// </summary>
        [TestMethod]
        public void TestAddPersonsHasItems()
        {
            using (this.mocks.Record())
            {
                Expect.Call(() => this.personDataService.AddPerson(Arg<Person>.Is.Anything)).WhenCalled(call =>
                {
                    Person personParameter = (Person)call.Arguments[0];
                    this.persons.Add(personParameter);
                });
            }

            using (this.mocks.Playback())
            {
                // Arrange
                PersonServiceImplementation servicesImplementation = new PersonServiceImplementation(this.personDataService);
                Person person = new Person { Id = 10, FirstName = "FirstName", LastName = "LastName", CNP = "1587624953825", Type = PersonType.Reader, Address = "asdad saasdas  sdasdasd", EmailAddress = "as1@mail.com" };

                // Act
                servicesImplementation.AddPerson(person);

                // Assert
                Assert.IsNotNull(this.persons.First(a => a.Id == person.Id));
                Assert.AreEqual(this.persons.Count, 4);
            }
        }

        /// <summary>
        /// Tests the <see cref="PersonServiceImplementation.AddPerson"/> method when there are 0 items initially.
        /// </summary>
        [TestMethod]
        public void TestAddPersonsHasNoItems()
        {
            using (this.mocks.Record())
            {
                Expect.Call(() => this.personDataService.AddPerson(Arg<Person>.Is.Anything)).WhenCalled(call =>
                {
                    Person personParameter = (Person)call.Arguments[0];
                    this.persons.Add(personParameter);
                });
            }

            using (this.mocks.Playback())
            {
                // Arrange
                PersonServiceImplementation servicesImplementation = new PersonServiceImplementation(this.personDataService);
                Person person = new Person { Id = 10, FirstName = "FirstName", LastName = "LastName", CNP = "1587624953825", Type = PersonType.Reader, Address = "asdad saasdas  sdasdasd", EmailAddress = "as1@mail.com" };
                this.persons.Clear();

                // Act
                servicesImplementation.AddPerson(person);

                // Assert
                Assert.IsNotNull(this.persons.First(a => a.Id == person.Id));
                Assert.AreEqual(this.persons.Count, 1);
            }
        }

        /// <summary>
        /// Tests the <see cref="PersonServiceImplementation.DeletePerson"/> method when the person exists.
        /// </summary>
        [TestMethod]
        public void TestDeletePersonPersonExists()
        {
            using (this.mocks.Record())
            {
                Expect.Call(() => this.personDataService.DeletePerson(Arg<Person>.Is.Anything)).WhenCalled(call =>
                {
                    Person personParameter = (Person)call.Arguments[0];
                    int index = this.persons.FindIndex(a => a.Id == personParameter.Id);
                    if (index == -1)
                    {
                        throw new Exception();
                    }

                    this.persons.RemoveAt(index);
                });
            }

            using (this.mocks.Playback())
            {
                // Arrange
                PersonServiceImplementation servicesImplementation = new PersonServiceImplementation(this.personDataService);
                Person personToDelete = this.persons.First(); // Select the first person for deletion

                // Act
                servicesImplementation.DeletePerson(personToDelete);

                // Assert
                Assert.AreEqual(this.persons.Count, 2); // Person should be removed
                Assert.IsFalse(this.persons.Contains(personToDelete));
            }
        }

        /// <summary>
        /// Tests the <see cref="PersonServiceImplementation.DeletePerson"/> method when the person does not exist.
        /// </summary>
        [TestMethod]
        public void TestDeletePersonPersonDoesNotExist()
        {
            using (this.mocks.Record())
            {
                Expect.Call(() => this.personDataService.DeletePerson(Arg<Person>.Is.Anything)).WhenCalled(call =>
                {
                    Person personParameter = (Person)call.Arguments[0];
                    int index = this.persons.FindIndex(a => a.Id == personParameter.Id);
                    if (index == -1)
                    {
                        throw new Exception();
                    }

                    this.persons.RemoveAt(index);
                });
            }

            using (this.mocks.Playback())
            {
                // Arrange
                PersonServiceImplementation servicesImplementation = new PersonServiceImplementation(this.personDataService);
                Person nonExistingPerson = new Person { Id = 99, FirstName = "FirstName", LastName = "LastName", CNP = "1587444953825", Type = PersonType.Reader, Address = "asdad saasdas  sdasdasd", EmailAddress = "as1@mail.com" };

                // Assert and Act
                Assert.ThrowsException<Exception>(() => servicesImplementation.DeletePerson(nonExistingPerson));
            }
        }

        /// <summary>
        /// Tests the <see cref="PersonServiceImplementation.UpdatePerson"/> method when the person exists.
        /// </summary>
        [TestMethod]
        public void TestUpdatePersonPersonExists()
        {
            using (this.mocks.Record())
            {
                Expect.Call(() => this.personDataService.UpdatePerson(Arg<Person>.Is.Anything)).WhenCalled(call =>
                {
                    Person personParameter = (Person)call.Arguments[0];
                    int index = this.persons.FindIndex(a => a.Id == personParameter.Id);
                    if (index == -1)
                    {
                        throw new Exception("Person not found");
                    }

                    this.persons[index] = personParameter;
                });
            }

            using (this.mocks.Playback())
            {
                // Arrange
                PersonServiceImplementation servicesImplementation = new PersonServiceImplementation(this.personDataService);
                Person personToUpdate = this.persons.First(); // Select the first person for updating

                // Act
                servicesImplementation.UpdatePerson(personToUpdate);

                // Assert
                Assert.IsTrue(this.persons.Contains(personToUpdate));
            }
        }

        /// <summary>
        /// Tests the <see cref="PersonServiceImplementation.UpdatePerson"/> method when the person does not exist.
        /// </summary>
        [TestMethod]
        public void TestUpdatePersonPersonDoesNotExist()
        {
            using (this.mocks.Record())
            {
                Expect.Call(() => this.personDataService.UpdatePerson(Arg<Person>.Is.Anything)).WhenCalled(call =>
                {
                    Person personParameter = (Person)call.Arguments[0];
                    int index = this.persons.FindIndex(a => a.Id == personParameter.Id);
                    if (index == -1)
                    {
                        throw new Exception("Person not found");
                    }

                    this.persons[index] = personParameter;
                });
            }

            using (this.mocks.Playback())
            {
                // Arrange
                PersonServiceImplementation servicesImplementation = new PersonServiceImplementation(this.personDataService);
                Person nonExistingPerson = new Person { Id = 99, FirstName = "FirstName", LastName = "LastName", CNP = "1587444953825", Type = PersonType.Reader, Address = "asdad saasdas  sdasdasd", EmailAddress = "as1@mail.com" };

                // Assert and Act
                Assert.ThrowsException<Exception>(() => servicesImplementation.UpdatePerson(nonExistingPerson));
            }
        }
    }
}
