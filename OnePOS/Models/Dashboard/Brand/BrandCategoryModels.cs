using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnePOS.Models.Dashboard.Brand
{
    public class BrandCategoryModels
    {
        [Key]
        public int BrandCategoryId { get; set; }
        public string BrandCategoryName { get; set; }
        

    }
}