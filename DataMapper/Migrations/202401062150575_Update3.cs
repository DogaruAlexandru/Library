// --------------------------------------------------------------------------------------------------------------------
// <copyright file="202401062150575_Update3.cs" company="Transilvania University of Brasov">
//   Copyright (c) Dogaru Alexandru.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace DataMapper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    /// <summary>
    /// Represents a database migration that updates the People table.
    /// </summary>
    public partial class Update3 : DbMigration
    {
        /// <summary>
        /// Modifies the database schema to update the People table.
        /// </summary>
        public override void Up()
        {
            this.AlterColumn("dbo.People", "CNP", c => c.String(nullable: false));
        }

        /// <summary>
        /// Reverts changes to the database schema.
        /// </summary>
        public override void Down()
        {
            this.AlterColumn("dbo.People", "CNP", c => c.String());
        }
    }
}
