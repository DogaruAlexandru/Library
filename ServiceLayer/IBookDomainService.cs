using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public interface IBookDomainService : IValidationService
    {
        IList<BookDomain> GetAllBookDomains();

        BookDomain GetBookDomainById(int id);

        void AddBookDomain(BookDomain bookDomain);

        void DeleteBookDomain(BookDomain bookDomain);

        void UpdateBookDomain(BookDomain bookDomain);
    }
}
