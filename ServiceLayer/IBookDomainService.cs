// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IBookDomainService.cs" company="Transilvania University of Brasov">
//   Copyright (c) Dogaru Alexandru.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ServiceLayer
{
    using System.Collections.Generic;
    using DomainModel;

    /// <summary>
    /// Represents the service interface for managing BookDomain entities.
    /// </summary>
    /// <remarks>
    /// This interface provides methods for performing CRUD (Create, Read, Update, Delete) operations on BookDomain entities.
    /// </remarks>
    public interface IBookDomainService : IValidationService
    {
        /// <summary>
        /// Gets a list of all book domains.
        /// </summary>
        /// <returns>List of BookDomain entities.</returns>
        IList<BookDomain> GetAllBookDomains();

        /// <summary>
        /// Gets a book domain by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the book domain.</param>
        /// <returns>The BookDomain entity.</returns>
        BookDomain GetBookDomainById(int id);

        /// <summary>
        /// Adds a new book domain.
        /// </summary>
        /// <param name="bookDomain">The BookDomain entity to be added.</param>
        void AddBookDomain(BookDomain bookDomain);

        /// <summary>
        /// Deletes an existing book domain.
        /// </summary>
        /// <param name="bookDomain">The BookDomain entity to be deleted.</param>
        void DeleteBookDomain(BookDomain bookDomain);

        /// <summary>
        /// Updates an existing book domain.
        /// </summary>
        /// <param name="bookDomain">The BookDomain entity to be updated.</param>
        void UpdateBookDomain(BookDomain bookDomain);
    }
}
