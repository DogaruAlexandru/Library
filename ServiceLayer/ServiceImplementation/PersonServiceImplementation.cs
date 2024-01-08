// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PersonServiceImplementation.cs" company="Transilvania University of Brasov">
//   Copyright (c) Dogaru Alexandru.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ServiceLayer.ServiceImplementation
{
    using System;
    using System.Collections.Generic;
    using DataMapper;
    using DomainModel;
    using log4net;

    /// <summary>
    /// Provides implementation for the IPersonService interface.
    /// </summary>
    public class PersonServiceImplementation : BaseService, IPersonService
    {
        /// <summary>
        /// Represents the logger instance for logging PersonServiceImplementation class.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(PersonServiceImplementation));

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonServiceImplementation"/> class.
        /// </summary>
        /// <param name="authorDataService">The data service for persons.</param>
        public PersonServiceImplementation(IPersonDataService personDataService)
        {
            this.PersonDataService = personDataService;
        }

        /// <summary>
        /// Gets or sets the data service for persons.
        /// </summary>
        private IPersonDataService PersonDataService { get; set; }

        /// <summary>
        /// Adds a new person to the data source.
        /// </summary>
        /// <param name="person">The person object to be added.</param>
        public void AddPerson(Person person)
        {
            this.ValidateEntity(person);

            Log.Info($"Adding Person with ID: {person.Id}");

            this.PersonDataService.AddPerson(person);
        }

        /// <summary>
        /// Deletes a person from the data source.
        /// </summary>
        /// <param name="person">The person object to be deleted.</param>
        public void DeletePerson(Person person)
        {
            Log.Debug($"Deleting Person with ID: {person.Id}");

            this.PersonDataService.DeletePerson(person);
        }

        /// <summary>
        /// Gets a list of all persons from the data source.
        /// </summary>
        /// <returns>A list of Person objects.</returns>
        public IList<Person> GetAllPersons()
        {
            Log.Debug("Getting all Persons.");

            return this.PersonDataService.GetAllPersons();
        }

        /// <summary>
        /// Gets a person by their ID from the data source.
        /// </summary>
        /// <param name="id">The ID of the person to retrieve.</param>
        /// <returns>The Person object with the specified ID.</returns>
        public Person GetPersonById(int id)
        {
            Log.Debug($"Getting Person with ID: {id}");

            return this.PersonDataService.GetPersonById(id);
        }

        /// <summary>
        /// Updates an existing person in the data source.
        /// </summary>
        /// <param name="person">The person object with updated information.</param>
        public void UpdatePerson(Person person)
        {
            this.ValidateEntity(person);

            Log.Info($"Updating Person with ID: {person.Id}");

            this.PersonDataService.UpdatePerson(person);
        }
    }
}
