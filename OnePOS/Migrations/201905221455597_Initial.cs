namespace OnePOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BillingDetail",
                c => new
                    {
                        NoBillingDetail = c.Int(nullable: false, identity: true),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DiscontPerItems = c.Int(nullable: false),
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
                        TotalBeforeTax = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalTransaction = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalItem = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalAfterTax = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PaymentTax = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalBeforeDiscount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalPayment = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DiscontTransaction = c.Int(nullable: false),
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
            
            CreateTable(
                "dbo.Vendor",
                c => new
                    {
                        VendorId = c.Int(nullable: false, identity: true),
                        VendorUniqueId = c.String(),
                        VendorName = c.String(nullable: false),
                        VendorAddress = c.String(nullable: false),
                        VendorPhone = c.String(nullable: false),
                        VendorEmail = c.String(),
                        VendorOwner = c.String(),
                        VendorCs = c.String(),
                        Active = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedAgent = c.String(),
                        UpdatedBy = c.String(),
                        UpdatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpadtedAgent = c.String(),
                    })
                .PrimaryKey(t => t.VendorId);
            
            CreateTable(
                "dbo.Item",
                c => new
                    {
                        ItemId = c.Int(nullable: false, identity: true),
                        ItemUniqueId = c.String(),
                        ItemName = c.String(),
                        SalePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BuyPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OldPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Stock = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ItemPicture = c.String(),
                        Active = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedAgent = c.String(),
                        UpdatedBy = c.String(),
                        UpdatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpadtedAgent = c.String(),
                        Brand_BrandId = c.Int(),
                        BrandCategory_BrandCategoryId = c.Int(),
                        Manufacturer_ManufacturerId = c.Int(),
                        Storage_StorageId = c.Int(),
                        Vendor_VendorId = c.Int(),
                    })
                .PrimaryKey(t => t.ItemId)
                .ForeignKey("dbo.Brand", t => t.Brand_BrandId)
                .ForeignKey("dbo.BrandCategory", t => t.BrandCategory_BrandCategoryId)
                .ForeignKey("dbo.Manufacturer", t => t.Manufacturer_ManufacturerId)
                .ForeignKey("dbo.Storage", t => t.Storage_StorageId)
                .ForeignKey("dbo.Vendor", t => t.Vendor_VendorId)
                .Index(t => t.Brand_BrandId)
                .Index(t => t.BrandCategory_BrandCategoryId)
                .Index(t => t.Manufacturer_ManufacturerId)
                .Index(t => t.Storage_StorageId)
                .Index(t => t.Vendor_VendorId);
            
            CreateTable(
                "dbo.Brand",
                c => new
                    {
                        BrandId = c.Int(nullable: false, identity: true),
                        BrandUniqueId = c.String(),
                        BrandName = c.String(nullable: false),
                        BrandDescription = c.String(),
                        Active = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedAgent = c.String(),
                        UpdatedBy = c.String(),
                        UpdatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpadtedAgent = c.String(),
                    })
                .PrimaryKey(t => t.BrandId);
            
            CreateTable(
                "dbo.BrandCategory",
                c => new
                    {
                        BrandCategoryId = c.Int(nullable: false, identity: true),
                        BrandCategoryName = c.String(),
                    })
                .PrimaryKey(t => t.BrandCategoryId);
            
            CreateTable(
                "dbo.Manufacturer",
                c => new
                    {
                        ManufacturerId = c.Int(nullable: false, identity: true),
                        ManufacturerUniqueId = c.String(),
                        ManufacturerName = c.String(),
                        ManufacturerDescription = c.String(),
                        Active = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedAgent = c.String(),
                        UpdatedBy = c.String(),
                        UpdatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpadtedAgent = c.String(),
                    })
                .PrimaryKey(t => t.ManufacturerId);
            
            CreateTable(
                "dbo.Storage",
                c => new
                    {
                        StorageId = c.Int(nullable: false, identity: true),
                        StorageUniqueId = c.String(),
                        StorageName = c.String(nullable: false),
                        StorageDescription = c.String(),
                        Active = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedAgent = c.String(),
                        UpdatedBy = c.String(),
                        UpdatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpadtedAgent = c.String(),
                    })
                .PrimaryKey(t => t.StorageId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Description = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FullName = c.String(),
                        PhoneNumber = c.String(),
                        LastLogin = c.DateTime(),
                        Active = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedAgent = c.String(),
                        UpdatedBy = c.String(),
                        UpdatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpadtedAgent = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.BillingDetail", "Item_ItemId", "dbo.Item");
            DropForeignKey("dbo.Item", "Vendor_VendorId", "dbo.Vendor");
            DropForeignKey("dbo.Item", "Storage_StorageId", "dbo.Storage");
            DropForeignKey("dbo.Item", "Manufacturer_ManufacturerId", "dbo.Manufacturer");
            DropForeignKey("dbo.Item", "BrandCategory_BrandCategoryId", "dbo.BrandCategory");
            DropForeignKey("dbo.Item", "Brand_BrandId", "dbo.Brand");
            DropForeignKey("dbo.BillingHeader", "Vendor_VendorId", "dbo.Vendor");
            DropForeignKey("dbo.BillingHeader", "BillingStatus_BillingStatusId", "dbo.BillingStatus");
            DropForeignKey("dbo.BillingDetail", "BillingHeader_NoBillingHeader", "dbo.BillingHeader");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Item", new[] { "Vendor_VendorId" });
            DropIndex("dbo.Item", new[] { "Storage_StorageId" });
            DropIndex("dbo.Item", new[] { "Manufacturer_ManufacturerId" });
            DropIndex("dbo.Item", new[] { "BrandCategory_BrandCategoryId" });
            DropIndex("dbo.Item", new[] { "Brand_BrandId" });
            DropIndex("dbo.BillingHeader", new[] { "Vendor_VendorId" });
            DropIndex("dbo.BillingHeader", new[] { "BillingStatus_BillingStatusId" });
            DropIndex("dbo.BillingDetail", new[] { "Item_ItemId" });
            DropIndex("dbo.BillingDetail", new[] { "BillingHeader_NoBillingHeader" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Storage");
            DropTable("dbo.Manufacturer");
            DropTable("dbo.BrandCategory");
            DropTable("dbo.Brand");
            DropTable("dbo.Item");
            DropTable("dbo.Vendor");
            DropTable("dbo.BillingStatus");
            DropTable("dbo.BillingHeader");
            DropTable("dbo.BillingDetail");
        }
    }
}
