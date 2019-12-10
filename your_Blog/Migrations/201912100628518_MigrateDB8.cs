namespace your_Blog.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class MigrateDB8 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ArticleModels", "Name", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.ArticleModels", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.CategoryModels", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.TagModels", "Name", c => c.String(nullable: false, maxLength: 50));
        }

        public override void Down()
        {
            AlterColumn("dbo.TagModels", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.CategoryModels", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.ArticleModels", "Description", c => c.String());
            AlterColumn("dbo.ArticleModels", "Name", c => c.String(nullable: false, maxLength: 255));
        }
    }
}
