using DataMapper;
using DomainModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ServiceImplementation
{
    public class BookServicesImplementation : BaseService, IBookService
    {
        public void AddBook(Book book)
        {
            ValidateEntity(book);
            DAOFactoryMethod.CurrentDAOFactory.BookDataService.AddBook(book);
        }

        public void DeleteBook(Book book)
        {
            DAOFactoryMethod.CurrentDAOFactory.BookDataService.DeleteBook(book);
        }

        public IList<Book> GetAllBooks()
        {
            return DAOFactoryMethod.CurrentDAOFactory.BookDataService.GetAllBooks();
        }

        public Book GetBookById(int id)
        {
            return DAOFactoryMethod.CurrentDAOFactory.BookDataService.GetBookById(id);
        }

        public void UpdateBook(Book book)
        {
            ValidateEntity(book);
            DAOFactoryMethod.CurrentDAOFactory.BookDataService.UpdateBook(book);
        }

        public override void ValidateEntity<T>(T entity)
        {
            base.ValidateEntity(entity);

            Book book = entity as Book;

            VerifyDifferentDomainRoots(book);
            VerifyLessBookDomainsThenMax(book);
        }

        private void VerifyLessBookDomainsThenMax(Book book)
        {
            int maxDomainCount = Convert.ToInt32(ConfigurationManager.AppSettings["DOMENII"]);
            if (book.BookDomains.Count() > maxDomainCount)
            {
                throw new ValidationException($"A Book cannot have more then {maxDomainCount} BookDomains");
            }
        }

        private void VerifyDifferentDomainRoots(Book book)
        {
            IBookDomainService service = new BookDomainServicesImplementation();

            List<int> domainsRoots = new List<int>();
            foreach (var bookDomain in book.BookDomains)
            {
                BookDomain aux = bookDomain;
                while (aux.ParentDomain != null)
                {
                    aux = service.GetBookDomainById(aux.ParentDomain.Id);
                }
                domainsRoots.Add(aux.Id);
            }
            var groupedByValue = domainsRoots.GroupBy(i => i);
            var duplicates = groupedByValue.Where(g => g.Count() > 1).Select(g => g.Key).ToList();
            if (duplicates.Any())
            {
                throw new ValidationException("The book domains cannot have the same root domain");
            }
        }
    }
}