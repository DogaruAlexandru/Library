﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BookDomainServicesImplementation.cs" company="Transilvania University of Brasov">
//   Copyright (c) Dogaru Alexandru.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ServiceLayer.ServiceImplementation
{
    using System;
    using System.Collections.Generic;
    using DataMapper;
    using DomainModel;
    using log4net;

    /// <summary>
    /// Represents the implementation of the <see cref="IBookDomainService"/> for book domain-related operations.
    /// </summary>
    public class BookDomainServicesImplementation : BaseService, IBookDomainService
    {
        /// <summary>
        /// Represents the logger instance for the <see cref="BookDomainServicesImplementation"/> class.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(BookDomainServicesImplementation));

        /// <summary>
        /// Adds a new book domain.
        /// </summary>
        /// <param name="bookDomain">The book domain to be added.</param>
        public void AddBookDomain(BookDomain bookDomain)
        {
            this.ValidateEntity(bookDomain);

            Log.Info($"Adding BookDomain with ID: {bookDomain.Id}");

            DAOFactoryMethod.CurrentDAOFactory.BookDomainDataService.AddBookDomain(bookDomain);
        }

        /// <summary>
        /// Deletes an existing book domain.
        /// </summary>
        /// <param name="bookDomain">The book domain to be deleted.</param>
        public void DeleteBookDomain(BookDomain bookDomain)
        {
            Log.Debug($"Deleting BookDomain with ID: {bookDomain.Id}");

            DAOFactoryMethod.CurrentDAOFactory.BookDomainDataService.DeleteBookDomain(bookDomain);
        }

        /// <summary>
        /// Retrieves all book domains.
        /// </summary>
        /// <returns>A list of all book domains.</returns>
        public IList<BookDomain> GetAllBookDomains()
        {
            Log.Debug("Getting all BookDomains.");

            return DAOFactoryMethod.CurrentDAOFactory.BookDomainDataService.GetAllBookDomains();
        }

        /// <summary>
        /// Retrieves a book domain by its ID.
        /// </summary>
        /// <param name="id">The ID of the book domain to retrieve.</param>
        /// <returns>The book domain with the specified ID.</returns>
        public BookDomain GetBookDomainById(int id)
        {
            Log.Debug($"Getting BookDomain with ID: {id}");

            return DAOFactoryMethod.CurrentDAOFactory.BookDomainDataService.GetBookDomainById(id);
        }

        /// <summary>
        /// Updates an existing book domain.
        /// </summary>
        /// <param name="bookDomain">The book domain to be updated.</param>
        public void UpdateBookDomain(BookDomain bookDomain)
        {
            this.ValidateEntity(bookDomain);

            Log.Info($"Updating BookDomain with ID: {bookDomain.Id}");

            DAOFactoryMethod.CurrentDAOFactory.BookDomainDataService.UpdateBookDomain(bookDomain);
        }
    }
}
