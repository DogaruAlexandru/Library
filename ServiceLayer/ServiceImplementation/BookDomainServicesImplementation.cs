using DataMapper;
using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ServiceImplementation
{
    public class BookDomainServicesImplementation : BaseService, IBookDomainService
    {
        public void AddBookDomain(BookDomain bookDomain)
        {
            ValidateEntity(bookDomain);
            DAOFactoryMethod.CurrentDAOFactory.BookDomainDataService.AddBookDomain(bookDomain);
        }

        public void DeleteBookDomain(BookDomain bookDomain)
        {
            DAOFactoryMethod.CurrentDAOFactory.BookDomainDataService.DeleteBookDomain(bookDomain);
        }

        public IList<BookDomain> GetAllBookDomains()
        {
            return DAOFactoryMethod.CurrentDAOFactory.BookDomainDataService.GetAllBookDomains();
        }

        public BookDomain GetBookDomainById(int id)
        {
            return DAOFactoryMethod.CurrentDAOFactory.BookDomainDataService.GetBookDomainById(id);
        }

        public void UpdateBookDomain(BookDomain bookDomain)
        {
            ValidateEntity(bookDomain);
            DAOFactoryMethod.CurrentDAOFactory.BookDomainDataService.UpdateBookDomain(bookDomain);
        }
    }
}
