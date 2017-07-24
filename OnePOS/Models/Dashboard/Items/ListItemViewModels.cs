using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnePOS.Models.Dashboard.Items
{
    public class ListItemViewModels
    {
        
        public string ItemName { get; set; }
        public decimal ItemBuyPrice { get; set; }
        public decimal ItemSalePrice { get; set; }
        public string ItemLocation { get; set; }
        //public string ItemBrandType { get; set; }
        public decimal ItemQuantitiy { get; set; }
        public string ItemVendorName { get; set; }
        public int ItemId { get; set; }
    }
}