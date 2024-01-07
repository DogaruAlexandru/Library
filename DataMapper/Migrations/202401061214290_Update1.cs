// --------------------------------------------------------------------------------------------------------------------
// <copyright file="202401061214290_Update1.cs" company="Transilvania University of Brasov">
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
    public partial class Update1 : DbMigration
    {
        /// <summary>
        /// Represents a database migration that updates the BorrowedBooks table.
        /// </summary>
        public override void Up()
        {
            this.DropForeignKey("dbo.BorrowedBooks", "Reader_Id", "dbo.People");
            this.DropIndex("dbo.BorrowedBooks", new[] { "Reader_Id" });
            this.AddColumn("dbo.BorrowedBooks", "Staff_Id", c => c.Int());
            this.AlterColumn("dbo.BorrowedBooks", "Reader_Id", c => c.Int());
            this.CreateIndex("dbo.BorrowedBooks", "Reader_Id");
            this.CreateIndex("dbo.BorrowedBooks", "Staff_Id");
            this.AddForeignKey("dbo.BorrowedBooks", "Staff_Id", "dbo.People", "Id");
            this.AddForeignKey("dbo.BorrowedBooks", "Reader_Id", "dbo.People", "Id");
        }

        /// <summary>
        /// Reverts changes to the database schema.
        /// </summary>
        public override void Down()
        {
            this.DropForeignKey("dbo.BorrowedBooks", "Reader_Id", "dbo.People");
            this.DropForeignKey("dbo.BorrowedBooks", "Staff_Id", "dbo.People");
            this.DropIndex("dbo.BorrowedBooks", new[] { "Staff_Id" });
            this.DropIndex("dbo.BorrowedBooks", new[] { "Reader_Id" });
            this.AlterColumn("dbo.BorrowedBooks", "Reader_Id", c => c.Int(nullable: false));
            this.DropColumn("dbo.BorrowedBooks", "Staff_Id");
            this.CreateIndex("dbo.BorrowedBooks", "Reader_Id");
            this.AddForeignKey("dbo.BorrowedBooks", "Reader_Id", "dbo.People", "Id", cascadeDelete: true);
        }
    }
}
