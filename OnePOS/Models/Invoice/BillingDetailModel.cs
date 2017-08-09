using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OnePOS.Models.Dashboard.Items;

namespace OnePOS.Models.Invoice
{
    public class BillingDetailModel
    {
        public BillingDetailModel()
        {
            Active = true;
            Deleted = false;
        }
        [Key]
        public int NoBillingDetail { get; set; }
        public virtual BillingHeaderModel BillingHeader { get; set; }
        public virtual ItemViewModels Item { get; set; }
        public decimal Quantity { get; set; }
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
        
        //[NotMapped]
        //public decimal TotalCalulated { get { return Quantity * Item.SalePrice; } }
    }
}