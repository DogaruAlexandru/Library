namespace DataMapper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update3 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.BookDomains", new[] { "ParentDomain_Id" });
            AlterColumn("dbo.BookDomains", "ParentDomain_Id", c => c.Int());
            AlterColumn("dbo.People", "CNP", c => c.String());
            CreateIndex("dbo.BookDomains", "ParentDomain_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.BookDomains", new[] { "ParentDomain_Id" });
            AlterColumn("dbo.People", "CNP", c => c.String(maxLength: 13));
            AlterColumn("dbo.BookDomains", "ParentDomain_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.BookDomains", "ParentDomain_Id");
        }
    }
}
