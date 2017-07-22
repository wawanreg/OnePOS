using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnePOS.Models.Dashboard
{
    public class ManufacturerViewModels
    {
        [Key]
        public int ManufacturerId { get; set; }
        public string ManufacturerUniqueId { get; set; }
        public string ManufacturerName { get; set; }
        public string ManufacturerDescription { get; set; }
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