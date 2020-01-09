using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnePOS.Models.Dashboard.Items
{
    public class ActionItemViewModels
    {
        [Required(ErrorMessage = "Fill This Field")]
        public string ItemName { get; set; }
        [Required(ErrorMessage = "Fill This Field")]
        public string ItemSalePrice { get; set; }
        [Required(ErrorMessage = "Fill This Field")]
        public string ItemBuyPrice   { get; set; }
        [Required(ErrorMessage = "Fill This Field")]
        public string ItemStock { get; set; }
        public string ItemStorage { get; set; }
        public string ItemBrandType { get; set; }
        public string ItemVendor { get; set; }
        public string ItemBrandCategory { get; set; }
        public IEnumerable<SelectListItem> VendorDropdownLists { get; set; }
        public IEnumerable<SelectListItem> BrandDropdownLists { get; set; }
        public IEnumerable<SelectListItem> BrandCategoryDropdownLists { get; set; }
        public IEnumerable<SelectListItem> StorageDropdownLists { get; set; }
    }
}