namespace DataMapper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BorrowedBooks", "Reader_Id", "dbo.People");
            DropIndex("dbo.BorrowedBooks", new[] { "Reader_Id" });
            AddColumn("dbo.BorrowedBooks", "Staff_Id", c => c.Int());
            AlterColumn("dbo.BorrowedBooks", "Reader_Id", c => c.Int());
            CreateIndex("dbo.BorrowedBooks", "Reader_Id");
            CreateIndex("dbo.BorrowedBooks", "Staff_Id");
            AddForeignKey("dbo.BorrowedBooks", "Staff_Id", "dbo.People", "Id");
            AddForeignKey("dbo.BorrowedBooks", "Reader_Id", "dbo.People", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BorrowedBooks", "Reader_Id", "dbo.People");
            DropForeignKey("dbo.BorrowedBooks", "Staff_Id", "dbo.People");
            DropIndex("dbo.BorrowedBooks", new[] { "Staff_Id" });
            DropIndex("dbo.BorrowedBooks", new[] { "Reader_Id" });
            AlterColumn("dbo.BorrowedBooks", "Reader_Id", c => c.Int(nullable: false));
            DropColumn("dbo.BorrowedBooks", "Staff_Id");
            CreateIndex("dbo.BorrowedBooks", "Reader_Id");
            AddForeignKey("dbo.BorrowedBooks", "Reader_Id", "dbo.People", "Id", cascadeDelete: true);
        }
    }
}
