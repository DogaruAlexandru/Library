namespace DataMapper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Editions", "Book_Id", "dbo.Books");
            DropIndex("dbo.Editions", new[] { "Book_Id" });
            AlterColumn("dbo.Editions", "Book_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Editions", "Book_Id");
            AddForeignKey("dbo.Editions", "Book_Id", "dbo.Books", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Editions", "Book_Id", "dbo.Books");
            DropIndex("dbo.Editions", new[] { "Book_Id" });
            AlterColumn("dbo.Editions", "Book_Id", c => c.Int());
            CreateIndex("dbo.Editions", "Book_Id");
            AddForeignKey("dbo.Editions", "Book_Id", "dbo.Books", "Id");
        }
    }
}
