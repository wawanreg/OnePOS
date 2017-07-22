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
     
        public string VendorName { get; set; }
        public string VendorAddress { get; set; }
        public string VendorPhone   { get; set; }
        
        public string VendorEmail { get; set; }
        public string VendorOwner { get; set; }
    }
}