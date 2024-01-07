// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IBookService.cs" company="Your Company">
//   Copyright (c) Your Company. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ServiceLayer
{
    using System.Collections.Generic;
    using DomainModel;

    /// <summary>
    /// Represents the service interface for managing Book entities.
    /// </summary>
    /// <remarks>
    /// This interface provides methods for performing CRUD (Create, Read, Update, Delete) operations on Book entities.
    /// </remarks>
    public interface IBookService : IValidationService
    {
        /// <summary>
        /// Gets a list of all books.
        /// </summary>
        /// <returns>List of Book entities.</returns>
        IList<Book> GetAllBooks();

        /// <summary>
        /// Gets a book by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the book.</param>
        /// <returns>The Book entity.</returns>
        Book GetBookById(int id);

        /// <summary>
        /// Adds a new book.
        /// </summary>
        /// <param name="book">The Book entity to be added.</param>
        void AddBook(Book book);

        /// <summary>
        /// Deletes an existing book.
        /// </summary>
        /// <param name="book">The Book entity to be deleted.</param>
        void DeleteBook(Book book);

        /// <summary>
        /// Updates an existing book.
        /// </summary>
        /// <param name="book">The Book entity to be updated.</param>
        void UpdateBook(Book book);
    }
}
