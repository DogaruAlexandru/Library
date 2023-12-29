using DataMapper.SqlServerDao;
using DomainModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataMapper
{
    internal class SQLBookDomainDataService : IBookDomainDataService
    {
        public void AddBookDomain(BookDomain bookDomain)
        {
            using (var context = new MyApplicationContext())
            {
                context.BookDomains.Add(bookDomain);
                context.SaveChanges();
            }
        }

        public void DeleteBookDomain(BookDomain bookDomain)
        {
            using (var context = new MyApplicationContext())
            {
                context.Entry(bookDomain).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public IList<BookDomain> GetAllBookDomains()
        {
            using (var context = new MyApplicationContext())
            {
                return context.BookDomains.ToList();
            }
        }

        public BookDomain GetBookDomainById(int id)
        {
            using (var context = new MyApplicationContext())
            {
                return context.BookDomains.Find(id);
            }
        }

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
