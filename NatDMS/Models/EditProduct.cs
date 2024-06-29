using Natural.Core.Models;
using System;
using System.Collections.Generic;

#nullable disable

namespace NatDMS.Models
{
    public partial class EditProduct
    {
        public string Id { get; set; }
        public List<CategoryModel> Categorylist { get; set; }

        public string Category { get; set; }
        public string CategoryId { get; set; }  
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        //public string PresignedUrl { get; set; }
        public string Image { get;set; }
        public IFormFile UploadImage { get; set; }
        public int? Quantity { get; set; }
        public decimal? Weight { get; set; }
        public string ProductType { get; set; }


    }
}

