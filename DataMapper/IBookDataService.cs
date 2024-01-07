// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IBookDataService.cs" company="Transilvania University of Brasov">
//   Copyright (c) Dogaru Alexandru.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace DataMapper
{
    using System;
    using System.Collections.Generic;
    using DomainModel;

    /// <summary>
    /// Interface for accessing and manipulating data related to books.
    /// </summary>
    public interface IBookDataService
    {
        /// <summary>
        /// Gets a list of all books.
        /// </summary>
        /// <returns>The list of all books.</returns>
        IList<Book> GetAllBooks();

        /// <summary>
        /// Gets a book by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the book.</param>
        /// <returns>The book with the specified identifier, or null if not found.</returns>
        Book GetBookById(int id);

        /// <summary>
        /// Adds a new book to the data store.
        /// </summary>
        /// <param name="book">The book to be added.</param>
        void AddBook(Book book);

        /// <summary>
        /// Deletes a book from the data store.
        /// </summary>
        /// <param name="book">The book to be deleted.</param>
        void DeleteBook(Book book);

        /// <summary>
        /// Updates the information of an existing book in the data store.
        /// </summary>
        /// <param name="book">The book with updated information.</param>
        void UpdateBook(Book book);
    }
}
