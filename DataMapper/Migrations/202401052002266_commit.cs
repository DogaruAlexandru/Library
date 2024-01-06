namespace DataMapper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class commit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        Description = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BookDomains",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        ParentDomain_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BookDomains", t => t.ParentDomain_Id)
                .Index(t => t.ParentDomain_Id);
            
            CreateTable(
                "dbo.Editions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        Publisher = c.String(nullable: false, maxLength: 100),
                        Book_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.Book_Id)
                .Index(t => t.Book_Id);
            
            CreateTable(
                "dbo.BorrowedBooks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BorrowDate = c.DateTime(nullable: false),
                        DueDate = c.DateTime(nullable: false),
                        ReturnedDate = c.DateTime(nullable: false),
                        Edition_Id = c.Int(nullable: false),
                        Reader_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Editions", t => t.Edition_Id, cascadeDelete: true)
                .ForeignKey("dbo.People", t => t.Reader_Id, cascadeDelete: true)
                .Index(t => t.Edition_Id)
                .Index(t => t.Reader_Id);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CNP = c.String(),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        LastName = c.String(nullable: false, maxLength: 100),
                        EmailAddress = c.String(maxLength: 100),
                        PhoneNumber = c.String(maxLength: 20),
                        Address = c.String(nullable: false, maxLength: 200),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BookAuthors",
                c => new
                    {
                        Book_Id = c.Int(nullable: false),
                        Author_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Book_Id, t.Author_Id })
                .ForeignKey("dbo.Books", t => t.Book_Id, cascadeDelete: true)
                .ForeignKey("dbo.Authors", t => t.Author_Id, cascadeDelete: true)
                .Index(t => t.Book_Id)
                .Index(t => t.Author_Id);
            
            CreateTable(
                "dbo.BookDomainBooks",
                c => new
                    {
                        BookDomain_Id = c.Int(nullable: false),
                        Book_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BookDomain_Id, t.Book_Id })
                .ForeignKey("dbo.BookDomains", t => t.BookDomain_Id, cascadeDelete: true)
                .ForeignKey("dbo.Books", t => t.Book_Id, cascadeDelete: true)
                .Index(t => t.BookDomain_Id)
                .Index(t => t.Book_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BorrowedBooks", "Reader_Id", "dbo.People");
            DropForeignKey("dbo.BorrowedBooks", "Edition_Id", "dbo.Editions");
            DropForeignKey("dbo.Editions", "Book_Id", "dbo.Books");
            DropForeignKey("dbo.BookDomains", "ParentDomain_Id", "dbo.BookDomains");
            DropForeignKey("dbo.BookDomainBooks", "Book_Id", "dbo.Books");
            DropForeignKey("dbo.BookDomainBooks", "BookDomain_Id", "dbo.BookDomains");
            DropForeignKey("dbo.BookAuthors", "Author_Id", "dbo.Authors");
            DropForeignKey("dbo.BookAuthors", "Book_Id", "dbo.Books");
            DropIndex("dbo.BookDomainBooks", new[] { "Book_Id" });
            DropIndex("dbo.BookDomainBooks", new[] { "BookDomain_Id" });
            DropIndex("dbo.BookAuthors", new[] { "Author_Id" });
            DropIndex("dbo.BookAuthors", new[] { "Book_Id" });
            DropIndex("dbo.BorrowedBooks", new[] { "Reader_Id" });
            DropIndex("dbo.BorrowedBooks", new[] { "Edition_Id" });
            DropIndex("dbo.Editions", new[] { "Book_Id" });
            DropIndex("dbo.BookDomains", new[] { "ParentDomain_Id" });
            DropTable("dbo.BookDomainBooks");
            DropTable("dbo.BookAuthors");
            DropTable("dbo.People");
            DropTable("dbo.BorrowedBooks");
            DropTable("dbo.Editions");
            DropTable("dbo.BookDomains");
            DropTable("dbo.Books");
            DropTable("dbo.Authors");
        }
    }
}
