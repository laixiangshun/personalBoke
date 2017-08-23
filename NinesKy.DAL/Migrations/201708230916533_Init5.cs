namespace NinesKy.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init5 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        ArticleID = c.Int(nullable: false, identity: true),
                        ModelID = c.Int(nullable: false),
                        Author = c.String(maxLength: 50),
                        Source = c.String(maxLength: 50),
                        Intro = c.String(maxLength: 255),
                        Content = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ArticleID)
                .ForeignKey("dbo.CommonModels", t => t.ModelID, cascadeDelete: true)
                .Index(t => t.ModelID);
            
            CreateTable(
                "dbo.CommonModels",
                c => new
                    {
                        ModelID = c.Int(nullable: false, identity: true),
                        CategoryID = c.Int(nullable: false),
                        Model = c.String(),
                        Title = c.String(nullable: false, maxLength: 255),
                        Inputer = c.String(maxLength: 50),
                        Hits = c.Int(nullable: false),
                        ReleaseDate = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        DefaultPicUrl = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.ModelID)
                .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: true)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.Attachments",
                c => new
                    {
                        AttachmentID = c.Int(nullable: false, identity: true),
                        ModelID = c.Int(),
                        Owner = c.String(),
                        Type = c.String(),
                        Extension = c.String(),
                        FileParth = c.String(),
                        UploadDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.AttachmentID)
                .ForeignKey("dbo.CommonModels", t => t.ModelID)
                .Index(t => t.ModelID);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        ParentId = c.Int(nullable: false),
                        ParentPath = c.String(nullable: false),
                        Description = c.String(),
                        MetaKeywords = c.String(),
                        MetaDescription = c.String(),
                        Type = c.Int(nullable: false),
                        Model = c.String(maxLength: 50),
                        CategoryView = c.String(maxLength: 255),
                        ContentView = c.String(maxLength: 255),
                        LinkUrl = c.String(maxLength: 255),
                        Order = c.Int(nullable: false),
                        ContentOrder = c.Int(),
                        PageSize = c.Int(),
                        RecordUnit = c.String(maxLength: 255),
                        RecordName = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentID = c.Int(nullable: false, identity: true),
                        ModelID = c.Int(nullable: false),
                        Title = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.CommentID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Articles", "ModelID", "dbo.CommonModels");
            DropForeignKey("dbo.CommonModels", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.Attachments", "ModelID", "dbo.CommonModels");
            DropIndex("dbo.Attachments", new[] { "ModelID" });
            DropIndex("dbo.CommonModels", new[] { "CategoryID" });
            DropIndex("dbo.Articles", new[] { "ModelID" });
            DropTable("dbo.Comments");
            DropTable("dbo.Categories");
            DropTable("dbo.Attachments");
            DropTable("dbo.CommonModels");
            DropTable("dbo.Articles");
        }
    }
}
