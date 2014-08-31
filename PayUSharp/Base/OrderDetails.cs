using System;
using System.Collections.Generic;

namespace PayU.Base
{
    public enum TokenType 
    {
      PAY_ON_TIME,
      PAY_BY_CLICK
    }

    public class OrderDetails
    {
        public OrderDetails()
        {
            PaymentMethod = "CCVISAMC";
            OrderHash = "";
            OrderDate = DateTime.UtcNow;
            PricesCurrency = "TRY";
        }
        
        /* Required Parameters */
        [Parameter(Name = "MERCHANT", SortIndex = 10)]
        public string Merchant { get; set; }
        
        [Parameter(Name = "LU_ENABLE_TOKEN", SortIndex = 15)]
        public bool? TokenEnable { get; set; }

        [Parameter(Name = "LU_TOKEN_TYPE", SortIndex = 16)]
        public TokenType? TokenType { get; set; }

        [Parameter(Name = "ORDER_REF", SortIndex = 20)]
        public string OrderRef { get; set; }
        
        [Parameter(Name = "ORDER_DATE", FormatString = "{0:yyyy-MM-dd HH:mm:ss}", SortIndex = 30)]
        public DateTime OrderDate { get; set; }
        
        [Parameter(Name = "PAY_METHOD", SortIndex = 110)]
        public string PaymentMethod { get; set; }
        
        [Parameter(Name = "ORDER_HASH", ExcludeFromHash = true)]
        public string OrderHash { get; protected set; }
        
        [Parameter(Name = "PRICES_CURRENCY", SortIndex = 60)]
        public string PricesCurrency { get; set; }
    }
}

