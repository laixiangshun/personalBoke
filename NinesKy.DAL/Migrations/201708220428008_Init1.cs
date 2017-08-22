namespace NinesKy.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init1 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.UserRoleRelations", "UserID");
            CreateIndex("dbo.UserRoleRelations", "RoleID");
            AddForeignKey("dbo.UserRoleRelations", "RoleID", "dbo.Roles", "RoleID", cascadeDelete: true);
            AddForeignKey("dbo.UserRoleRelations", "UserID", "dbo.Users", "UserID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoleRelations", "UserID", "dbo.Users");
            DropForeignKey("dbo.UserRoleRelations", "RoleID", "dbo.Roles");
            DropIndex("dbo.UserRoleRelations", new[] { "RoleID" });
            DropIndex("dbo.UserRoleRelations", new[] { "UserID" });
        }
    }
}
