using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnePOS.Models.Dashboard.Items
{
    public class DdCollectionItemModels
    {
        public IEnumerable<SelectListItem> VendorDropdownLists { get; set; }
        public IEnumerable<SelectListItem> BrandDropdownLists { get; set; }

    }
}