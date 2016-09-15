namespace CorrectPassword.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fgd : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "status", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "status", c => c.Boolean(nullable: false));
        }
    }
}
