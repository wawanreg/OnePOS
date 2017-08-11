using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using OnePOS.Models.Dashboard.Vendors;


namespace OnePOS.Models.Invoice
{
    public class BillingCollectionModel
    {
        public bool IsDeleted { get;set; }
        public int NoBillingHeader { get; set; }
        public string NoInvoice { get; set; }
        [Column(TypeName = "DateTime2")]
        public DateTime InvoiceDate { get; set; }
        public decimal TotalPayment { get; set; }
        public decimal TotalItem { get; set; }
        public virtual VendorViewModels Vendor { get; set; }
        public virtual BillingStatusModel BillingStatus { get; set; }
        public virtual List<BillingDetailModel> BillingDetails { get; set; }
        public IEnumerable<SelectListItem> BillStatusDropdownLists { get; set; }
    }
}