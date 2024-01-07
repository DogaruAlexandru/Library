// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SQLServerDAOFactory.cs" company="Transilvania University of Brasov">
//   Copyright (c) Dogaru Alexandru.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace DataMapper
{
    using DataMapper.SqlServerDao;

    /// <summary>
    /// Represents the SQL Server implementation of the Data Access Object (DAO) Factory.
    /// </summary>
    internal class SQLServerDAOFactory : IDAOFactory
    {
        /// <summary>
        /// Gets the data service for Author entities.
        /// </summary>
        /// <returns>The data service for Author entities.</returns>
        public IAuthorDataService AuthorDataService
        {
            get
            {
                return new SQLAuthorDataService();
            }
        }

        /// <summary>
        /// Gets the data service for Book entities.
        /// </summary>
        /// <returns>The data service for Book entities.</returns>
        public IBookDataService BookDataService
        {
            get
            {
                return new SQLBookDataService();
            }
        }

        /// <summary>
        /// Gets the data service for BookDomain entities.
        /// </summary>
        /// <returns>The data service for BookDomain entities.</returns>
        public IBookDomainDataService BookDomainDataService
        {
            get
            {
                return new SQLBookDomainDataService();
            }
        }

        /// <summary>
        /// Gets the data service for BorrowedBook entities.
        /// </summary>
        /// <returns>The data service for BorrowedBook entities.</returns>
        public IBorrowedBookDataService BorrowedBookDataService
        {
            get
            {
                return new SQLBorrowedBookDataService();
            }
        }

        /// <summary>
        /// Gets the data service for Edition entities.
        /// </summary>
        /// <returns>The data service for Edition entities.</returns>
        public IEditionDataService EditionDataService
        {
            get
            {
                return new SQLEditionDataService();
            }
        }

        /// <summary>
        /// Gets the data service for Person entities.
        /// </summary>
        /// <returns>The data service for Person entities.</returns>
        public IPersonDataService PersonDataService
        {
            get
            {
                return new SQLPersonDataService();
            }
        }
    }
}
