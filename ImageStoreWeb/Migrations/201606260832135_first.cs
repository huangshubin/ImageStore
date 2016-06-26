namespace ImageStoreWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClientInfo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 100),
                        Password = c.String(nullable: false, maxLength: 256),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        LastName = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false, maxLength: 100),
                        Street = c.String(maxLength: 200),
                        City = c.String(maxLength: 100),
                        State = c.String(maxLength: 60),
                        Zip = c.String(maxLength: 10),
                        Country = c.String(maxLength: 100),
                        Phone = c.String(maxLength: 100),
                        Active = c.Boolean(nullable: false),
                        DateRegistered = c.DateTime(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Image",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImageContent = c.Binary(),
                        ImagePath = c.String(maxLength: 256),
                        ImageType = c.String(nullable: false, maxLength: 10),
                        Active = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClientInfo", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Token",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AuthToken = c.String(nullable: false, maxLength: 1000),
                        UserId = c.Int(nullable: false),
                        IssuedOn = c.DateTime(),
                        ExpiresOn = c.DateTime(),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClientInfo", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Token", "UserId", "dbo.ClientInfo");
            DropForeignKey("dbo.Image", "UserId", "dbo.ClientInfo");
            DropIndex("dbo.Token", new[] { "UserId" });
            DropIndex("dbo.Image", new[] { "UserId" });
            DropTable("dbo.Token");
            DropTable("dbo.Image");
            DropTable("dbo.ClientInfo");
        }
    }
}
