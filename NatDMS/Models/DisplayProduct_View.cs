

using System;
using Natural.Core.Models;
#nullable disable

namespace NatDMS.Models
{
	public class DisplayProduct_View
	{
        public List<CategoryModel> CategoryList { get; set; }
        public string Category { get; set; }
        public List<EditProduct> product { get; set; }
        public string ProductName { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPageCount { get; set; }
        public decimal DisplayPrice { get; set; }



        // public int TotalPages { get; set; }
        public int TotalItems { get; set; }


    }
}

