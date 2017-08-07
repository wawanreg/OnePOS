using System.ComponentModel.DataAnnotations;

namespace OnePOS.Models.Invoice
{
    public class BillingStatusModel
    {
        [Key]
        public int BillingStatusId { get; set; }
        public string BillingName { get; set; }
    }
}