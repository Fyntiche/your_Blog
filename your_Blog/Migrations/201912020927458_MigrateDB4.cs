namespace your_Blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ArticleModels", "HeroImage", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ArticleModels", "HeroImage");
        }
    }
}
