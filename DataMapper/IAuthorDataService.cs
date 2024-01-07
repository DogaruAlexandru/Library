// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAuthorDataService.cs" company="Transilvania University of Brasov">
//   Copyright (c) Dogaru Alexandru.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace DataMapper
{
    using System;
    using System.Collections.Generic;
    using DomainModel;

    /// <summary>
    /// Interface for accessing and manipulating data related to authors.
    /// </summary>
    public interface IAuthorDataService
    {
        /// <summary>
        /// Gets a list of all authors.
        /// </summary>
        /// <returns>The list of all authors.</returns>
        IList<Author> GetAllAuthors();

        /// <summary>
        /// Gets an author by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the author.</param>
        /// <returns>The author with the specified identifier, or null if not found.</returns>
        Author GetAuthorById(int id);

        /// <summary>
        /// Adds a new author to the data store.
        /// </summary>
        /// <param name="author">The author to be added.</param>
        void AddAuthor(Author author);

        /// <summary>
        /// Deletes an author from the data store.
        /// </summary>
        /// <param name="author">The author to be deleted.</param>
        void DeleteAuthor(Author author);

        /// <summary>
        /// Updates the information of an existing author in the data store.
        /// </summary>
        /// <param name="author">The author with updated information.</param>
        void UpdateAuthor(Author author);
    }
}
