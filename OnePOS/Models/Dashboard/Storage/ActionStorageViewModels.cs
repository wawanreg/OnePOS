﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnePOS.Models.Dashboard.Storage
{
    public class ActionStorageViewModels
    {
        
        public int StorageId { get; set; }
        public string StorageUniqueId { get; set; }
        [Required(ErrorMessage = "Fill This Field")]
        public string StorageName { get; set; }
        public string StorageDescription { get; set; }


    }
}