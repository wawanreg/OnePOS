namespace OnePOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateShoppingModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BillingHeader", "ReduceInvoiceValue", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BillingHeader", "ReduceInvoiceValue");
        }
    }
}
