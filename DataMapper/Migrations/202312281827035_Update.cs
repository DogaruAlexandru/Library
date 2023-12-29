namespace DataMapper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Borroweds", "Edition_Id", "dbo.Editions");
            DropForeignKey("dbo.Borroweds", "Reader_Id", "dbo.People");
            DropIndex("dbo.BookDomains", new[] { "ParentDomain_Id" });
            DropIndex("dbo.Borroweds", new[] { "Edition_Id" });
            DropIndex("dbo.Borroweds", new[] { "Reader_Id" });
            AddColumn("dbo.People", "CNP", c => c.String(maxLength: 13));
            AlterColumn("dbo.Authors", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Books", "Title", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Books", "Description", c => c.String(maxLength: 100));
            AlterColumn("dbo.BookDomains", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.BookDomains", "ParentDomain_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Editions", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Editions", "Publisher", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Borroweds", "Edition_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Borroweds", "Reader_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.People", "FirstName", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.People", "LastName", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.People", "Address", c => c.String(nullable: false, maxLength: 200));
            CreateIndex("dbo.BookDomains", "ParentDomain_Id");
            CreateIndex("dbo.Borroweds", "Edition_Id");
            CreateIndex("dbo.Borroweds", "Reader_Id");
            AddForeignKey("dbo.Borroweds", "Edition_Id", "dbo.Editions", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Borroweds", "Reader_Id", "dbo.People", "Id", cascadeDelete: true);
            DropColumn("dbo.People", "SocialId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.People", "SocialId", c => c.String());
            DropForeignKey("dbo.Borroweds", "Reader_Id", "dbo.People");
            DropForeignKey("dbo.Borroweds", "Edition_Id", "dbo.Editions");
            DropIndex("dbo.Borroweds", new[] { "Reader_Id" });
            DropIndex("dbo.Borroweds", new[] { "Edition_Id" });
            DropIndex("dbo.BookDomains", new[] { "ParentDomain_Id" });
            AlterColumn("dbo.People", "Address", c => c.String());
            AlterColumn("dbo.People", "LastName", c => c.String());
            AlterColumn("dbo.People", "FirstName", c => c.String());
            AlterColumn("dbo.Borroweds", "Reader_Id", c => c.Int());
            AlterColumn("dbo.Borroweds", "Edition_Id", c => c.Int());
            AlterColumn("dbo.Editions", "Publisher", c => c.String());
            AlterColumn("dbo.Editions", "Name", c => c.String());
            AlterColumn("dbo.BookDomains", "ParentDomain_Id", c => c.Int());
            AlterColumn("dbo.BookDomains", "Name", c => c.String());
            AlterColumn("dbo.Books", "Description", c => c.String());
            AlterColumn("dbo.Books", "Title", c => c.String());
            AlterColumn("dbo.Authors", "Name", c => c.String());
            DropColumn("dbo.People", "CNP");
            CreateIndex("dbo.Borroweds", "Reader_Id");
            CreateIndex("dbo.Borroweds", "Edition_Id");
            CreateIndex("dbo.BookDomains", "ParentDomain_Id");
            AddForeignKey("dbo.Borroweds", "Reader_Id", "dbo.People", "Id");
            AddForeignKey("dbo.Borroweds", "Edition_Id", "dbo.Editions", "Id");
        }
    }
}
