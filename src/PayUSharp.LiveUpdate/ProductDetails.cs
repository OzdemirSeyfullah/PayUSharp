using System;
using System.Collections.Generic;
using PayU.Core;

namespace PayU.LiveUpdate
{
    public enum PriceType
    {
        GROSS,
        NET
    }

    public class ProductDetails: PayU.Core.Base.ProductDetails
    {
        public ProductDetails() {
            PriceType = PriceType.NET;
            Code = "";
            Name = "";
            Information = "";
        }

        [Parameter(Name = "ORDER_VAT", SortIndex = 46)]
        public decimal VAT { get; set; }

        [Parameter(Name = "ORDER_PRICE_TYPE", SortIndex = 115)]
        public PriceType PriceType { get; set; }
    }

}

