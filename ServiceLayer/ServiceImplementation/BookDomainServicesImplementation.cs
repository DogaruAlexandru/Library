using DataMapper;
using DomainModel;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ServiceImplementation
{
    public class BookDomainServicesImplementation : BaseService, IBookDomainService
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(BookDomainServicesImplementation));

        public void AddBookDomain(BookDomain bookDomain)
        {
            ValidateEntity(bookDomain);

            log.Info($"Adding BookDomain with ID: {bookDomain.Id}");

            DAOFactoryMethod.CurrentDAOFactory.BookDomainDataService.AddBookDomain(bookDomain);
        }

        public void DeleteBookDomain(BookDomain bookDomain)
        {
            log.Debug($"Deleting BookDomain with ID: {bookDomain.Id}");

            DAOFactoryMethod.CurrentDAOFactory.BookDomainDataService.DeleteBookDomain(bookDomain);
        }

        public IList<BookDomain> GetAllBookDomains()
        {
            log.Debug("Getting all BookDomains.");

            return DAOFactoryMethod.CurrentDAOFactory.BookDomainDataService.GetAllBookDomains();
        }

        public BookDomain GetBookDomainById(int id)
        {
            log.Debug($"Getting BookDomain with ID: {id}");

            return DAOFactoryMethod.CurrentDAOFactory.BookDomainDataService.GetBookDomainById(id);
        }

        public void UpdateBookDomain(BookDomain bookDomain)
        {
            ValidateEntity(bookDomain);

            log.Info($"Updating BookDomain with ID: {bookDomain.Id}");

            DAOFactoryMethod.CurrentDAOFactory.BookDomainDataService.UpdateBookDomain(bookDomain);
        }
    }
}
