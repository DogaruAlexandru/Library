// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SQLPersonDataService.cs" company="Transilvania University of Brasov">
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
    /// Provides an implementation of the <see cref="IPersonDataService"/> for SQL Server data storage.
    /// </summary>
    public class SQLPersonDataService : IPersonDataService
    {
        /// <inheritdoc/>
        public void AddPerson(Person person)
        {
            using (var context = new MyApplicationContext())
            {
                context.Persons.Add(person);
                context.SaveChanges();
            }
        }

        /// <inheritdoc/>
        public void DeletePerson(Person person)
        {
            using (var context = new MyApplicationContext())
            {
                context.Entry(person).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        /// <inheritdoc/>
        public IList<Person> GetAllPersons()
        {
            using (var context = new MyApplicationContext())
            {
                return context.Persons.ToList();
            }
        }

        /// <inheritdoc/>
        public Person GetPersonById(int id)
        {
            using (var context = new MyApplicationContext())
            {
                return context.Persons.Find(id);
            }
        }

        /// <inheritdoc/>
        public void UpdatePerson(Person person)
        {
            using (var context = new MyApplicationContext())
            {
                context.Entry(person).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
