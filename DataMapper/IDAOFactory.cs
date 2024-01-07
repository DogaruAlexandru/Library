// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDAOFactory.cs" company="Transilvania University of Brasov">
//   Copyright (c) Dogaru Alexandru.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace DataMapper.SqlServerDao
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Interface for creating Data Access Objects (DAO) related to SQL Server database.
    /// </summary>
    public interface IDAOFactory
    {
        /// <summary>
        /// Gets the Data Access Object (DAO) for author-related data.
        /// </summary>
        IAuthorDataService AuthorDataService { get; }

        /// <summary>
        /// Gets the Data Access Object (DAO) for book-related data.
        /// </summary>
        IBookDataService BookDataService { get; }

        /// <summary>
        /// Gets the Data Access Object (DAO) for book domain-related data.
        /// </summary>
        IBookDomainDataService BookDomainDataService { get; }

        /// <summary>
        /// Gets the Data Access Object (DAO) for borrowed book-related data.
        /// </summary>
        IBorrowedBookDataService BorrowedBookDataService { get; }

        /// <summary>
        /// Gets the Data Access Object (DAO) for edition-related data.
        /// </summary>
        IEditionDataService EditionDataService { get; }

        /// <summary>
        /// Gets the Data Access Object (DAO) for person-related data.
        /// </summary>
        IPersonDataService PersonDataService { get; }
    }
}
