using Natural.Core.Models;
using Natural_Core.Models;
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
        public List<ProductType> productTypesList { get; set; }
        public string Category { get; set; }
        [StringLength(30, ErrorMessage = "Product name must be 30 characters or fewer.")]
       // [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Product name can only contain letters.")]
        [RegularExpression("[a-zA-Z]{1,40}", ErrorMessage = "must contain upto 40 alphabets only")]
        public string ProductName { get; set; }
        [RegularExpression(@"^\d*\.?\d+$", ErrorMessage = " Numbers and decimals are allowed & Words and special characters are not allowed.")]
        public decimal Price { get; set; }
        public IFormFile UploadImage { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessage = "Numbers and decimals are allowed & Words and special characters are not allowed.")]
        public int? Quantity { get; set; }

        [RegularExpression(@"^\d*\.?\d+$", ErrorMessage = "Numbers and decimals are allowed & Words and special characters are not allowed.")]
        public decimal? Weight { get; set; }
        public string ProductType { get; set; }


    }
}

