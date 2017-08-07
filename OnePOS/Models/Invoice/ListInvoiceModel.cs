
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.VisualBasic;

namespace OnePOS.Models.Invoice
{
    public class ListInvoiceMode
    {
        public int BillingHeaderId { get; set; }
        public string NoInvoice { get; set; }
        [Column(TypeName = "DateTime2")]
        public DateTime InvoiceDate { get; set; }
        public string BillingStatus { get; set; }
        public decimal TotalPayment { get; set; }
        
    }
}