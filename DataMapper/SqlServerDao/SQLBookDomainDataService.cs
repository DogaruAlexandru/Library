// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SQLBookDomainDataService.cs" company="Transilvania University of Brasov">
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
    /// Represents the SQL implementation of the data service for the BookDomain entity.
    /// </summary>
    internal class SQLBookDomainDataService : IBookDomainDataService
    {
        /// <summary>
        /// Adds a new book domain to the database.
        /// </summary>
        /// <param name="bookDomain">The book domain to be added.</param>
        public void AddBookDomain(BookDomain bookDomain)
        {
            using (var context = new MyApplicationContext())
            {
                context.BookDomains.Add(bookDomain);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes a book domain from the database.
        /// </summary>
        /// <param name="bookDomain">The book domain to be deleted.</param>
        public void DeleteBookDomain(BookDomain bookDomain)
        {
            using (var context = new MyApplicationContext())
            {
                context.Entry(bookDomain).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Retrieves all book domains from the database.
        /// </summary>
        /// <returns>A list of all book domains.</returns>
        public IList<BookDomain> GetAllBookDomains()
        {
            using (var context = new MyApplicationContext())
            {
                return context.BookDomains.ToList();
            }
        }

        /// <summary>
        /// Retrieves a book domain by its ID.
        /// </summary>
        /// <param name="id">The ID of the book domain to retrieve.</param>
        /// <returns>The book domain with the specified ID.</returns>
        public BookDomain GetBookDomainById(int id)
        {
            using (var context = new MyApplicationContext())
            {
                return context.BookDomains.Find(id);
            }
        }

        /// <summary>
        /// Updates an existing book domain in the database.
        /// </summary>
        /// <param name="bookDomain">The book domain to be updated.</param>
        public void UpdateBookDomain(BookDomain bookDomain)
        {
            using (var context = new MyApplicationContext())
            {
                context.Entry(bookDomain).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
