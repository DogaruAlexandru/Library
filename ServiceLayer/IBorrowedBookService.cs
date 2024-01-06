using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public interface IBorrowedBookService : IValidationService
    {
        IList<BorrowedBook> GetAllBorrowedBooks();

        BorrowedBook GetBorrowedBookById(int id);

        void AddBorrowedBook(BorrowedBook borrowedBook);

        void DeleteBorrowedBook(BorrowedBook borrowedBook);

        void UpdateBorrowedBook(BorrowedBook borrowedBook);

        bool CanBorrowMoreBooks(Edition edition);

        int CountBorrowedSinceDateForPerson(Person person, DateTime date);

        int CountBorrowedBooksForPersonAndDomainAfterDate(Person person, BookDomain bookDomain, DateTime date);

        List<int> GetDueDateDifferencesForPersonAfterDate(Person person, DateTime date);

        int CountBorrowedBooksByEditionForPersonAfterDate(Person person, Edition edition, DateTime date);

        int CountBooksBorrowedByPersonOnDate(Person person, DateTime date);

        int CountBooksBorrowedBySuffOnDate(Person person, DateTime date);

        void BorrowMultipleBook(List<BorrowedBook> borrowedBooks);
    }
}
