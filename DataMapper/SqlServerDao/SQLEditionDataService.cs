using DataMapper.SqlServerDao;
using DomainModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataMapper
{
    internal class SQLEditionDataService : IEditionDataService
    {
        public void AddEdition(Edition edition)
        {
            using (var context = new MyApplicationContext())
            {
                context.Editions.Add(edition);
                context.SaveChanges();
            }
        }

        public void DeleteEdition(Edition edition)
        {
            using (var context = new MyApplicationContext())
            {
                context.Entry(edition).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public IList<Edition> GetAllEditions()
        {
            using (var context = new MyApplicationContext())
            {
                return context.Editions.ToList();
            }
        }

        public Edition GetEditionById(int id)
        {
            using (var context = new MyApplicationContext())
            {
                return context.Editions.Find(id);
            }
        }

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
