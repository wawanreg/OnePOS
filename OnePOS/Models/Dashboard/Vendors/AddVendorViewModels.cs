using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnePOS.Models.Dashboard.Vendors
{
    public class AddVendorViewModels
    {
        [Required(ErrorMessage = "Fill This Field")]
        public string VendorName { get; set; }
        [Required(ErrorMessage = "Fill This Field")]
        public string VendorAddress { get; set; }
        [Required(ErrorMessage = "Fill This Field")]
        public string VendorPhone   { get; set; }
        public string VendorEmail { get; set; }
        public string VendorOwner { get; set; }
    }
}