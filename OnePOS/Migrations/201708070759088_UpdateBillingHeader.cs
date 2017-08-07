namespace OnePOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateBillingHeader : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BillingHeader", "TotalItem", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BillingHeader", "TotalItem");
        }
    }
}
