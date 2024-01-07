// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuthorServicesImplementation.cs" company="Transilvania University of Brasov">
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
    /// Represents the implementation of the service for Author entities.
    /// </summary>
    /// <remarks>
    /// This class provides methods for adding, deleting, retrieving, and updating Author entities.
    /// </remarks>
    public class AuthorServicesImplementation : BaseService, IAuthorService
    {
        /// <summary>
        /// Represents the logger instance for the <see cref="AuthorServicesImplementation"/> class.
        /// </summary>
        /// <remarks>
        /// The logger is responsible for capturing and logging events and messages related to Author services.
        /// </remarks>
        private static readonly ILog Log = LogManager.GetLogger(typeof(BookDomainServicesImplementation));

        /// <summary>
        /// Adds an Author entity.
        /// </summary>
        /// <param name="author">The Author entity to be added.</param>
        public void AddAuthor(Author author)
        {
            this.ValidateEntity(author);

            Log.Info($"Adding Author with ID: {author.Id}");

            DAOFactoryMethod.CurrentDAOFactory.AuthorDataService.AddAuthor(author);
        }

        /// <summary>
        /// Deletes an Author entity.
        /// </summary>
        /// <param name="author">The Author entity to be deleted.</param>
        public void DeleteAuthor(Author author)
        {
            Log.Debug($"Deleting Author with ID: {author.Id}");

            DAOFactoryMethod.CurrentDAOFactory.AuthorDataService.DeleteAuthor(author);
        }

        /// <summary>
        /// Retrieves all Author entities.
        /// </summary>
        /// <returns>A list of all Author entities.</returns>
        public IList<Author> GetAllAuthors()
        {
            Log.Debug("Getting all Authors.");

            return DAOFactoryMethod.CurrentDAOFactory.AuthorDataService.GetAllAuthors();
        }

        /// <summary>
        /// Retrieves an Author entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the Author entity to retrieve.</param>
        /// <returns>The Author entity with the specified ID.</returns>
        public Author GetAuthorById(int id)
        {
            Log.Debug($"Getting Author with ID: {id}");

            return DAOFactoryMethod.CurrentDAOFactory.AuthorDataService.GetAuthorById(id);
        }

        /// <summary>
        /// Updates an Author entity.
        /// </summary>
        /// <param name="author">The Author entity to be updated.</param>
        public void UpdateAuthor(Author author)
        {
            this.ValidateEntity(author);

            Log.Info($"Updating Author with ID: {author.Id}");

            DAOFactoryMethod.CurrentDAOFactory.AuthorDataService.UpdateAuthor(author);
        }
    }
}
