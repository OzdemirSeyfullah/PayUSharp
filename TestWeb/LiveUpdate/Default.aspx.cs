
using PayU.LiveUpdate;
using System;
using PayU;

namespace TestWeb.LiveUpdate
{
    public partial class Default : System.Web.UI.Page
    {
        public virtual void button1Clicked (object sender, EventArgs args)
        {
            form1.Disabled = true;
            form1.Visible = false;

            Configuration.Instance
                .SetSignatureKey("P5@F8*3!m0+?^9s3&u8(")
                .SetEnvironment("https://secure.payuodeme.com/order/");

            var order = new OrderDetails();
            order.Merchant = "PAYUDEMO";
            order.OrderRef = "EXT_" + new Random().Next(100000, 999999).ToString();
            
            order.ProductDetails.Add(new ProductDetails
                                          {
                Code = "TCK1",
                Name = "Ticket1",
                Quantity = 1,
                UnitPrice = 1M,
                Information = "Barcelona flight"
            });
            order.ProductDetails.Add(new ProductDetails
                                          {
                Code = "TCK2",
                Name = "Ticket2",
                Quantity = 1,
                UnitPrice = 1M,
                Information = "London Flight"
            });

            order.ShippingCosts = 2M;
            order.PricesCurrency = "TRY";
//            order.PaymentMethod = "";
            order.DestinationCity = "Ankara";
            order.DestinationState = "Ankara";
            order.DestinationCountry = "TR";
            order.TestOrder = true;
            order.InstallmentOptions = "2,3,7,10,12";
            
            order.BillingDetails = new BillingDetails {
                FirstName = textBoxName.Text,
                LastName = textBoxLastName.Text,
                Email = textBoxEmail.Text,
                City = "Kağıthane", //Ilce/Semt
                State = "Istanbul", //Sehir
                CountryCode = "TR"
            };
            
            var request = new LiveUpdateRequest(order);

            ltrLiveUpdateForm.Text = request.RenderPaymentForm("Ödeme Yap");

            ltrLiveUpdateForm.Text += @"<script>document.getElementById('payForm').submit();</script>";
        }
    }
}

