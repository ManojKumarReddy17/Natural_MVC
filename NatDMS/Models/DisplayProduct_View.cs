

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
    }
}

