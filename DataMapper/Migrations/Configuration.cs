// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Configuration.cs" company="Transilvania University of Brasov">
//   Copyright (c) Dogaru Alexandru.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace DataMapper.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    /// <summary>
    /// Configuration class for database migrations.
    /// </summary>
    internal sealed class Configuration : DbMigrationsConfiguration<DataMapper.SqlServerDao.MyApplicationContext>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Configuration"/> class.
        /// </summary>
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = false;
        }

        /// <summary>
        /// Seed method that will be called after migrating to the latest version.
        /// </summary>
        /// <param name="context">The database context.</param>
        protected override void Seed(DataMapper.SqlServerDao.MyApplicationContext context)
        {
            ////  This method will be called after migrating to the latest version.

            ////  You can use the DbSet<T>.AddOrUpdate() helper extension method
            ////  to avoid creating duplicate seed data.
        }
    }
}
