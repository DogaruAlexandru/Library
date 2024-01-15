// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SQLBookDataService.cs" company="Transilvania University of Brasov">
//   Copyright (c) Dogaru Alexandru.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace DataMapper
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using DataMapper.SqlServerDao;
    using DomainModel;

    /// <summary>
    /// Represents the SQL implementation of the data service for the Book entity.
    /// </summary>
    public class SQLBookDataService : IBookDataService
    {
        /// <summary>
        /// Adds a new book to the database.
        /// </summary>
        /// <param name="book">The book to be added.</param>
        public void AddBook(Book book)
        {
            using (var context = new MyApplicationContext())
            {
                context.Books.Add(book);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes a book from the database.
        /// </summary>
        /// <param name="book">The book to be deleted.</param>
        public void DeleteBook(Book book)
        {
            using (var context = new MyApplicationContext())
            {
                context.Entry(book).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Retrieves all books from the database.
        /// </summary>
        /// <returns>A list of all books.</returns>
        public IList<Book> GetAllBooks()
        {
            using (var context = new MyApplicationContext())
            {
                return context.Books.ToList();
            }
        }

        /// <summary>
        /// Retrieves a book by its ID.
        /// </summary>
        /// <param name="id">The ID of the book to retrieve.</param>
        /// <returns>The book with the specified ID.</returns>
        public Book GetBookById(int id)
        {
            using (var context = new MyApplicationContext())
            {
                return context.Books.Find(id);
            }
        }

        /// <summary>
        /// Updates an existing book in the database.
        /// </summary>
        /// <param name="book">The book to be updated.</param>
        public void UpdateBook(Book book)
        {
            using (var context = new MyApplicationContext())
            {
                context.Entry(book).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
