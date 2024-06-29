using Microsoft.AspNetCore.Http;
using Natural.Core.Models;
using System;
using System.Collections.Generic;

#nullable disable

namespace Natural.Core.Models
{
    public partial class ProductModel
    {
        public string Id { get; set; }
        public string Category { get; set; }
        public string ProductName { get; set; }
        public decimal? Price { get; set; }
        public IFormFile UploadImage { get; set; }
        public int? Quantity { get; set; }
        public decimal? Weight { get; set; }
        public string ProductType { get; set; }

    }
}
