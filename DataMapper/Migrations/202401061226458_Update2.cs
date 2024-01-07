// --------------------------------------------------------------------------------------------------------------------
// <copyright file="202401061226458_Update2.cs" company="Transilvania University of Brasov">
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
    public partial class Update2 : DbMigration
    {
        /// <summary>
        /// Modifies the database schema to update the BorrowedBooks table.
        /// </summary>
        public override void Up()
        {
            this.DropIndex("dbo.BorrowedBooks", new[] { "Reader_Id" });
            this.DropIndex("dbo.BorrowedBooks", new[] { "Staff_Id" });
            this.RenameColumn(table: "dbo.BorrowedBooks", name: "Reader_Id", newName: "ReaderId");
            this.RenameColumn(table: "dbo.BorrowedBooks", name: "Staff_Id", newName: "StaffId");
            this.AlterColumn("dbo.BorrowedBooks", "ReaderId", c => c.Int(nullable: false));
            this.AlterColumn("dbo.BorrowedBooks", "StaffId", c => c.Int(nullable: false));
            this.CreateIndex("dbo.BorrowedBooks", "ReaderId");
            this.CreateIndex("dbo.BorrowedBooks", "StaffId");
        }

        /// <summary>
        /// Reverts changes to the database schema.
        /// </summary>
        public override void Down()
        {
            this.DropIndex("dbo.BorrowedBooks", new[] { "StaffId" });
            this.DropIndex("dbo.BorrowedBooks", new[] { "ReaderId" });
            this.AlterColumn("dbo.BorrowedBooks", "StaffId", c => c.Int());
            this.AlterColumn("dbo.BorrowedBooks", "ReaderId", c => c.Int());
            this.RenameColumn(table: "dbo.BorrowedBooks", name: "StaffId", newName: "Staff_Id");
            this.RenameColumn(table: "dbo.BorrowedBooks", name: "ReaderId", newName: "Reader_Id");
            this.CreateIndex("dbo.BorrowedBooks", "Staff_Id");
            this.CreateIndex("dbo.BorrowedBooks", "Reader_Id");
        }
    }
}
