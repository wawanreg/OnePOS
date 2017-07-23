namespace OnePOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BranchCategory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BrandCategory",
                c => new
                    {
                        BrandCategoryId = c.Int(nullable: false, identity: true),
                        BrandCategoryName = c.String(),
                    })
                .PrimaryKey(t => t.BrandCategoryId);
        }
        
        public override void Down()
        {
            DropTable("dbo.BrandCategory");
        }
    }
}
