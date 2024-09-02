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
        [Required(ErrorMessage = "Category is required.")]
        public string Category { get; set; }
        [Required(ErrorMessage = "ProductName is required.")]
        [StringLength(40, ErrorMessage = "Product name must be 40 characters or fewer.")]
       // [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Product name can only contain letters.")]
        //[RegularExpression("[a-zA-Z]{1,40}", ErrorMessage = "must contain upto 40 alphabets only")]
        public string ProductName { get; set; }
        [RegularExpression(@"^\d*\.?\d+$", ErrorMessage = " Numbers and decimals are allowed & Words and special characters are not allowed.")]
        [Required(ErrorMessage = "Price is required.")]
        public decimal? Price { get; set; }
        public decimal DisplayPrice { get; set; }

        public IFormFile UploadImage { get; set; }
        [Required(ErrorMessage = "Quantity is required.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Numbers and decimals are allowed & Words and special characters are not allowed.")]
        public int? Quantity { get; set; }
        [Required(ErrorMessage = "Weight is required.")]
        [RegularExpression(@"^\d*\.?\d+$", ErrorMessage = "Numbers and decimals are allowed & Words and special characters are not allowed.")]
      
        public decimal? Weight { get; set; }
        [Required(ErrorMessage = "ProductType is required.")]
        public string ProductType { get; set; }


    }
}

