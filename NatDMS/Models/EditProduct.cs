using Natural.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace NatDMS.Models
{
    public partial class EditProduct
    {
        public string Id { get; set; }
        public List<CategoryModel> Categorylist { get; set; }
        [Required(ErrorMessage = "Category is required.")]
        public string Category { get; set; }
        [Required(ErrorMessage = "Category is required.")]
        public string CategoryId { get; set; }
        [Required(ErrorMessage = "Product Name is required.")]
        [RegularExpression("[a-zA-Z]{1,50}", ErrorMessage = "must contain upto 50 alphabets only")]
        public string ProductName { get; set; }
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public decimal Price { get; set; }
        //public string PresignedUrl { get; set; }
        public string Image { get;set; }
        public IFormFile UploadImage { get; set; }
        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int? Quantity { get; set; }
        [Required(ErrorMessage = "Weight is required.")]
        public decimal? Weight { get; set; }

        [Required(ErrorMessage = "Product Type is required.")]
        public string ProductType { get; set; }


    }
}


