namespace ImageWebAPIs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FourDB : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Token", "ExpiresOn", c => c.DateTime());
            AlterColumn("dbo.Token", "IssuedOn", c => c.DateTime());
            DropColumn("dbo.Token", "ExpriesOn");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Token", "ExpriesOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Token", "IssuedOn", c => c.DateTime(nullable: false));
            DropColumn("dbo.Token", "ExpiresOn");
        }
    }
}
