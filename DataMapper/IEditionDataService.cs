// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEditionDataService.cs" company="Transilvania University of Brasov">
//   Copyright (c) Dogaru Alexandru.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace DataMapper
{
    using System;
    using System.Collections.Generic;
    using DomainModel;

    /// <summary>
    /// Interface for accessing and manipulating data related to editions.
    /// </summary>
    public interface IEditionDataService
    {
        /// <summary>
        /// Gets a list of all editions.
        /// </summary>
        /// <returns>The list of all editions.</returns>
        IList<Edition> GetAllEditions();

        /// <summary>
        /// Gets an edition by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the edition.</param>
        /// <returns>The edition with the specified identifier, or null if not found.</returns>
        Edition GetEditionById(int id);

        /// <summary>
        /// Adds a new edition to the data store.
        /// </summary>
        /// <param name="edition">The edition to be added.</param>
        void AddEdition(Edition edition);

        /// <summary>
        /// Deletes an edition from the data store.
        /// </summary>
        /// <param name="edition">The edition to be deleted.</param>
        void DeleteEdition(Edition edition);

        /// <summary>
        /// Updates the information of an existing edition in the data store.
        /// </summary>
        /// <param name="edition">The edition with updated information.</param>
        void UpdateEdition(Edition edition);
    }
}
