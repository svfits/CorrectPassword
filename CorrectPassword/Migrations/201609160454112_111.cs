namespace CorrectPassword.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _111 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserPasswordsDefaults",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        passwordСomplexity = c.Int(nullable: false),
                        passwordLength = c.Int(nullable: false),
                        passwordLifeTime = c.Int(nullable: false),
                        defaultLoginUser = c.String(),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        loginUser = c.String(),
                        description = c.String(),
                        password = c.String(),
                        passwordСomplexity = c.Int(nullable: false),
                        passwordLength = c.Int(nullable: false),
                        passwordLifeTime = c.Int(nullable: false),
                        namePc = c.String(),
                        ipPC = c.String(),
                        stampDateTimeLoadPc = c.DateTime(nullable: false),
                        status = c.Boolean(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.UserPasswordsDefaults");
        }
    }
}
