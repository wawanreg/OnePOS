using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
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

        public int ReduceInvoiceValue { get; set; }
        public decimal TotalItem { get; set; }
        //public int TotalPricePerItem { get; set; }
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public int DiscontTransaction { get; set; }
        public decimal PaymentTax { get; set; }
        public virtual VendorViewModels Vendor { get; set; }
        public virtual BillingStatusModel BillingStatus { get; set; }
        //public virtual List<BillingDetailModel> BillingDetails { get; set; }

        public virtual List<BillingDetailCollectionModel> DetailCollections { get; set; }

        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public decimal TotalPayment
        {
            get
            {
                decimal invoicePrice = DetailCollections.Sum(billingDetailCollectionModel => billingDetailCollectionModel.TotalPricePerItemAfterDiscount);
                var invoiceDisc = ((invoicePrice * DiscontTransaction) / 100);
                return invoicePrice - invoiceDisc - ReduceInvoiceValue;   
            }
        }
        public virtual List<decimal> MaxStorageItm { get; set; }
        public int SelectedValue { get; set; }
        public IEnumerable<SelectListItem> BillStatusDropdownLists { get; set; }
    }
}