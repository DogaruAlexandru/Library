// --------------------------------------------------------------------------------------------------------------------
// <copyright file="202401052047280_Update.cs" company="Transilvania University of Brasov">
//   Copyright (c) Dogaru Alexandru.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace DataMapper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    /// <summary>
    /// Represents a database migration that updates the Editions table.
    /// </summary>
    public partial class Update : DbMigration
    {
        /// <summary>
        /// Modifies the database schema to update the Editions table.
        /// </summary>
        public override void Up()
        {
            this.DropForeignKey("dbo.Editions", "Book_Id", "dbo.Books");
            this.DropIndex("dbo.Editions", new[] { "Book_Id" });
            this.AlterColumn("dbo.Editions", "Book_Id", c => c.Int(nullable: false));
            this.CreateIndex("dbo.Editions", "Book_Id");
            this.AddForeignKey("dbo.Editions", "Book_Id", "dbo.Books", "Id", cascadeDelete: true);
        }

        /// <summary>
        /// Reverts changes to the database schema.
        /// </summary>
        public override void Down()
        {
            this.DropForeignKey("dbo.Editions", "Book_Id", "dbo.Books");
            this.DropIndex("dbo.Editions", new[] { "Book_Id" });
            this.AlterColumn("dbo.Editions", "Book_Id", c => c.Int());
            this.CreateIndex("dbo.Editions", "Book_Id");
            this.AddForeignKey("dbo.Editions", "Book_Id", "dbo.Books", "Id");
        }
    }
}
