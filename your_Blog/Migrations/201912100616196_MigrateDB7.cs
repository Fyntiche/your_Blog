namespace your_Blog.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class MigrateDB7 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ArticleModels", "Name", c => c.String(nullable: false, maxLength: 255));
        }

        public override void Down()
        {
            AlterColumn("dbo.ArticleModels", "Name", c => c.String(nullable: false));
        }
    }
}
