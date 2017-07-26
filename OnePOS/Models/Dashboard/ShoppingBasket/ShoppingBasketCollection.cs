using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OnePOS.Models.Dashboard.Brand;
using OnePOS.Models.Dashboard.Storage;
using OnePOS.Models.Dashboard.Vendors;

namespace OnePOS.Models.Dashboard.ShoppingBasket
{
    public class ShoppingBasketCollection
    {
        public int ItemId { get; set; }
        public string ItemUniqueId { get; set; }
        public string ItemName { get; set; }
        public decimal ItemPrice { get; set; }
        public decimal TotalStock { get; set; }
    }
}