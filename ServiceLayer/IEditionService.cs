// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEditionService.cs" company="Your Company">
//   Copyright (c) Your Company. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ServiceLayer
{
    using System;
    using System.Collections.Generic;
    using DomainModel;

    /// <summary>
    /// Represents the service interface for managing Edition entities.
    /// </summary>
    /// <remarks>
    /// This interface provides methods for performing CRUD (Create, Read, Update, Delete) operations on Edition entities.
    /// </remarks>
    public interface IEditionService : IValidationService
    {
        /// <summary>
        /// Gets a list of all editions.
        /// </summary>
        /// <returns>List of Edition entities.</returns>
        IList<Edition> GetAllEditions();

        /// <summary>
        /// Gets an edition by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the edition.</param>
        /// <returns>The Edition entity.</returns>
        Edition GetEditionById(int id);

        /// <summary>
        /// Adds a new edition.
        /// </summary>
        /// <param name="edition">The Edition entity to be added.</param>
        void AddEdition(Edition edition);

        /// <summary>
        /// Deletes an existing edition.
        /// </summary>
        /// <param name="edition">The Edition entity to be deleted.</param>
        void DeleteEdition(Edition edition);

        /// <summary>
        /// Updates an existing edition.
        /// </summary>
        /// <param name="edition">The Edition entity to be updated.</param>
        void UpdateEdition(Edition edition);
    }
}
