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
        /// Initializes a new instance of the <see cref="AuthorServicesImplementation"/> class.
        /// </summary>
        /// <param name="authorDataService">The data service for authors.</param>
        public AuthorServicesImplementation(IAuthorDataService authorDataService)
        {
            this.AuthorDataService = authorDataService;
        }

        /// <summary>
        /// Gets or sets the data service for authors.
        /// </summary>
        private IAuthorDataService AuthorDataService { get; set; }

        /// <summary>
        /// Adds an Author entity.
        /// </summary>
        /// <param name="author">The Author entity to be added.</param>
        public void AddAuthor(Author author)
        {
            this.ValidateEntity(author);

            Log.Info($"Adding Author with ID: {author.Id}");

            this.AuthorDataService.AddAuthor(author);
        }

        /// <summary>
        /// Deletes an Author entity.
        /// </summary>
        /// <param name="author">The Author entity to be deleted.</param>
        public void DeleteAuthor(Author author)
        {
            Log.Debug($"Deleting Author with ID: {author.Id}");

            this.AuthorDataService.DeleteAuthor(author);
        }

        /// <summary>
        /// Retrieves all Author entities.
        /// </summary>
        /// <returns>A list of all Author entities.</returns>
        public IList<Author> GetAllAuthors()
        {
            Log.Debug("Getting all Authors.");

            return this.AuthorDataService.GetAllAuthors();
        }

        /// <summary>
        /// Retrieves an Author entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the Author entity to retrieve.</param>
        /// <returns>The Author entity with the specified ID.</returns>
        public Author GetAuthorById(int id)
        {
            Log.Debug($"Getting Author with ID: {id}");

            return this.AuthorDataService.GetAuthorById(id);
        }

        /// <summary>
        /// Updates an Author entity.
        /// </summary>
        /// <param name="author">The Author entity to be updated.</param>
        public void UpdateAuthor(Author author)
        {
            this.ValidateEntity(author);

            Log.Info($"Updating Author with ID: {author.Id}");

            this.AuthorDataService.UpdateAuthor(author);
        }
    }
}
