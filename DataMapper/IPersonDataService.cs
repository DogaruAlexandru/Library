// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPersonDataService.cs" company="Transilvania University of Brasov">
//   Copyright (c) Dogaru Alexandru.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace DataMapper
{
    using System;
    using System.Collections.Generic;
    using DomainModel;

    /// <summary>
    /// Interface for accessing and manipulating data related to persons.
    /// </summary>
    public interface IPersonDataService
    {
        /// <summary>
        /// Gets a list of all persons.
        /// </summary>
        /// <returns>The list of all persons.</returns>
        IList<Person> GetAllPersons();

        /// <summary>
        /// Gets a person by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the person.</param>
        /// <returns>The person with the specified identifier, or null if not found.</returns>
        Person GetPersonById(int id);

        /// <summary>
        /// Adds a new person to the data store.
        /// </summary>
        /// <param name="person">The person to be added.</param>
        void AddPerson(Person person);

        /// <summary>
        /// Deletes a person from the data store.
        /// </summary>
        /// <param name="person">The person to be deleted.</param>
        void DeletePerson(Person person);

        /// <summary>
        /// Updates the information of an existing person in the data store.
        /// </summary>
        /// <param name="person">The person with updated information.</param>
        void UpdatePerson(Person person);
    }
}
