using Natural.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace NatDMS.Models
{
    public partial class Product
    {
        public string Id { get; set; }
        public List<CategoryModel> Categorylist { get; set; }
        public string Category { get; set; }
        public string ProductName { get; set; }
        [RegularExpression(@"^\d*\.?\d+$", ErrorMessage = "Only Words Allowd No  Numbers and decimals are allowed & special characters are not allowed.")]
        public decimal Price { get; set; }
        public IFormFile UploadImage { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessage = "Only numbers are allowed. Words and special characters are not allowed.")]
        public int? Quantity { get; set; }

        [RegularExpression(@"^\d*\.?\d+$", ErrorMessage = "Only numbers and decimals are allowed. Words and special characters are not allowed.")]
        public decimal? Weight { get; set; }

    }
}

