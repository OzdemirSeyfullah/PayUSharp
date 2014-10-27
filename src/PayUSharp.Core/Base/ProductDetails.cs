using System;
using PayU.Core;

namespace PayU.Core.Base
{
    public class ProductDetails
    {
        /* Required Fields */

        [Parameter(Name = "ORDER_PNAME", SortIndex = 41)]
        public string Name { get; set; }
        
        [Parameter(Name = "ORDER_PCODE", SortIndex = 42)]
        public string Code { get; set; }
        
        [Parameter(Name = "ORDER_PRICE", SortIndex = 44)]
        public decimal UnitPrice { get; set; }
        
        [Parameter(Name = "ORDER_QTY", SortIndex = 45)]
        public int Quantity { get; set; }

        /* Optional Fields */

        [Parameter(Name = "ORDER_PINFO", SortIndex = 43)]
        public string Information { get; set; }
    }
}

