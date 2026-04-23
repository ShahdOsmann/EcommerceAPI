using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Common
{
    public class ProductFilterParameters: BaseFilterParameters
    {
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
        public decimal MinCount { get; set; }
        public decimal MaxCount { get; set; }
    }
}
