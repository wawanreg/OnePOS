using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnePOS.Models.Dashboard
{
    public class ItemViewModels
    {
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public decimal SalePrice { get; set; }
        public decimal NewPrice { get; set; }
        public decimal OldPrice { get; set; }
        public string ItemLocation { get; set; }
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
    }
}