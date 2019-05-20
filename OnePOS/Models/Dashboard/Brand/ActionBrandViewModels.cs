using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnePOS.Models.Dashboard.Brand
{
    public class ActionBrandViewModels
    {
       
        public int BrandId { get; set; }
        [Required(ErrorMessage = "Please fill the brand name")]
        public string BrandName { get; set; }
        public string BrandDescription { get; set; }
        
        public IEnumerable<SelectListItem> BrandCategoryDropdownLists { get; set; }
        public string BrandCategoryId { get; set; }

    }
}