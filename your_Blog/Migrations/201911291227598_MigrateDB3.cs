namespace your_Blog.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class MigrateDB3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ArticleModels", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.CategoryModels", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.TagModels", "Name", c => c.String(nullable: false));
        }

        public override void Down()
        {
            AlterColumn("dbo.TagModels", "Name", c => c.String());
            AlterColumn("dbo.CategoryModels", "Name", c => c.String());
            AlterColumn("dbo.ArticleModels", "Name", c => c.String());
        }
    }
}
