// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPersonService.cs" company="Your Company">
//   Copyright (c) Your Company. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ServiceLayer
{
    using System;
    using System.Collections.Generic;
    using DomainModel;

    /// <summary>
    /// Represents the service interface for managing Person entities.
    /// </summary>
    /// <remarks>
    /// This interface provides methods for performing CRUD (Create, Read, Update, Delete) operations on Person entities.
    /// </remarks>
    public interface IPersonService : IValidationService
    {
        /// <summary>
        /// Gets a list of all persons.
        /// </summary>
        /// <returns>List of Person entities.</returns>
        IList<Person> GetAllPersons();

        /// <summary>
        /// Gets a person by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the person.</param>
        /// <returns>The Person entity.</returns>
        Person GetPersonById(int id);

        /// <summary>
        /// Adds a new person.
        /// </summary>
        /// <param name="person">The Person entity to be added.</param>
        void AddPerson(Person person);

        /// <summary>
        /// Deletes an existing person.
        /// </summary>
        /// <param name="person">The Person entity to be deleted.</param>
        void DeletePerson(Person person);

        /// <summary>
        /// Updates an existing person.
        /// </summary>
        /// <param name="person">The Person entity to be updated.</param>
        void UpdatePerson(Person person);
    }
}
