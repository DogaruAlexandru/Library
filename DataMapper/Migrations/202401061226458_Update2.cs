namespace DataMapper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.BorrowedBooks", new[] { "Reader_Id" });
            DropIndex("dbo.BorrowedBooks", new[] { "Staff_Id" });
            RenameColumn(table: "dbo.BorrowedBooks", name: "Reader_Id", newName: "ReaderId");
            RenameColumn(table: "dbo.BorrowedBooks", name: "Staff_Id", newName: "StaffId");
            AlterColumn("dbo.BorrowedBooks", "ReaderId", c => c.Int(nullable: false));
            AlterColumn("dbo.BorrowedBooks", "StaffId", c => c.Int(nullable: false));
            CreateIndex("dbo.BorrowedBooks", "ReaderId");
            CreateIndex("dbo.BorrowedBooks", "StaffId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.BorrowedBooks", new[] { "StaffId" });
            DropIndex("dbo.BorrowedBooks", new[] { "ReaderId" });
            AlterColumn("dbo.BorrowedBooks", "StaffId", c => c.Int());
            AlterColumn("dbo.BorrowedBooks", "ReaderId", c => c.Int());
            RenameColumn(table: "dbo.BorrowedBooks", name: "StaffId", newName: "Staff_Id");
            RenameColumn(table: "dbo.BorrowedBooks", name: "ReaderId", newName: "Reader_Id");
            CreateIndex("dbo.BorrowedBooks", "Staff_Id");
            CreateIndex("dbo.BorrowedBooks", "Reader_Id");
        }
    }
}
