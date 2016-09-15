namespace CorrectPassword.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ggfd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "status");
        }
    }
}
