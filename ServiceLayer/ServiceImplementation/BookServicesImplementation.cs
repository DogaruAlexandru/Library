// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BookServicesImplementation.cs" company="Transilvania University of Brasov">
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
    /// Represents the implementation of the service for managing Book entities.
    /// </summary>
    public class BookServicesImplementation : BaseService, IBookService
    {
        /// <summary>
        /// The logger instance for this class.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(BookDomainServicesImplementation));

        /// <summary>
        /// Adds a new book to the system.
        /// </summary>
        /// <param name="book">The book to be added.</param>
        public void AddBook(Book book)
        {
            this.ValidateEntity(book);

            Log.Info($"Adding Book with ID: {book.Id}");

            DAOFactoryMethod.CurrentDAOFactory.BookDataService.AddBook(book);
        }

        /// <summary>
        /// Deletes a book from the system.
        /// </summary>
        /// <param name="book">The book to be deleted.</param>
        public void DeleteBook(Book book)
        {
            Log.Debug($"Deleting Book with ID: {book.Id}");

            DAOFactoryMethod.CurrentDAOFactory.BookDataService.DeleteBook(book);
        }

        /// <summary>
        /// Gets all books in the system.
        /// </summary>
        /// <returns>A list of all books.</returns>
        public IList<Book> GetAllBooks()
        {
            Log.Debug("Getting all Books.");

            return DAOFactoryMethod.CurrentDAOFactory.BookDataService.GetAllBooks();
        }

        /// <summary>
        /// Gets a specific book by its ID.
        /// </summary>
        /// <param name="id">The ID of the book to retrieve.</param>
        /// <returns>The book with the specified ID.</returns>
        public Book GetBookById(int id)
        {
            Log.Debug($"Getting Book with ID: {id}");

            return DAOFactoryMethod.CurrentDAOFactory.BookDataService.GetBookById(id);
        }

        /// <summary>
        /// Updates an existing book in the system.
        /// </summary>
        /// <param name="book">The book to be updated.</param>
        public void UpdateBook(Book book)
        {
            this.ValidateEntity(book);

            Log.Info($"Updating Book with ID: {book.Id}");

            DAOFactoryMethod.CurrentDAOFactory.BookDataService.UpdateBook(book);
        }

        /// <inheritdoc/>
        public override void ValidateEntity<T>(T entity)
        {
            base.ValidateEntity(entity);

            Book book = entity as Book;

            this.VerifyDifferentDomainRoots(book);
            this.VerifyLessBookDomainsThenMax(book);
        }

        /// <summary>
        /// Verifies that the book has a maximum number of allowed book domains.
        /// </summary>
        /// <param name="book">The book to be validated.</param>
        private void VerifyLessBookDomainsThenMax(Book book)
        {
            int maxDomainCount = Convert.ToInt32(ConfigurationManager.AppSettings["DOMENII"]);
            if (book.BookDomains.Count() > maxDomainCount)
            {
                throw new ValidationException($"A Book cannot have more than {maxDomainCount} BookDomains");
            }
        }

        /// <summary>
        /// Verifies that the book domains have different root domains.
        /// </summary>
        /// <param name="book">The book to be validated.</param>
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
