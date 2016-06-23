namespace ImageWebAPIs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ClientInfo", "Phone", c => c.String(maxLength: 100));
            AlterColumn("dbo.ImageStore", "ImagePath", c => c.String(maxLength: 256));
            AlterColumn("dbo.Token", "AuthToken", c => c.String(nullable: false, maxLength: 256));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Token", "AuthToken", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.ImageStore", "ImagePath", c => c.String(maxLength: 255));
            AlterColumn("dbo.ClientInfo", "Phone", c => c.String(maxLength: 20));
        }
    }
}
