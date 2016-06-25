namespace ImageWebAPIs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FiveDB : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Token", "AuthToken", c => c.String(nullable: false, maxLength: 1000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Token", "AuthToken", c => c.String(nullable: false, maxLength: 256));
        }
    }
}
