// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EditionServiceImplementation.cs" company="Transilvania University of Brasov">
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
    /// Provides implementation for the IEditionService interface.
    /// </summary>
    public class EditionServiceImplementation : BaseService, IEditionService
    {
        /// <summary>
        /// Represents the logger instance for logging EditionServiceImplementation class.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(BookDomainServicesImplementation));

        /// <summary>
        /// Initializes a new instance of the <see cref="EditionServiceImplementation"/> class.
        /// </summary>
        /// <param name="editionDataService">The data service for editions.</param>
        public EditionServiceImplementation(IEditionDataService editionDataService)
        {
            this.EditionDataService = editionDataService;
        }

        /// <summary>
        /// Gets or sets the data service for editions.
        /// </summary>
        private IEditionDataService EditionDataService { get; set; }

        /// <summary>
        /// Adds a new edition.
        /// </summary>
        /// <param name="edition">The edition to be added.</param>
        public void AddEdition(Edition edition)
        {
            this.ValidateEntity(edition);

            Log.Info($"Adding Edition with ID: {edition.Id}");

            this.EditionDataService.AddEdition(edition);
        }

        /// <summary>
        /// Deletes an existing edition.
        /// </summary>
        /// <param name="edition">The edition to be deleted.</param>
        public void DeleteEdition(Edition edition)
        {
            Log.Debug($"Deleting Edition with ID: {edition.Id}");

            this.EditionDataService.DeleteEdition(edition);
        }

        /// <summary>
        /// Retrieves all editions.
        /// </summary>
        /// <returns>A list of all editions.</returns>
        public IList<Edition> GetAllEditions()
        {
            Log.Debug("Getting all Editions.");

            return this.EditionDataService.GetAllEditions();
        }

        /// <summary>
        /// Retrieves an edition by its ID.
        /// </summary>
        /// <param name="id">The ID of the edition to be retrieved.</param>
        /// <returns>The edition with the specified ID.</returns>
        public Edition GetEditionById(int id)
        {
            Log.Debug($"Getting Edition with ID: {id}");

            return this.EditionDataService.GetEditionById(id);
        }

        /// <summary>
        /// Updates an existing edition.
        /// </summary>
        /// <param name="edition">The edition to be updated.</param>
        public void UpdateEdition(Edition edition)
        {
            this.ValidateEntity(edition);

            Log.Info($"Updating Edition with ID: {edition.Id}");

            this.EditionDataService.UpdateEdition(edition);
        }
    }
}
