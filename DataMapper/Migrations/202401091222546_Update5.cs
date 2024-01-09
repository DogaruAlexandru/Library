// --------------------------------------------------------------------------------------------------------------------
// <copyright file="202401091222546_Update5.cs" company="Transilvania University of Brasov">
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
    public partial class Update5 : DbMigration
    {
        /// <summary>
        /// Modifies the database schema to update the Editions table.
        /// </summary>
        public override void Up()
        {
            this.AddColumn("dbo.Editions", "CanNotBorrow", c => c.Int(nullable: false));
            this.AddColumn("dbo.Editions", "CanBorrow", c => c.Int(nullable: false));
        }

        /// <summary>
        /// Reverts changes to the database schema.
        /// </summary>
        public override void Down()
        {
            this.DropColumn("dbo.Editions", "CanBorrow");
            this.DropColumn("dbo.Editions", "CanNotBorrow");
        }
    }
}
