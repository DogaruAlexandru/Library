// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAuthorService.cs" company="Transilvania University of Brasov">
//   Copyright (c) Dogaru Alexandru.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ServiceLayer
{
    using System.Collections.Generic;
    using DomainModel;

    /// <summary>
    /// Represents the service interface for managing Author entities.
    /// </summary>
    /// <remarks>
    /// This interface provides methods for performing CRUD (Create, Read, Update, Delete) operations on Author entities.
    /// </remarks>
    public interface IAuthorService : IValidationService
    {
        /// <summary>
        /// Gets a list of all authors.
        /// </summary>
        /// <returns>List of Author entities.</returns>
        IList<Author> GetAllAuthors();

        /// <summary>
        /// Gets an author by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the author.</param>
        /// <returns>The Author entity.</returns>
        Author GetAuthorById(int id);

        /// <summary>
        /// Adds a new author.
        /// </summary>
        /// <param name="author">The Author entity to be added.</param>
        void AddAuthor(Author author);

        /// <summary>
        /// Deletes an existing author.
        /// </summary>
        /// <param name="author">The Author entity to be deleted.</param>
        void DeleteAuthor(Author author);

        /// <summary>
        /// Updates an existing author.
        /// </summary>
        /// <param name="author">The Author entity to be updated.</param>
        void UpdateAuthor(Author author);
    }
}
