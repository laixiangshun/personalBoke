namespace NinesKy.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "RegistrationTime", c => c.DateTime());
            AlterColumn("dbo.Users", "LoginTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "LoginTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Users", "RegistrationTime", c => c.DateTime(nullable: false));
        }
    }
}
