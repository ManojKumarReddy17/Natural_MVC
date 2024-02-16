﻿using System;
#nullable disable

namespace Natural.Core.Models
{
	public class DsrProduct
	{
        public string Id { get; set; }

        public string Category { get; set; }
        public string ProductName { get; set; }
        public decimal? Price { get; set; }
        public string Quantity { get; set; }
        public int? Weight { get; set; }
    }
}
