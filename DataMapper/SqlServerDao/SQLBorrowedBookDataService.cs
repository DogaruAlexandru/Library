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

        public int CountBorrowedBooksByEditionWithNullReturnedDate(Edition edition)
        {
            using (var context = new MyApplicationContext())
            {
                return context.BorrowedBooks
                    .Count(b => b.Edition.Id == edition.Id && b.ReturnedDate == null);
            }
        }

        public int CountBorrowedBooksByPersonAndDate(Person person, DateTime date)
        {
            using (var context = new MyApplicationContext())
            {
                return context.BorrowedBooks
                    .Count(b => b.Reader.Id == person.Id && b.BorrowDate > date);
            }
        }

        public int CountBorrowedBooksForPersonAndDomainAfterDate(Person person, BookDomain bookDomain, DateTime date)
        {
            using (var context = new MyApplicationContext())
            {
                return context.BorrowedBooks
                    .Count(b => b.Edition.Book.BookDomains.Any(bd => bd.Id == bookDomain.Id)
                        && b.BorrowDate > date
                        && b.Reader.Id == person.Id);
            }
        }

        public List<int> GetDueDateDifferencesForPersonAfterDate(Person person, DateTime date)
        {
            using (var context = new MyApplicationContext())
            {
                return context.BorrowedBooks
                    .Where(b => b.Reader.Id == person.Id && b.BorrowDate > date)
                    .Select(b => (b.DueDate - b.BorrowDate).Days)
                    .ToList();
            }
        }

        public int CountBorrowedBooksByEditionForPersonAfterDate(Person person, Edition edition, DateTime date)
        {
            using (var context = new MyApplicationContext())
            {
                return context.BorrowedBooks
                    .Count(b => b.Reader.Id == person.Id && b.Edition.Id == edition.Id && b.BorrowDate > date);
            }
        }

        public int CountBooksBorrowedByPersonOnDate(Person person, DateTime date)
        {
            using (var context = new MyApplicationContext())
            {
                return context.BorrowedBooks
                    .Count(b => b.Reader.Id == person.Id && b.BorrowDate.Date == date.Date);
            }
        }

        public int CountBooksBorrowedBySuffOnDate(Person person, DateTime date)
        {
            using (var context = new MyApplicationContext())
            {
                return context.BorrowedBooks
                    .Count(b => b.Staff.Id == person.Id && b.BorrowDate.Date == date.Date);
            }
        }
    }
}
