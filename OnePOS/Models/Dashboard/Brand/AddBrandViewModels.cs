using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnePOS.Models.Dashboard.Brand
{
    public class AddBrandViewModels
    {
       
        public int BrandId { get; set; }
       
        public string BrandName { get; set; }
        public string BrandDescription { get; set; }
        
        public IEnumerable<SelectListItem> BrandCategoryDropdownLists { get; set; }
        public string BrandCategoryId { get; set; }

    }
}