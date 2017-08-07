namespace OnePOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InvoiceDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BillingDetail",
                c => new
                    {
                        NoBillingDetail = c.Int(nullable: false, identity: true),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Active = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedAgent = c.String(),
                        UpdatedBy = c.String(),
                        UpdatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedAgent = c.String(),
                        BillingHeader_NoBillingHeader = c.Int(),
                        Item_ItemId = c.Int(),
                    })
                .PrimaryKey(t => t.NoBillingDetail)
                .ForeignKey("dbo.BillingHeader", t => t.BillingHeader_NoBillingHeader)
                .ForeignKey("dbo.Item", t => t.Item_ItemId)
                .Index(t => t.BillingHeader_NoBillingHeader)
                .Index(t => t.Item_ItemId);
            
            CreateTable(
                "dbo.BillingHeader",
                c => new
                    {
                        NoBillingHeader = c.Int(nullable: false, identity: true),
                        NoInvoice = c.String(),
                        NoMos = c.String(),
                        DueDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        StartTransactionDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        EndTransactionDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        InvoiceDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        TotalPaymentBeforeTax = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalTransaction = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalPaymentAfterTax = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PaymentTax = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Tax = c.Int(nullable: false),
                        MerchantEmail = c.String(),
                        Active = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedAgent = c.String(),
                        UpdatedBy = c.String(),
                        UpdatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedAgent = c.String(),
                        BillingStatus_BillingStatusId = c.Int(),
                        Vendor_VendorId = c.Int(),
                    })
                .PrimaryKey(t => t.NoBillingHeader)
                .ForeignKey("dbo.BillingStatus", t => t.BillingStatus_BillingStatusId)
                .ForeignKey("dbo.Vendor", t => t.Vendor_VendorId)
                .Index(t => t.BillingStatus_BillingStatusId)
                .Index(t => t.Vendor_VendorId);
            
            CreateTable(
                "dbo.BillingStatus",
                c => new
                    {
                        BillingStatusId = c.Int(nullable: false, identity: true),
                        BillingName = c.String(),
                    })
                .PrimaryKey(t => t.BillingStatusId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BillingDetail", "Item_ItemId", "dbo.Item");
            DropForeignKey("dbo.BillingHeader", "Vendor_VendorId", "dbo.Vendor");
            DropForeignKey("dbo.BillingHeader", "BillingStatus_BillingStatusId", "dbo.BillingStatus");
            DropForeignKey("dbo.BillingDetail", "BillingHeader_NoBillingHeader", "dbo.BillingHeader");
            DropIndex("dbo.BillingHeader", new[] { "Vendor_VendorId" });
            DropIndex("dbo.BillingHeader", new[] { "BillingStatus_BillingStatusId" });
            DropIndex("dbo.BillingDetail", new[] { "Item_ItemId" });
            DropIndex("dbo.BillingDetail", new[] { "BillingHeader_NoBillingHeader" });
            DropTable("dbo.BillingStatus");
            DropTable("dbo.BillingHeader");
            DropTable("dbo.BillingDetail");
        }
    }
}
