// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IBookDomainDataService.cs" company="Transilvania University of Brasov">
//   Copyright (c) Dogaru Alexandru.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace DataMapper
{
    using System;
    using System.Collections.Generic;
    using DomainModel;

    /// <summary>
    /// Interface for accessing and manipulating data related to book domains.
    /// </summary>
    public interface IBookDomainDataService
    {
        /// <summary>
        /// Gets a list of all book domains.
        /// </summary>
        /// <returns>The list of all book domains.</returns>
        IList<BookDomain> GetAllBookDomains();

        /// <summary>
        /// Gets a book domain by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the book domain.</param>
        /// <returns>The book domain with the specified identifier, or null if not found.</returns>
        BookDomain GetBookDomainById(int id);

        /// <summary>
        /// Adds a new book domain to the data store.
        /// </summary>
        /// <param name="bookDomain">The book domain to be added.</param>
        void AddBookDomain(BookDomain bookDomain);

        /// <summary>
        /// Deletes a book domain from the data store.
        /// </summary>
        /// <param name="bookDomain">The book domain to be deleted.</param>
        void DeleteBookDomain(BookDomain bookDomain);

        /// <summary>
        /// Updates the information of an existing book domain in the data store.
        /// </summary>
        /// <param name="bookDomain">The book domain with updated information.</param>
        void UpdateBookDomain(BookDomain bookDomain);
    }
}
