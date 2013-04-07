using System;
using System.Collections.Generic;

namespace PayU.AutomaticLiveUpdate
{
    public class OrderDetails: PayU.Base.OrderDetails
    {
        public OrderDetails() {
            ProductDetails = new List<ProductDetails>();
        }

        [Parameter(IsNested = true)]
        public IList<ProductDetails> ProductDetails { get; set; }
        
        [Parameter(IsNested = true)]
        public BillingDetails BillingDetails { get; set; }
        
        [Parameter(IsNested = true)]
        public CardDetails CardDetails { get; set; }
        
        [Parameter(IsNested = true)]
        public DeliveryDetails DeliveryDetails { get; set; }

        [Parameter(Name = "BACK_REF")]
        public string ReturnUrl { get; set; }
        
        /* Optional Parameters */
        
        [Parameter(Name = "CLIENT_IP")]
        public string ClientIpAddress { get; set; }
        
        [Parameter(Name = "CLIENT_TIME", FormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        public DateTime? ClientTime { get; set; }
        
        [Parameter(Name = "SELECTED_INSTALLMENTS_NUMBER")]
        public int? SelectedInstallmentNumber { get; set; }
        
        [Parameter(Name = "CARD_PROGRAM_NAME")]
        public string CardProgramName { get; set; }        

        [Parameter(Name = "ORDER_TIMEOUT")]
        public int? OrderTimeout { get; set; }
    }
}

