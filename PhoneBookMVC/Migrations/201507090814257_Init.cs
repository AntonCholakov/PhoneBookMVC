namespace PhoneBookMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContactGroups",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        PhotoFilePath = c.String(),
                        BirthDay = c.DateTime(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Notes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        DateLastEdit = c.DateTime(nullable: false),
                        ContactId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Contacts", t => t.ContactId, cascadeDelete: true)
                .Index(t => t.ContactId);
            
            CreateTable(
                "dbo.Phones",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Number = c.String(),
                        Type = c.Int(nullable: false),
                        ContactId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Contacts", t => t.ContactId, cascadeDelete: true)
                .Index(t => t.ContactId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        Hash = c.String(),
                        LastName = c.String(),
                        Role = c.Int(nullable: false),
                        Salt = c.String(),
                        Username = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Queries",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Content = c.String(),
                        Email = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ContactJoinContactGroup",
                c => new
                    {
                        ContactId = c.Int(nullable: false),
                        ContactGroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ContactId, t.ContactGroupId })
                .ForeignKey("dbo.Contacts", t => t.ContactId)
                .ForeignKey("dbo.ContactGroups", t => t.ContactGroupId)
                .Index(t => t.ContactId)
                .Index(t => t.ContactGroupId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contacts", "UserId", "dbo.Users");
            DropForeignKey("dbo.ContactGroups", "UserId", "dbo.Users");
            DropForeignKey("dbo.Phones", "ContactId", "dbo.Contacts");
            DropForeignKey("dbo.Notes", "ContactId", "dbo.Contacts");
            DropForeignKey("dbo.ContactJoinContactGroup", "ContactGroupId", "dbo.ContactGroups");
            DropForeignKey("dbo.ContactJoinContactGroup", "ContactId", "dbo.Contacts");
            DropIndex("dbo.ContactJoinContactGroup", new[] { "ContactGroupId" });
            DropIndex("dbo.ContactJoinContactGroup", new[] { "ContactId" });
            DropIndex("dbo.Phones", new[] { "ContactId" });
            DropIndex("dbo.Notes", new[] { "ContactId" });
            DropIndex("dbo.Contacts", new[] { "UserId" });
            DropIndex("dbo.ContactGroups", new[] { "UserId" });
            DropTable("dbo.ContactJoinContactGroup");
            DropTable("dbo.Queries");
            DropTable("dbo.Users");
            DropTable("dbo.Phones");
            DropTable("dbo.Notes");
            DropTable("dbo.Contacts");
            DropTable("dbo.ContactGroups");
        }
    }
}
