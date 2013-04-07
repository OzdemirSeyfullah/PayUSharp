using System;

namespace PayU.AutomaticLiveUpdate
{
    public class ProductDetails: PayU.Base.ProductDetails
    {
        [Parameter(Name = "ORDER_VER")]
        public string Version { get; set; }
    }
}