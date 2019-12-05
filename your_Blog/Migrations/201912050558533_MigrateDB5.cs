namespace your_Blog.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class MigrateDB5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ArticleModels", "Description", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.ArticleModels", "Description");
        }
    }
}
