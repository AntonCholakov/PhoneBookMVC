namespace PhoneBookMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GroupsNameRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ContactGroups", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ContactGroups", "Name", c => c.String());
        }
    }
}
