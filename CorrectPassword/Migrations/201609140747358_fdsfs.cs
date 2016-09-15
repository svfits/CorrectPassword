namespace CorrectPassword.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fdsfs : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "correctDateTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "correctDateTime", c => c.DateTime(nullable: false));
        }
    }
}
