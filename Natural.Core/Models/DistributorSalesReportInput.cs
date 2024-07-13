using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable disable
namespace Natural.Core.Models
{
    public class DistributorSalesReportInput
    {

        public string Product { get; set; }
        public string Product_Name { get; set; }
        public string productType { get; set; }
        //public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal SaleAmount { get; set; }
    }
}
