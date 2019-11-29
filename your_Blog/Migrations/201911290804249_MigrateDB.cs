namespace your_Blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ArticleModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ShortDescription = c.String(),
                        Date = c.DateTime(nullable: false),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CategoryModels", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.CategoryModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TagModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TagArticle",
                c => new
                    {
                        TagId = c.Int(nullable: false),
                        ArticleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TagId, t.ArticleId })
                .ForeignKey("dbo.TagModels", t => t.TagId, cascadeDelete: true)
                .ForeignKey("dbo.ArticleModels", t => t.ArticleId, cascadeDelete: true)
                .Index(t => t.TagId)
                .Index(t => t.ArticleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TagArticle", "ArticleId", "dbo.ArticleModels");
            DropForeignKey("dbo.TagArticle", "TagId", "dbo.TagModels");
            DropForeignKey("dbo.ArticleModels", "CategoryId", "dbo.CategoryModels");
            DropIndex("dbo.TagArticle", new[] { "ArticleId" });
            DropIndex("dbo.TagArticle", new[] { "TagId" });
            DropIndex("dbo.ArticleModels", new[] { "CategoryId" });
            DropTable("dbo.TagArticle");
            DropTable("dbo.TagModels");
            DropTable("dbo.CategoryModels");
            DropTable("dbo.ArticleModels");
        }
    }
}
