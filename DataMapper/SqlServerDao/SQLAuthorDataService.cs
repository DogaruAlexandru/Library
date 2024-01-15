// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SQLAuthorDataService.cs" company="Transilvania University of Brasov">
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
    /// Represents the SQL implementation of the data service for the Author entity.
    /// </summary>
    public class SQLAuthorDataService : IAuthorDataService
    {
        /// <summary>
        /// Adds a new author to the database.
        /// </summary>
        /// <param name="author">The author to be added.</param>
        public void AddAuthor(Author author)
        {
            using (var context = new MyApplicationContext())
            {
                context.Authors.Add(author);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes an author from the database.
        /// </summary>
        /// <param name="author">The author to be deleted.</param>
        public void DeleteAuthor(Author author)
        {
            using (var context = new MyApplicationContext())
            {
                context.Entry(author).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Retrieves all authors from the database.
        /// </summary>
        /// <returns>A list of all authors.</returns>
        public IList<Author> GetAllAuthors()
        {
            using (var context = new MyApplicationContext())
            {
                return context.Authors.ToList();
            }
        }

        /// <summary>
        /// Retrieves an author by their ID.
        /// </summary>
        /// <param name="id">The ID of the author to retrieve.</param>
        /// <returns>The author with the specified ID.</returns>
        public Author GetAuthorById(int id)
        {
            using (var context = new MyApplicationContext())
            {
                return context.Authors.Find(id);
            }
        }

        /// <summary>
        /// Updates an existing author in the database.
        /// </summary>
        /// <param name="author">The author to be updated.</param>
        public void UpdateAuthor(Author author)
        {
            using (var context = new MyApplicationContext())
            {
                context.Entry(author).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
