using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OnePOS.Models.Dashboard.Brand;
using OnePOS.Models.Dashboard.Storage;
using OnePOS.Models.Dashboard.Vendors;

namespace OnePOS.Models.Dashboard.Items
{
    public class ItemViewModels
    {
        [Key]
        public int ItemId { get; set; }
        public string ItemUniqueId { get; set; }
        public string ItemName { get; set; }
        public decimal SalePrice { get; set; }
        public decimal BuyPrice { get; set; }
        public decimal OldPrice { get; set; }
        public decimal Stock { get; set; }
        public string ItemPicture { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        public string CreatedBy { get; set; }
        [Column(TypeName = "DateTime2")]
        public DateTime CreatedDate { get; set; }
        public string CreatedAgent { get; set; }
        public string UpdatedBy { get; set; }
        [Column(TypeName = "DateTime2")]
        public DateTime UpdatedDate { get; set; }
        public string UpadtedAgent { get; set; }
        public virtual ManufacturerViewModels Manufacturer { get; set; }
        public virtual BrandViewModels Brand { get; set; }
        public virtual VendorViewModels Vendor { get; set; }
        public virtual BrandCategoryModels BrandCategory { get; set; }
        public virtual StorageViewModels Storage { get; set; }
    }
}