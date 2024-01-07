// --------------------------------------------------------------------------------------------------------------------
// <copyright file="202401072155388_Update4.cs" company="Transilvania University of Brasov">
//   Copyright (c) Dogaru Alexandru.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace DataMapper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    /// <summary>
    /// Represents a database migration that updates the BorrowedBooks table.
    /// </summary>
    public partial class Update4 : DbMigration
    {
        /// <summary>
        /// Modifies the database schema to update the BorrowedBooks table.
        /// </summary>
        public override void Up()
        {
            this.AlterColumn("dbo.BorrowedBooks", "ReturnedDate", c => c.DateTime());
        }

        /// <summary>
        /// Reverts changes to the database schema.
        /// </summary>
        public override void Down()
        {
            this.AlterColumn("dbo.BorrowedBooks", "ReturnedDate", c => c.DateTime(nullable: false));
        }
    }
}
