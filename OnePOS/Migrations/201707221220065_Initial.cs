namespace OnePOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            
            CreateTable(
                "dbo.Brand",
                c => new
                    {
                        BrandId = c.Int(nullable: false, identity: true),
                        BrandUniqueId = c.String(),
                        BrandName = c.String(),
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
                        ItemLocation = c.String(),
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
                        Manufacturer_ManufacturerId = c.Int(),
                        Vendor_VendorId = c.Int(),
                    })
                .PrimaryKey(t => t.ItemId)
                .ForeignKey("dbo.Brand", t => t.Brand_BrandId)
                .ForeignKey("dbo.Manufacturer", t => t.Manufacturer_ManufacturerId)
                .ForeignKey("dbo.Vendor", t => t.Vendor_VendorId)
                .Index(t => t.Brand_BrandId)
                .Index(t => t.Manufacturer_ManufacturerId)
                .Index(t => t.Vendor_VendorId);
            
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
                "dbo.Vendor",
                c => new
                    {
                        VendorId = c.Int(nullable: false, identity: true),
                        VendorUniqueId = c.String(),
                        VendorName = c.String(),
                        VendorAddress = c.String(),
                        VendorPhone = c.String(),
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
            DropForeignKey("dbo.Item", "Vendor_VendorId", "dbo.Vendor");
            DropForeignKey("dbo.Item", "Manufacturer_ManufacturerId", "dbo.Manufacturer");
            DropForeignKey("dbo.Item", "Brand_BrandId", "dbo.Brand");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Item", new[] { "Vendor_VendorId" });
            DropIndex("dbo.Item", new[] { "Manufacturer_ManufacturerId" });
            DropIndex("dbo.Item", new[] { "Brand_BrandId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Vendor");
            DropTable("dbo.Manufacturer");
            DropTable("dbo.Item");
            DropTable("dbo.Brand");
        }
    }
}
