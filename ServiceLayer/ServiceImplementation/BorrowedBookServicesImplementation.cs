// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BorrowedBookServicesImplementation.cs" company="Transilvania University of Brasov">
//   Copyright (c) Dogaru Alexandru.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ServiceLayer.ServiceImplementation
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Configuration;
    using System.Linq;
    using DataMapper;
    using DomainModel;
    using log4net;

    /// <summary>
    /// Represents the implementation of the service for managing BorrowedBook entities.
    /// </summary>
    public class BorrowedBookServicesImplementation : BaseService, IBorrowedBookService
    {
        /// <summary>
        /// The default period (in days) for borrowing a book.
        /// </summary>
        public const int BorrowPeriodDays = 14;

        /// <summary>
        /// The logger instance for this class.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(BookDomainServicesImplementation));

        /// <summary>
        /// Initializes a new instance of the <see cref="BorrowedBookServicesImplementation"/> class.
        /// </summary>
        /// <param name="borrowedBookDataService">The data service for borrowedBooks.</param>
        public BorrowedBookServicesImplementation(IBorrowedBookDataService borrowedBookDataService)
        {
            this.BorrowedBookDataService = borrowedBookDataService;
        }

        /// <summary>
        /// Gets or sets the data service for borrowedBooks.
        /// </summary>
        private IBorrowedBookDataService BorrowedBookDataService { get; set; }

        /// <summary>
        /// Adds a new borrowed book to the system.
        /// </summary>
        /// <param name="borrowedBook">The borrowed book to be added.</param>
        public void AddBorrowedBook(BorrowedBook borrowedBook)
        {
            this.ValidateEntity(borrowedBook);

            Log.Info($"Adding BorrowedBook with ID: {borrowedBook.Id}");

            this.BorrowedBookDataService.AddBorrowedBook(borrowedBook);
        }

        /// <summary>
        /// Deletes a borrowed book from the system.
        /// </summary>
        /// <param name="borrowedBook">The borrowed book to be deleted.</param>
        public void DeleteBorrowedBook(BorrowedBook borrowedBook)
        {
            Log.Debug($"Deleting BorrowedBook with ID: {borrowedBook.Id}");

            this.BorrowedBookDataService.DeleteBorrowedBook(borrowedBook);
        }

        /// <summary>
        /// Gets all borrowed books in the system.
        /// </summary>
        /// <returns>A list of all borrowed books.</returns>
        public IList<BorrowedBook> GetAllBorrowedBooks()
        {
            Log.Debug("Getting all BorrowedBooks.");

            return this.BorrowedBookDataService.GetAllBorrowedBooks();
        }

        /// <summary>
        /// Gets a specific borrowed book by its ID.
        /// </summary>
        /// <param name="id">The ID of the borrowed book to retrieve.</param>
        /// <returns>The borrowed book with the specified ID.</returns>
        public BorrowedBook GetBorrowedBookById(int id)
        {
            Log.Debug($"Getting BorrowedBook with ID: {id}");

            return this.BorrowedBookDataService.GetBorrowedBookById(id);
        }

        /// <summary>
        /// Updates an existing borrowed book in the system.
        /// </summary>
        /// <param name="borrowedBook">The borrowed book to be updated.</param>
        public void UpdateBorrowedBook(BorrowedBook borrowedBook)
        {
            this.ValidateEntity(borrowedBook);

            Log.Info($"Updating BorrowedBook with ID: {borrowedBook.Id}");

            this.BorrowedBookDataService.UpdateBorrowedBook(borrowedBook);
        }

        /// <summary>
        /// Determines if more books can be borrowed based on the provided edition.
        /// </summary>
        /// <param name="edition">The edition for which to check the availability.</param>
        /// <returns>True if more books can be borrowed; otherwise, false.</returns>
        public bool CanBorrowMoreBooks(Edition edition)
        {
            Log.Debug($"Verifying Edition can be borrowed with ID: {edition.Id}");

            int borrowedBooksCount = this.BorrowedBookDataService.CountBorrowedBooksByEditionWithNullReturnedDate(edition);

            return (edition.CanBorrow - borrowedBooksCount) * 10 > edition.CanBorrow + edition.CanNotBorrow;
        }

        /// <summary>
        /// Counts the number of books borrowed by a person since a specified date.
        /// </summary>
        /// <param name="person">The person for whom to count the borrowed books.</param>
        /// <param name="date">The date since when to count the borrowed books.</param>
        /// <returns>The number of borrowed books by the person since the specified date.</returns>
        public int CountBorrowedSinceDateForPerson(Person person, DateTime date)
        {
            Log.Debug($"Counting borrowed books for Person with ID {person.Id} since {date.ToShortDateString()}");

            return this.BorrowedBookDataService.CountBorrowedBooksByPersonAndDate(person, date);
        }

        /// <summary>
        /// Counts the number of books borrowed by a person for a specific book domain after a specified date.
        /// </summary>
        /// <param name="person">The person for whom to count the borrowed books.</param>
        /// <param name="bookDomain">The book domain for which to count the borrowed books.</param>
        /// <param name="date">The date after which to count the borrowed books.</param>
        /// <returns>The number of borrowed books by the person for the specified book domain after the specified date.</returns>
        public int CountBorrowedBooksForPersonAndDomainAfterDate(Person person, BookDomain bookDomain, DateTime date)
        {
            Log.Debug($"Counting borrowed books for Person with ID {person.Id}, BookDomain with ID {bookDomain.Id} after {date.ToShortDateString()}");

            return this.BorrowedBookDataService.CountBorrowedBooksForPersonAndDomainAfterDate(person, bookDomain, date);
        }

        /// <summary>
        /// Gets the due date differences for a person after a specified date.
        /// </summary>
        /// <param name="person">The person for whom to get the due date differences.</param>
        /// <param name="date">The date after which to get the due date differences.</param>
        /// <returns>The list of due date differences for the person after the specified date.</returns>
        public List<int> GetDueDateDifferencesForPersonAfterDate(Person person, DateTime date)
        {
            Log.Debug($"Getting due date differences for Person with ID {person.Id} after {date.ToShortDateString()}");

            return this.BorrowedBookDataService.GetDueDateDifferencesForPersonAfterDate(person, date);
        }

        /// <summary>
        /// Counts the number of borrowed books by edition for a person after a specified date.
        /// </summary>
        /// <param name="person">The person for whom to count the borrowed books.</param>
        /// <param name="edition">The edition for which to count the borrowed books.</param>
        /// <param name="date">The date after which to count the borrowed books.</param>
        /// <returns>The number of borrowed books by the person for the specified edition after the specified date.</returns>
        public int CountBorrowedBooksByEditionForPersonAfterDate(Person person, Edition edition, DateTime date)
        {
            Log.Debug($"Counting borrowed books for Person with ID {person.Id}, Edition with ID {edition.Id} after {date.ToShortDateString()}");

            return this.BorrowedBookDataService.CountBorrowedBooksByEditionForPersonAfterDate(person, edition, date);
        }

        /// <summary>
        /// Counts the number of books borrowed by a person on a specific date.
        /// </summary>
        /// <param name="person">The person for whom to count the borrowed books.</param>
        /// <param name="date">The date on which to count the borrowed books.</param>
        /// <returns>The number of books borrowed by the person on the specified date.</returns>
        public int CountBooksBorrowedByPersonOnDate(Person person, DateTime date)
        {
            Log.Debug($"Counting books borrowed by Person with ID {person.Id} on {date.ToShortDateString()}");

            return this.BorrowedBookDataService.CountBooksBorrowedByPersonOnDate(person, date);
        }

        /// <summary>
        /// Counts the number of books borrowed by a staff member with 'Suff' attribute on a specific date.
        /// </summary>
        /// <param name="person">The person for whom to count the borrowed books.</param>
        /// <param name="date">The date on which to count the borrowed books.</param>
        /// <returns>The number of books borrowed by the person with 'Suff' attribute on the specified date.</returns>
        public int CountBooksBorrowedBySuffOnDate(Person person, DateTime date)
        {
            Log.Debug($"Counting books borrowed by Person with ID {person.Id} with 'Suff' attribute on {date.ToShortDateString()}");

            return this.BorrowedBookDataService.CountBooksBorrowedBySuffOnDate(person, date);
        }

        /// <summary>
        /// Borrows multiple books at once.
        /// </summary>
        /// <param name="borrowedBooks">The list of borrowed books to be added.</param>
        public void BorrowMultipleBook(List<BorrowedBook> borrowedBooks)
        {
            this.ValidateMultipleBorrow(borrowedBooks);

            Log.Info($"Adding multiple BorrowedBooks");

            int i = 0;
            try
            {
                for (i = 0; i < borrowedBooks.Count; i++)
                {
                    this.ValidateEntity(borrowedBooks[i]);
                    this.BorrowedBookDataService.AddBorrowedBook(borrowedBooks[i]);
                }
            }
            catch (ValidationException ex)
            {
                for (int j = 0; j < i; j++)
                {
                    this.BorrowedBookDataService.DeleteBorrowedBook(borrowedBooks[i]);
                }

                throw new ValidationException($"Validation failed at index {i}; " + ex.Message);
            }
        }

        #region Validations

        /// <summary>
        /// Validates an individual borrowed book entity.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="entity">The borrowed book entity to be validated.</param>
        public override void ValidateEntity<T>(T entity)
        {
            base.ValidateEntity(entity);

            BorrowedBook borrowedBook = entity as BorrowedBook;

            this.VerifyDates(borrowedBook);
            this.VerifyCanBorrowMoreBooks(borrowedBook);

            int multiplier = borrowedBook.Reader.Type == PersonType.Reader ? 1 : 2;
            this.VerifyLimitForPersonReached(borrowedBook, multiplier);
            this.VerifyDomainLimitForPersonReached(borrowedBook, multiplier);
            this.VerifyBorrowExtensionsReached(borrowedBook, multiplier);
            this.VerifyBorrowedTooRecently(borrowedBook, multiplier);
            this.VerifyTodayBorrowsReached(borrowedBook);
            this.VerifyStuffCanBorrow(borrowedBook);
        }

        /// <summary>
        /// Validates a list of borrowed books for addition.
        /// </summary>
        /// <param name="borrowedBooks">The list of borrowed books to be validated.</param>
        private void ValidateMultipleBorrow(List<BorrowedBook> borrowedBooks)
        {
            Log.Debug($"Validating BorrowedBooks list for addition");

            if (borrowedBooks != null && borrowedBooks.Count > 0)
            {
                this.VerifyTooManyBorrowedAtOnce(borrowedBooks);
                this.VerifyDistinctCategories(borrowedBooks);
                this.VerifyIndependentValidation(borrowedBooks);
            }
        }

        /// <summary>
        /// Verifies the independent validation for each borrowed book in the list.
        /// </summary>
        /// <param name="borrowedBooks">The list of borrowed books to be validated independently.</param>
        private void VerifyIndependentValidation(List<BorrowedBook> borrowedBooks)
        {
            foreach (var borrowedBook in borrowedBooks)
            {
                this.ValidateEntity(borrowedBook);
            }
        }

        /// <summary>
        /// Verifies that there are enough distinct categories (domains) for the borrowed books.
        /// </summary>
        /// <param name="borrowedBooks">The list of borrowed books to be checked for distinct categories.</param>
        private void VerifyDistinctCategories(List<BorrowedBook> borrowedBooks)
        {
            if (borrowedBooks.Count > 2)
            {
                List<int> domains = new List<int>();
                foreach (var item in borrowedBooks)
                {
                    foreach (var bookDomain in item.Edition.Book.BookDomains)
                    {
                        domains.Add(bookDomain.Id);
                    }
                }

                int uniqueDomainCount = domains.Distinct().Count();

                if (uniqueDomainCount < 2)
                {
                    throw new ValidationException("Not enough distinct domains");
                }
            }
        }

        /// <summary>
        /// Verifies that the number of borrowed books at once does not exceed the configured limit.
        /// </summary>
        /// <param name="borrowedBooks">The list of borrowed books to be checked for the limit.</param>
        private void VerifyTooManyBorrowedAtOnce(List<BorrowedBook> borrowedBooks)
        {
            int multiplier = borrowedBooks[0].Reader.Type == PersonType.Reader ? 1 : 2;
            int max = Convert.ToInt32(ConfigurationManager.AppSettings["C"]) * multiplier;
            if (borrowedBooks.Count > max)
            {
                throw new ValidationException($"Can borrow a maximum of {max} books at once");
            }
        }

        /// <summary>
        /// Verifies whether a staff member can borrow a book.
        /// </summary>
        /// <param name="borrowedBook">The BorrowedBook instance to be validated.</param>
        private void VerifyStuffCanBorrow(BorrowedBook borrowedBook)
        {
            if (borrowedBook.Staff.Type == PersonType.Reader)
            {
                throw new ValidationException("A reader cannot borrow a book to another reader");
            }

            int maxDay = Convert.ToInt32(ConfigurationManager.AppSettings["PERSIMP"]);
            int borrowedTodayCount = this.CountBooksBorrowedBySuffOnDate(borrowedBook.Staff, DateTime.Today);
            if (borrowedTodayCount >= maxDay)
            {
                throw new ValidationException("The maximum borrows by staff today have been reached");
            }
        }

        /// <summary>
        /// Verifies if the maximum borrows by staff for the day have been reached.
        /// </summary>
        /// <param name="borrowedBook">The BorrowedBook instance to be validated.</param>
        private void VerifyTodayBorrowsReached(BorrowedBook borrowedBook)
        {
            if (borrowedBook.Reader.Type == PersonType.LibraryPersonnel)
            {
                return;
            }

            int maxDay = Convert.ToInt32(ConfigurationManager.AppSettings["NCZ"]);
            int borrowedTodayCount = this.CountBooksBorrowedByPersonOnDate(borrowedBook.Reader, DateTime.Today);
            if (borrowedTodayCount >= maxDay)
            {
                throw new ValidationException("The maximum borrows today have been reached");
            }
        }

        /// <summary>
        /// Verifies if the interval since the last borrow has passed.
        /// </summary>
        /// <param name="borrowedBook">The BorrowedBook instance to be validated.</param>
        /// <param name="multiplier">The multiplier value for certain calculations.</param>
        private void VerifyBorrowedTooRecently(BorrowedBook borrowedBook, int multiplier)
        {
            int period = Convert.ToInt32(ConfigurationManager.AppSettings["DELTA"]) / multiplier;
            DateTime date = DateTime.Today.AddDays(-period);
            int borrowInPeriod = this.CountBorrowedBooksByEditionForPersonAfterDate(borrowedBook.Reader, borrowedBook.Edition, date);
            if (borrowInPeriod > 0)
            {
                throw new ValidationException("The interval since the last borrow has not passed");
            }
        }

        /// <summary>
        /// Verifies if the limit for borrow time extensions in the interval has been reached.
        /// </summary>
        /// <param name="borrowedBook">The BorrowedBook instance to be validated.</param>
        /// <param name="multiplier">The multiplier value for certain calculations.</param>
        private void VerifyBorrowExtensionsReached(BorrowedBook borrowedBook, int multiplier)
        {
            int limitExtraDays = Convert.ToInt32(ConfigurationManager.AppSettings["LIM"]) * multiplier;
            DateTime date = DateTime.Today.AddMonths(-3);
            List<int> list = this.GetDueDateDifferencesForPersonAfterDate(borrowedBook.Reader, date);
            int extraTaken = list.Select(element => element - BorrowPeriodDays).Sum();
            if ((borrowedBook.DueDate - borrowedBook.BorrowDate).Days > BorrowPeriodDays && extraTaken >= limitExtraDays)
            {
                throw new ValidationException("The limit for borrow time extensions in the interval has been reached");
            }
        }

        /// <summary>
        /// Verifies if the borrow domain limit in the interval has been reached for the person.
        /// </summary>
        /// <param name="borrowedBook">The BorrowedBook instance to be validated.</param>
        /// <param name="multiplier">The multiplier value for certain calculations.</param>
        private void VerifyDomainLimitForPersonReached(BorrowedBook borrowedBook, int multiplier)
        {
            int maxSameDomain = Convert.ToInt32(ConfigurationManager.AppSettings["D"]) * multiplier;
            int period = Convert.ToInt32(ConfigurationManager.AppSettings["L"]);
            DateTime date = DateTime.Today.AddMonths(-period);
            foreach (var bookDomain in borrowedBook.Edition.Book.BookDomains)
            {
                int sameDomainCount = this.CountBorrowedBooksForPersonAndDomainAfterDate(borrowedBook.Reader, bookDomain, date);
                if (sameDomainCount >= maxSameDomain)
                {
                    throw new ValidationException($"The borrow domain limit in the interval has been reached for {bookDomain.Name}");
                }
            }
        }

        /// <summary>
        /// Verifies if the borrow limit in the interval has been reached for the person.
        /// </summary>
        /// <param name="borrowedBook">The BorrowedBook instance to be validated.</param>
        /// <param name="multiplier">The multiplier value for certain calculations.</param>
        private void VerifyLimitForPersonReached(BorrowedBook borrowedBook, int multiplier)
        {
            int maxBooksCanBorrow = Convert.ToInt32(ConfigurationManager.AppSettings["NMC"]) * multiplier;
            int period = Convert.ToInt32(ConfigurationManager.AppSettings["PER"]) / multiplier;
            DateTime date = DateTime.Today.AddDays(-period);
            int borrowedBooksCount = this.CountBorrowedSinceDateForPerson(borrowedBook.Reader, date);
            if (borrowedBooksCount >= maxBooksCanBorrow)
            {
                throw new ValidationException("The borrow limit in the interval has been reached");
            }
        }

        /// <summary>
        /// Verifies if the person can borrow more books based on the edition's availability.
        /// </summary>
        /// <param name="borrowedBook">The BorrowedBook instance to be validated.</param>
        private void VerifyCanBorrowMoreBooks(BorrowedBook borrowedBook)
        {
            if (!this.CanBorrowMoreBooks(borrowedBook.Edition))
            {
                throw new ValidationException("There are not enough books to be borrowed");
            }
        }

        /// <summary>
        /// Verifies the dates (BorrowDate, DueDate, and ReturnedDate) in the BorrowedBook instance.
        /// </summary>
        /// <param name="borrowedBook">The BorrowedBook instance to be validated.</param>
        private void VerifyDates(BorrowedBook borrowedBook)
        {
            if (borrowedBook == null)
            {
                return;
            }

            if (borrowedBook.BorrowDate > borrowedBook.DueDate)
            {
                throw new ValidationException("The BorrowDate cannot be later than the DueDate");
            }

            if (borrowedBook.ReturnedDate == null)
            {
                return;
            }

            if (borrowedBook.BorrowDate > borrowedBook.ReturnedDate)
            {
                throw new ValidationException("The BorrowDate cannot be later than the ReturnedDate");
            }
        }

        #endregion
    }
}