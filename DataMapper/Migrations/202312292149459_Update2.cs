namespace DataMapper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update2 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Borroweds", newName: "BorrowedBooks");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.BorrowedBooks", newName: "Borroweds");
        }
    }
}
