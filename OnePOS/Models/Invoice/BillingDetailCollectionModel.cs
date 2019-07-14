using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using System.Windows.Documents;
using OnePOS.Models.Dashboard.Vendors;


namespace OnePOS.Models.Invoice
{
    
    public class BillingDetailCollectionModel
    {
        //public BillingDetailCollectionModel()
        //{
            
        //    TotalPricePerItem = PricePerItem*BuyQuantity;
        //    TotalPricePerItemAfterDiscount = TotalPricePerItem - ((TotalPricePerItem * DiscountPerItem) / 100);
        //}
        public int BillingDetailId { get; set; }
        public int ItemIndexing { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public decimal BuyQuantity { get; set; }
        public decimal OriginalQuantity { get; set; }
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public decimal PricePerItem { get; set; }
        public int DiscountPerItem { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public decimal TotalPricePerItem {
            get { return PricePerItem * BuyQuantity; }
        }
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public decimal CurrentValueDiscount
        {
            get { return (TotalPricePerItem * DiscountPerItem) / 100; }
            
        }

        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public decimal TotalPricePerItemAfterDiscount {
            get { return TotalPricePerItem - CurrentValueDiscount; }
        }
    }
}