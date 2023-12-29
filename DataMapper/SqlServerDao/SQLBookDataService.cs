using DataMapper.SqlServerDao;
using DomainModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataMapper
{
    internal class SQLBookDataService : IBookDataService
    {
        public void AddBook(Book book)
        {
            using (var context = new MyApplicationContext())
            {
                context.Books.Add(book);
                context.SaveChanges();
            }
        }

        public void DeleteBook(Book book)
        {
            using (var context = new MyApplicationContext())
            {
                context.Entry(book).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public IList<Book> GetAllBooks()
        {
            using (var context = new MyApplicationContext())
            {
                return context.Books.ToList();
            }
        }

        public Book GetBookById(int id)
        {
            using (var context = new MyApplicationContext())
            {
                return context.Books.Find(id);
            }
        }

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
