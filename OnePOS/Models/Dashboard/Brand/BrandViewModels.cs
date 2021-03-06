﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnePOS.Models.Dashboard.Brand
{
    public class BrandViewModels
    {
        [Key]
        public int BrandId { get; set; }
        public string BrandUniqueId { get; set; }
        [Required(ErrorMessage = "Fill This Field")]
        public string BrandName { get; set; }
        public string BrandDescription { get; set; }
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