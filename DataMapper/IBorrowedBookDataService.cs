using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper
{
    public interface IBorrowedBookDataService
    {
        IList<BorrowedBook> GetAllBorrowedBooks();

        BorrowedBook GetBorrowedBookById(int id);

        void AddBorrowedBook(BorrowedBook BorrowedBook);

        void DeleteBorrowedBook(BorrowedBook BorrowedBook);

        void UpdateBorrowedBook(BorrowedBook BorrowedBook);
    }
}
