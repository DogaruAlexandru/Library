// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SQLBorrowedBookDataService.cs" company="Transilvania University of Brasov">
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
    /// Represents the SQL implementation of the data service for the BorrowedBook entity.
    /// </summary>
    public class SQLBorrowedBookDataService : IBorrowedBookDataService
    {
        /// <summary>
        /// Adds a new borrowed book to the database.
        /// </summary>
        /// <param name="borrowedBook">The borrowed book to be added.</param>
        public void AddBorrowedBook(BorrowedBook borrowedBook)
        {
            using (var context = new MyApplicationContext())
            {
                context.BorrowedBooks.Add(borrowedBook);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes a borrowed book from the database.
        /// </summary>
        /// <param name="borrowedBook">The borrowed book to be deleted.</param>
        public void DeleteBorrowedBook(BorrowedBook borrowedBook)
        {
            using (var context = new MyApplicationContext())
            {
                context.Entry(borrowedBook).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Retrieves all borrowed books from the database.
        /// </summary>
        /// <returns>A list of all borrowed books.</returns>
        public IList<BorrowedBook> GetAllBorrowedBooks()
        {
            using (var context = new MyApplicationContext())
            {
                return context.BorrowedBooks.ToList();
            }
        }

        /// <summary>
        /// Retrieves a borrowed book by its ID.
        /// </summary>
        /// <param name="id">The ID of the borrowed book to retrieve.</param>
        /// <returns>The borrowed book with the specified ID.</returns>
        public BorrowedBook GetBorrowedBookById(int id)
        {
            using (var context = new MyApplicationContext())
            {
                return context.BorrowedBooks.Find(id);
            }
        }

        /// <summary>
        /// Updates an existing borrowed book in the database.
        /// </summary>
        /// <param name="borrowedBook">The borrowed book to be updated.</param>
        public void UpdateBorrowedBook(BorrowedBook borrowedBook)
        {
            using (var context = new MyApplicationContext())
            {
                context.Entry(borrowedBook).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Counts the number of borrowed books for a specific edition with a null returned date.
        /// </summary>
        /// <param name="edition">The edition for which to count borrowed books.</param>
        /// <returns>The count of borrowed books for the specified edition with a null returned date.</returns>
        public int CountBorrowedBooksByEditionWithNullReturnedDate(Edition edition)
        {
            using (var context = new MyApplicationContext())
            {
                return context.BorrowedBooks.Count(b => b.Edition.Id == edition.Id && b.ReturnedDate == null);
            }
        }

        /// <summary>
        /// Counts the number of borrowed books by a person after a specific date.
        /// </summary>
        /// <param name="person">The person for whom to count borrowed books.</param>
        /// <param name="date">The date after which to count borrowed books.</param>
        /// <returns>The count of borrowed books by the specified person after the given date.</returns>
        public int CountBorrowedBooksByPersonAndDate(Person person, DateTime date)
        {
            using (var context = new MyApplicationContext())
            {
                return context.BorrowedBooks.Count(b => b.Reader.Id == person.Id && b.BorrowDate > date);
            }
        }

        /// <summary>
        /// Counts the number of borrowed books by a person, for a specific book domain, after a specific date.
        /// </summary>
        /// <param name="person">The person for whom to count borrowed books.</param>
        /// <param name="bookDomain">The book domain for which to count borrowed books.</param>
        /// <param name="date">The date after which to count borrowed books.</param>
        /// <returns>
        /// The count of borrowed books by the specified person, for the specified book domain, after the given date.
        /// </returns>
        public int CountBorrowedBooksForPersonAndDomainAfterDate(Person person, BookDomain bookDomain, DateTime date)
        {
            using (var context = new MyApplicationContext())
            {
                return context.BorrowedBooks.Count(b =>
                    b.Edition.Book.BookDomains.Any(bd => bd.Id == bookDomain.Id)
                    && b.BorrowDate > date
                    && b.Reader.Id == person.Id);
            }
        }

        /// <summary>
        /// Gets the differences in days between due date and borrow date for a person after a specific date.
        /// </summary>
        /// <param name="person">The person for whom to get due date differences.</param>
        /// <param name="date">The date after which to get due date differences.</param>
        /// <returns>A list of differences in days between due date and borrow date for the specified person.</returns>
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

        /// <summary>
        /// Counts the number of borrowed books for a specific edition by a person after a specific date.
        /// </summary>
        /// <param name="person">The person for whom to count borrowed books.</param>
        /// <param name="edition">The edition for which to count borrowed books.</param>
        /// <param name="date">The date after which to count borrowed books.</param>
        /// <returns>
        /// The count of borrowed books by the specified person for the specified edition after the given date.
        /// </returns>
        public int CountBorrowedBooksByEditionForPersonAfterDate(Person person, Edition edition, DateTime date)
        {
            using (var context = new MyApplicationContext())
            {
                return context.BorrowedBooks
                    .Count(b => b.Reader.Id == person.Id && b.Edition.Id == edition.Id && b.BorrowDate > date);
            }
        }

        /// <summary>
        /// Counts the number of books borrowed by a person on a specific date.
        /// </summary>
        /// <param name="person">The person for whom to count borrowed books.</param>
        /// <param name="date">The date on which to count borrowed books.</param>
        /// <returns>The count of books borrowed by the specified person on the given date.</returns>
        public int CountBooksBorrowedByPersonOnDate(Person person, DateTime date)
        {
            using (var context = new MyApplicationContext())
            {
                return context.BorrowedBooks
                    .Count(b => b.Reader.Id == person.Id && b.BorrowDate.Date == date.Date);
            }
        }

        /// <summary>
        /// Counts the number of books borrowed by a staff member on a specific date.
        /// </summary>
        /// <param name="person">The staff member for whom to count borrowed books.</param>
        /// <param name="date">The date on which to count borrowed books.</param>
        /// <returns>The count of books borrowed by the specified staff member on the given date.</returns>
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
