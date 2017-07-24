namespace OnePOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeFewDb : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Brand", "BrandCategory_BrandCategoryId", c => c.Int());
            CreateIndex("dbo.Brand", "BrandCategory_BrandCategoryId");
            AddForeignKey("dbo.Brand", "BrandCategory_BrandCategoryId", "dbo.BrandCategory", "BrandCategoryId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Brand", "BrandCategory_BrandCategoryId", "dbo.BrandCategory");
            DropIndex("dbo.Brand", new[] { "BrandCategory_BrandCategoryId" });
            DropColumn("dbo.Brand", "BrandCategory_BrandCategoryId");
        }
    }
}
