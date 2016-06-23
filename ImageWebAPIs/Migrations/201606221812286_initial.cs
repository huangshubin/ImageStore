namespace ImageWebAPIs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClientInfo",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 100),
                        Password = c.String(nullable: false, maxLength: 100),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        LastName = c.String(nullable: false, maxLength: 100),
                        Street = c.String(maxLength: 200),
                        City = c.String(maxLength: 100),
                        State = c.String(maxLength: 60),
                        Zip = c.String(maxLength: 10),
                        Country = c.String(maxLength: 100),
                        Phone = c.String(maxLength: 20),
                        DateRegistered = c.DateTime(nullable: false),
                        Active = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ImageStore",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ImageContent = c.Binary(),
                        ImagePath = c.String(maxLength: 255),
                        Active = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ClientInfo", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Token",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AuthToken = c.String(nullable: false, maxLength: 255),
                        IssuedOn = c.DateTime(nullable: false),
                        ExpriesOn = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ClientInfo", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Token", "UserId", "dbo.ClientInfo");
            DropForeignKey("dbo.ImageStore", "UserId", "dbo.ClientInfo");
            DropIndex("dbo.Token", new[] { "UserId" });
            DropIndex("dbo.ImageStore", new[] { "UserId" });
            DropTable("dbo.Token");
            DropTable("dbo.ImageStore");
            DropTable("dbo.ClientInfo");
        }
    }
}
