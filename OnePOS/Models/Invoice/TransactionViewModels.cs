using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OnePOS.Models.Dashboard.Vendors;


namespace OnePOS.Models.Invoice
{
    public class TransactionViewModels
    {
        
        public decimal TotalTransaction { get; set; }
        public decimal TotalPayment { get; set; }
        public string Quantity { get; set; }
        public string ItemId { get; set; }
        
    }
}