using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Natural.Core.Models
{
    public class PaginatioResult<T>
    {
        public int TotalItems { get; set; }
        public int TotalPageCount { get; set; }
        public List<T> Items { get; set; }
    }
}
