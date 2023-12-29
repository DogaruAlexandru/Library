namespace DataMapper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.People", "EmailAddress", c => c.String(maxLength: 100));
            AlterColumn("dbo.People", "PhoneNumber", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.People", "PhoneNumber", c => c.String());
            AlterColumn("dbo.People", "EmailAddress", c => c.String());
        }
    }
}
