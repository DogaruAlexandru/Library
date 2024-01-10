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
        /// Initializes a new instance of the <see cref="BookServicesImplementation"/> class.
        /// </summary>
        /// <param name="bookDataService">The data service for books.</param>
        /// <param name="bookDomainDataService">The data service for bookDomain.</param>
        public BookServicesImplementation(IBookDataService bookDataService, IBookDomainDataService bookDomainDataService)
        {
            this.BookDataService = bookDataService;
            this.BookDomainDataService = bookDomainDataService;
        }

        /// <summary>
        /// Gets or sets the data service for books.
        /// </summary>
        private IBookDataService BookDataService { get; set; }

        /// <summary>
        /// Gets or sets the data service for booksDomain.
        /// </summary>
        private IBookDomainDataService BookDomainDataService { get; set; }

        /// <summary>
        /// Adds a new book to the system.
        /// </summary>
        /// <param name="book">The book to be added.</param>
        public void AddBook(Book book)
        {
            try
            {
                this.ValidateEntity(book);
            }
            catch (ValidationException ex)
            {
                throw ex;
            }

            Log.Info($"Adding Book with ID: {book.Id}");

            this.BookDataService.AddBook(book);
        }

        /// <summary>
        /// Deletes a book from the system.
        /// </summary>
        /// <param name="book">The book to be deleted.</param>
        public void DeleteBook(Book book)
        {
            Log.Debug($"Deleting Book with ID: {book.Id}");

            this.BookDataService.DeleteBook(book);
        }

        /// <summary>
        /// Gets all books in the system.
        /// </summary>
        /// <returns>A list of all books.</returns>
        public IList<Book> GetAllBooks()
        {
            Log.Debug("Getting all Books.");

            return this.BookDataService.GetAllBooks();
        }

        /// <summary>
        /// Gets a specific book by its ID.
        /// </summary>
        /// <param name="id">The ID of the book to retrieve.</param>
        /// <returns>The book with the specified ID.</returns>
        public Book GetBookById(int id)
        {
            Log.Debug($"Getting Book with ID: {id}");

            return this.BookDataService.GetBookById(id);
        }

        /// <summary>
        /// Updates an existing book in the system.
        /// </summary>
        /// <param name="book">The book to be updated.</param>
        public void UpdateBook(Book book)
        {
            try
            {
                this.ValidateEntity(book);
            }
            catch (ValidationException ex)
            {
                throw ex;
            }

            Log.Info($"Updating Book with ID: {book.Id}");

            this.BookDataService.UpdateBook(book);
        }

        /// <inheritdoc/>
        public override void ValidateEntity<T>(T entity)
        {
            try
            {
                base.ValidateEntity(entity);

                Book book = entity as Book;

                this.VerifyDifferentDomainRoots(book);
                this.VerifyLessBookDomainsThenMax(book);
            }
            catch (ValidationException ex)
            {
                throw ex;
            }
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
            IBookDomainService service = new BookDomainServicesImplementation(this.BookDomainDataService);

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
