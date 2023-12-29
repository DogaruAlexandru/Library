using DataMapper.SqlServerDao;
using DomainModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataMapper
{
    internal class SQLBorrowedBookDataService : IBorrowedBookDataService
    {
        public void AddBorrowedBook(BorrowedBook borrowedBook)
        {
            using (var context = new MyApplicationContext())
            {
                context.BorrowedBooks.Add(borrowedBook);
                context.SaveChanges();
            }
        }

        public void DeleteBorrowedBook(BorrowedBook borrowedBook)
        {
            using (var context = new MyApplicationContext())
            {
                context.Entry(borrowedBook).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public IList<BorrowedBook> GetAllBorrowedBooks()
        {
            using (var context = new MyApplicationContext())
            {
                return context.BorrowedBooks.ToList();
            }
        }

        public BorrowedBook GetBorrowedBookById(int id)
        {
            using (var context = new MyApplicationContext())
            {
                return context.BorrowedBooks.Find(id);
            }
        }

        public void UpdateBorrowedBook(BorrowedBook borrowedBook)
        {
            using (var context = new MyApplicationContext())
            {
                context.Entry(borrowedBook).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
