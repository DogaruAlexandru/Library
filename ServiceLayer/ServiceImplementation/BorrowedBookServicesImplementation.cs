using DataMapper;
using DomainModel;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ServiceImplementation
{
    public class BorrowedBookServicesImplementation : BaseService, IBorrowedBookService
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(BookDomainServicesImplementation));

        public const int BorrowPeriodDays = 14;

        public void AddBorrowedBook(BorrowedBook borrowedBook)
        {
            ValidateEntity(borrowedBook);

            log.Info($"Adding BorrowedBook with ID: {borrowedBook.Id}");

            DAOFactoryMethod.CurrentDAOFactory.BorrowedBookDataService.AddBorrowedBook(borrowedBook);
        }

        public void DeleteBorrowedBook(BorrowedBook borrowedBook)
        {
            log.Debug($"Deleting BorrowedBook with ID: {borrowedBook.Id}");

            DAOFactoryMethod.CurrentDAOFactory.BorrowedBookDataService.DeleteBorrowedBook(borrowedBook);
        }

        public IList<BorrowedBook> GetAllBorrowedBooks()
        {
            log.Debug("Getting all BorrowedBooks.");

            return DAOFactoryMethod.CurrentDAOFactory.BorrowedBookDataService.GetAllBorrowedBooks();
        }

        public BorrowedBook GetBorrowedBookById(int id)
        {
            log.Debug($"Getting BorrowedBook with ID: {id}");

            return DAOFactoryMethod.CurrentDAOFactory.BorrowedBookDataService.GetBorrowedBookById(id);
        }

        public void UpdateBorrowedBook(BorrowedBook borrowedBook)
        {
            ValidateEntity(borrowedBook);

            log.Info($"Updating BorrowedBook with ID: {borrowedBook.Id}");

            DAOFactoryMethod.CurrentDAOFactory.BorrowedBookDataService.UpdateBorrowedBook(borrowedBook);
        }

        public bool CanBorrowMoreBooks(Edition edition)
        {
            log.Debug($"Verifing Edition can be borrowed with ID: {edition.Id}");

            int borrowedBooksCount = DAOFactoryMethod.CurrentDAOFactory.BorrowedBookDataService
                .CountBorrowedBooksByEditionWithNullReturnedDate(edition);

            return (edition.CanBorrow - borrowedBooksCount) * 10 > edition.CanBorrow + edition.CanNotBorrow;
        }

        public int CountBorrowedSinceDateForPerson(Person person, DateTime date)
        {
            log.Debug($"Counting borrowed books for Person with ID {person.Id} since {date.ToShortDateString()}");

            return DAOFactoryMethod.CurrentDAOFactory.BorrowedBookDataService.CountBorrowedBooksByPersonAndDate(person, date);
        }

        public int CountBorrowedBooksForPersonAndDomainAfterDate(Person person, BookDomain bookDomain, DateTime date)
        {
            log.Debug($"Counting borrowed books for Person with ID {person.Id}, BookDomain with ID {bookDomain.Id} after {date.ToShortDateString()}");

            return DAOFactoryMethod.CurrentDAOFactory.BorrowedBookDataService
                .CountBorrowedBooksForPersonAndDomainAfterDate(person, bookDomain, date);
        }

        public List<int> GetDueDateDifferencesForPersonAfterDate(Person person, DateTime date)
        {
            log.Debug($"Getting due date differences for Person with ID {person.Id} after {date.ToShortDateString()}");

            return DAOFactoryMethod.CurrentDAOFactory.BorrowedBookDataService
                .GetDueDateDifferencesForPersonAfterDate(person, date);
        }

        public int CountBorrowedBooksByEditionForPersonAfterDate(Person person, Edition edition, DateTime date)
        {
            log.Debug($"Counting borrowed books for Person with ID {person.Id}, Edition with ID {edition.Id} after {date.ToShortDateString()}");

            return DAOFactoryMethod.CurrentDAOFactory.BorrowedBookDataService
                .CountBorrowedBooksByEditionForPersonAfterDate(person, edition, date);
        }

        public int CountBooksBorrowedByPersonOnDate(Person person, DateTime date)
        {
            log.Debug($"Counting books borrowed by Person with ID {person.Id} on {date.ToShortDateString()}");

            return DAOFactoryMethod.CurrentDAOFactory.BorrowedBookDataService
                .CountBooksBorrowedByPersonOnDate(person, date);
        }

        public int CountBooksBorrowedBySuffOnDate(Person person, DateTime date)
        {
            log.Debug($"Counting books borrowed by Person with ID {person.Id} with 'Suff' attribute on {date.ToShortDateString()}");

            return DAOFactoryMethod.CurrentDAOFactory.BorrowedBookDataService
                .CountBooksBorrowedBySuffOnDate(person, date);
        }


        public void BorrowMultipleBook(List<BorrowedBook> borrowedBooks)
        {
            ValidateMultipleBorrow(borrowedBooks);

            log.Info($"Adding multiple BorrowedBooks");

            int i = 0;
            try
            {
                for (i = 0; i < borrowedBooks.Count; i++)
                {
                    ValidateEntity(borrowedBooks[i]);
                    DAOFactoryMethod.CurrentDAOFactory.BorrowedBookDataService.AddBorrowedBook(borrowedBooks[i]);
                }
            }
            catch (ValidationException ex)
            {
                for (int j = 0; j < i; j++)
                {
                    DAOFactoryMethod.CurrentDAOFactory.BorrowedBookDataService.DeleteBorrowedBook(borrowedBooks[i]);
                }
                throw new ValidationException($"Validation faild at index {i}; " + ex.Message);
            }
        }

        #region Validations_borrowedBooks
        private void ValidateMultipleBorrow(List<BorrowedBook> borrowedBooks)
        {
            log.Debug($"Validating BorrowedBooks list for addition");

            if (borrowedBooks != null && borrowedBooks.Count > 0)
            {
                VerifyTooManyBorrowedArOnce(borrowedBooks);
                VerifyDistinctCategories(borrowedBooks);
                VerifyIndependentValidation(borrowedBooks);
            }
        }

        private void VerifyIndependentValidation(List<BorrowedBook> borrowedBooks)
        {
            foreach (var borrowedBook in borrowedBooks)
            {
                ValidateEntity(borrowedBook);
            }
        }

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

        private void VerifyTooManyBorrowedArOnce(List<BorrowedBook> borrowedBooks)
        {
            int multiplyer = borrowedBooks[0].Reader.Type == PersonType.Reader ? 1 : 2;
            int max = Convert.ToInt32(ConfigurationManager.AppSettings["C"]) * multiplyer;
            if (borrowedBooks.Count > max)
            {
                throw new ValidationException($"Can borrow maximum {max} books at once");
            }
        }
        #endregion

        #region Validations_borrowedBook
        public override void ValidateEntity<T>(T entity)
        {
            base.ValidateEntity(entity);

            BorrowedBook borrowedBook = entity as BorrowedBook;

            VerifyDates(borrowedBook);
            VerifyCanBorrowMoreBooks(borrowedBook);

            int multiplyer = borrowedBook.Reader.Type == PersonType.Reader ? 1 : 2;
            VerifyLimitForPersonReached(borrowedBook, multiplyer);
            VerifyDomainLimitForPersonReached(borrowedBook, multiplyer);
            VerifyBorrowExtensionsReached(borrowedBook, multiplyer);
            VerifyBorrowedTooRecently(borrowedBook, multiplyer);
            VerifyTodayBorrowsReached(borrowedBook);
            VerifyStuffCanBorrow(borrowedBook);
        }

        private void VerifyStuffCanBorrow(BorrowedBook borrowedBook)
        {
            if (borrowedBook.Staff.Type == PersonType.Reader)
            {
                throw new ValidationException("Reader cannot borrow book to other reader");
            }

            int maxDay = Convert.ToInt32(ConfigurationManager.AppSettings["PERSIMP"]);
            int borrowedTodayCount = CountBooksBorrowedBySuffOnDate(borrowedBook.Staff, DateTime.Today);
            if (borrowedTodayCount >= maxDay)
            {
                throw new ValidationException("Maximum borrows by stuff today reached");
            }
        }

        private void VerifyTodayBorrowsReached(BorrowedBook borrowedBook)
        {
            if (borrowedBook.Reader.Type == PersonType.LibraryPersonnel)
            {
                return;
            }
            int maxDay = Convert.ToInt32(ConfigurationManager.AppSettings["NCZ"]);
            int borrowedTodayCount = CountBooksBorrowedByPersonOnDate(borrowedBook.Reader, DateTime.Today);
            if (borrowedTodayCount >= maxDay)
            {
                throw new ValidationException("Maximum borrows today reached");
            }
        }

        private void VerifyBorrowedTooRecently(BorrowedBook borrowedBook, int multiplyer)
        {
            int period = Convert.ToInt32(ConfigurationManager.AppSettings["DELTA"]) / multiplyer;
            DateTime date = DateTime.Today.AddDays(-period);
            int borrowInPeriod = CountBorrowedBooksByEditionForPersonAfterDate(borrowedBook.Reader, borrowedBook.Edition, date);
            if (borrowInPeriod > 0)
            {
                throw new ValidationException("Interval since last borrow has not pass");
            }
        }

        private void VerifyBorrowExtensionsReached(BorrowedBook borrowedBook, int multiplyer)
        {
            int limitExtraDays = Convert.ToInt32(ConfigurationManager.AppSettings["LIM"]) * multiplyer;
            DateTime date = DateTime.Today.AddMonths(-3);
            List<int> list = GetDueDateDifferencesForPersonAfterDate(borrowedBook.Reader, date);
            int extraTaken = list.Select(element => element - BorrowPeriodDays).Sum();
            if ((borrowedBook.DueDate - borrowedBook.BorrowDate).Days > BorrowPeriodDays && extraTaken >= limitExtraDays)
            {
                throw new ValidationException("Borrow time extensions limit in interval reached");
            }
        }

        private void VerifyDomainLimitForPersonReached(BorrowedBook borrowedBook, int multiplyer)
        {
            int maxSameDomain = Convert.ToInt32(ConfigurationManager.AppSettings["D"]) * multiplyer;
            int period = Convert.ToInt32(ConfigurationManager.AppSettings["L"]);
            DateTime date = DateTime.Today.AddMonths(-period);
            foreach (var bookDomain in borrowedBook.Edition.Book.BookDomains)
            {
                int sameDomainCount = CountBorrowedBooksForPersonAndDomainAfterDate(borrowedBook.Reader, bookDomain, date);
                if (sameDomainCount >= maxSameDomain)
                {
                    throw new ValidationException($"Borrow domain limit in interval reached for {bookDomain.Name}");
                }
            }
        }

        private void VerifyLimitForPersonReached(BorrowedBook borrowedBook, int multiplyer)
        {
            int maxBooksCanBorrow = Convert.ToInt32(ConfigurationManager.AppSettings["NMC"]) * multiplyer;
            int period = Convert.ToInt32(ConfigurationManager.AppSettings["PER"]) / multiplyer;
            DateTime date = DateTime.Today.AddDays(-period);
            int borrowedBooksCount = CountBorrowedSinceDateForPerson(borrowedBook.Reader, date);
            if (borrowedBooksCount >= maxBooksCanBorrow)
            {
                throw new ValidationException("Borrow limit in interval reached");
            }
        }

        private void VerifyCanBorrowMoreBooks(BorrowedBook borrowedBook)
        {
            if (!CanBorrowMoreBooks(borrowedBook.Edition))
            {
                throw new ValidationException("Not enough books to be borrowed");
            }
        }

        private void VerifyDates(BorrowedBook borrowedBook)
        {
            if (borrowedBook == null)
            {
                return;
            }

            if (borrowedBook.BorrowDate > borrowedBook.DueDate)
            {
                throw new ValidationException("BorrowDate connot be later then DueDate");
            }

            if (borrowedBook.ReturnedDate == null)
            {
                return;
            }
            if (borrowedBook.BorrowDate > borrowedBook.ReturnedDate)
            {
                throw new ValidationException("BorrowDate connot be later then ReturnedDate");
            }
        }
        #endregion
    }
}