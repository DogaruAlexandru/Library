using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public interface IBookService : IValidationService
    {
        IList<Book> GetAllBooks();

        Book GetBookById(int id);

        void AddBook(Book book);

        void DeleteBook(Book book);

        void UpdateBook(Book book);
    }
}
