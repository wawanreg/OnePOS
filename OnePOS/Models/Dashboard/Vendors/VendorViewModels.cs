using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnePOS.Models.Dashboard.Vendors
{
    public class VendorViewModels
    {
        [Key]
        public int VendorId { get; set; }
        public string VendorUniqueId { get; set; }
        public string VendorName { get; set; }
        public string VendorAddress { get; set; }
        public string VendorPhone { get; set; }
        public string VendorEmail { get; set; }
        public string VendorOwner { get; set; }
        public string VendorCs { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        public string CreatedBy { get; set; }
        [Column(TypeName = "DateTime2")]
        public DateTime CreatedDate { get; set; }
        public string CreatedAgent { get; set; }
        public string UpdatedBy { get; set; }
        [Column(TypeName = "DateTime2")]
        public DateTime UpdatedDate { get; set; }
        public string UpadtedAgent { get; set; }
    }
}