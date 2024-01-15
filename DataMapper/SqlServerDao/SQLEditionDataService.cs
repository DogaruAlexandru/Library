// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SQLEditionDataService.cs" company="Transilvania University of Brasov">
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
    /// Implementation of <see cref="IEditionDataService"/> for SQL Server data storage.
    /// </summary>
    public class SQLEditionDataService : IEditionDataService
    {
        /// <summary>
        /// Adds a new edition to the data store.
        /// </summary>
        /// <param name="edition">The edition to be added.</param>
        public void AddEdition(Edition edition)
        {
            using (var context = new MyApplicationContext())
            {
                context.Editions.Add(edition);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes an edition from the data store.
        /// </summary>
        /// <param name="edition">The edition to be deleted.</param>
        public void DeleteEdition(Edition edition)
        {
            using (var context = new MyApplicationContext())
            {
                context.Entry(edition).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Retrieves all editions from the data store.
        /// </summary>
        /// <returns>A list of all editions.</returns>
        public IList<Edition> GetAllEditions()
        {
            using (var context = new MyApplicationContext())
            {
                return context.Editions.ToList();
            }
        }

        /// <summary>
        /// Retrieves an edition by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the edition.</param>
        /// <returns>The edition with the specified identifier.</returns>
        public Edition GetEditionById(int id)
        {
            using (var context = new MyApplicationContext())
            {
                return context.Editions.Find(id);
            }
        }

        /// <summary>
        /// Updates an existing edition in the data store.
        /// </summary>
        /// <param name="edition">The edition to be updated.</param>
        public void UpdateEdition(Edition edition)
        {
            using (var context = new MyApplicationContext())
            {
                context.Entry(edition).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
