using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnePOS.Models.Dashboard.Items
{
    public class AddItemViewModels
    {
     
        public string ItemName { get; set; }
        public string ItemSalePrice { get; set; }
        public string ItemBuyPrice   { get; set; }
        public string ItemLocation { get; set; }
        public string ItemBrandType { get; set; }
        public string ItemQuantitiy { get; set; }
        public string ItemVendor { get; set; }
    }
}