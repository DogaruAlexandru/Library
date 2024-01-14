// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DAOFactoryMethod.cs" company="Transilvania University of Brasov">
//   Copyright (c) Dogaru Alexandru.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace DataMapper
{
    using System;
    using System.Configuration;
    using DataMapper.SqlServerDao;

    /// <summary>
    /// Factory method for creating Data Access Objects (DAO) factories based on the configured data provider.
    /// </summary>
    public static class DAOFactoryMethod
    {
        /// <summary>
        /// The current Data Access Object (DAO) factory.
        /// </summary>
        private static readonly IDAOFactory ThisCurrentDAOFactory;

        /// <summary>
        /// Initializes static members of the <see cref="DAOFactoryMethod"/> class.
        /// </summary>
        static DAOFactoryMethod()
        {
            string currentDataProvider = ConfigurationManager.AppSettings["dataProvider"];
            if (string.IsNullOrWhiteSpace(currentDataProvider))
            {
                ThisCurrentDAOFactory = null;
            }
            else
            {
                switch (currentDataProvider.ToLower().Trim())
                {
                    case "sqlserver":
                        ThisCurrentDAOFactory = new SQLServerDAOFactory();
                        break;
                    case "oracle":
                        ThisCurrentDAOFactory = null; // In practice, this should be a new OracleDaoFactory, but it's not yet implemented.
                        return;
                    default:
                        ThisCurrentDAOFactory = new SQLServerDAOFactory();
                        break;
                }
            }
        }

        /// <summary>
        /// Gets the current DAO factory.
        /// </summary>
        /// <value>The current DAO factory.</value>
        public static IDAOFactory CurrentDAOFactory
        {
            get
            {
                return ThisCurrentDAOFactory;
            }
        }
    }
}
