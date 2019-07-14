using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OnePOS.Models.Dashboard.Vendors;


namespace OnePOS.Models.Invoice
{
    public class BillingHeaderModel
    {
        public BillingHeaderModel()
        {
            Active = true;
            Deleted = false;
        }
        [Key]
        public int NoBillingHeader { get; set; }
        public string NoInvoice { get; set; }
        public string NoMos { get; set; }
        [Column(TypeName = "DateTime2")]
        public DateTime DueDate { get; set; }
        [Column(TypeName = "DateTime2")]
        public DateTime StartTransactionDate { get; set; }
        [Column(TypeName = "DateTime2")]
        public DateTime EndTransactionDate { get; set; }
        [Column(TypeName = "DateTime2")]
        public DateTime InvoiceDate { get; set; }
        public decimal TotalBeforeTax { get; set; }
        public decimal TotalTransaction { get; set; }
        public decimal TotalItem { get; set; }
        public decimal TotalAfterTax { get; set; }
        public decimal PaymentTax { get; set; }
        public decimal TotalBeforeDiscount { get; set; }
        public decimal TotalPayment { get; set; }
        public int DiscontTransaction { get; set; }
        public int ReduceInvoiceValue { get; set; }
        public int Tax { get; set; }
        public string MerchantEmail { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        public string CreatedBy { get; set; }
        [Column(TypeName = "DateTime2")]
        public DateTime CreatedDate { get; set; }
        public string CreatedAgent { get; set; }
        public string UpdatedBy { get; set; }
        [Column(TypeName = "DateTime2")]
        public DateTime UpdatedDate { get; set; }
        public string UpdatedAgent { get; set; }
        public virtual VendorViewModels Vendor { get; set; }
        public virtual BillingStatusModel BillingStatus { get; set; }
        public virtual ICollection<BillingDetailModel> BillingDetails { get; set; } 


    }
}